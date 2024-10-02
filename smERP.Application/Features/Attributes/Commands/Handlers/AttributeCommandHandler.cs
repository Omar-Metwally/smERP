using MediatR;
using smERP.Application.Contracts.Persistence;
using smERP.Application.Features.Attributes.Commands.Models;
using smERP.Domain.Entities.Product;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using Attribute = smERP.Domain.Entities.Product.Attribute;


namespace smERP.Application.Features.Attributes.Commands.Handlers;

public class AttributeCommandHandler(IAttributeRepository attributeRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<AddAttributeCommandModel, IResultBase>,
    IRequestHandler<EditAttributeCommandModel, IResultBase>,
    IRequestHandler<DeleteAttributeCommandModel, IResultBase>,
    IRequestHandler<AddAttributeValueCommandModel, IResultBase>,
    IRequestHandler<EditAttributeValueCommandModel, IResultBase>,
    IRequestHandler<DeleteAttributeValueCommandModel, IResultBase>
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IResultBase> Handle(AddAttributeCommandModel request, CancellationToken cancellationToken)
    {
        var doesEnglishNameExist = await _attributeRepository.DoesExist(x => x.Name.English == request.EnglishName);
        if (doesEnglishNameExist)
            return new Result<Attribute>()
                .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.NameEn.Localize()));

        var doesArabicNameExist = await _attributeRepository.DoesExist(x => x.Name.Arabic == request.ArabicName);
        if (doesArabicNameExist)
            return new Result<Attribute>()
                .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.NameAr.Localize()));

        var attributeToBeCreated = Attribute.Create(request.EnglishName, request.ArabicName);
        if (attributeToBeCreated.IsFailed)
            return attributeToBeCreated;

        await attributeToBeCreated
            .WithTask(() => _attributeRepository.Add(attributeToBeCreated.Value, cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeToBeCreated.IsFailed)
            return attributeToBeCreated;

        await attributeToBeCreated
            .WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeToBeCreated.IsFailed)
            return attributeToBeCreated;

        return attributeToBeCreated.ChangeType(attributeToBeCreated.Value.Id).WithCreated();

    }

    public async Task<IResultBase> Handle(EditAttributeCommandModel request, CancellationToken cancellationToken)
    {
        var attributeToBeEdited = await _attributeRepository.GetByID(request.AttributeId);
        if (attributeToBeEdited == null)
            return new Result<Attribute>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Attribute.Localize()));

        if (!string.IsNullOrWhiteSpace(request.EnglishName))
        {
            var doesEnglishNameExist = await _attributeRepository.DoesExist(x => x.Name.English == request.EnglishName && x.Id != request.AttributeId);
            if (doesEnglishNameExist)
                return new Result<Attribute>()
                    .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.NameEn.Localize()));

            attributeToBeEdited.Name.UpdateEnglish(request.EnglishName);
        }

        if (!string.IsNullOrWhiteSpace(request.ArabicName))
        {
            var doesArabicNameExist = await _attributeRepository.DoesExist(x => x.Name.Arabic == request.ArabicName && x.Id != request.AttributeId);
            if (doesArabicNameExist)
                return new Result<Attribute>()
                    .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.NameAr.Localize()));

            attributeToBeEdited.Name.UpdateArabic(request.ArabicName);
        }

        _attributeRepository.Update(attributeToBeEdited);

        var attributeUpdateResult = await new Result<Attribute>(attributeToBeEdited)
            .WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeUpdateResult.IsFailed)
            return attributeUpdateResult;

        return attributeUpdateResult.WithUpdated();
    }

    public async Task<IResultBase> Handle(DeleteAttributeCommandModel request, CancellationToken cancellationToken)
    {
        var attributeToBeDeleted = await _attributeRepository.GetByID(request.AttributeId);
        if (attributeToBeDeleted == null)
            return new Result<Attribute>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Attribute.Localize()));

        _attributeRepository.Remove(attributeToBeDeleted);

        var attributeDeleteResult = await new Result<Attribute>(attributeToBeDeleted)
            .WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeDeleteResult.IsFailed)
            return attributeDeleteResult;

        return attributeDeleteResult.WithDeleted();
    }

    public async Task<IResultBase> Handle(AddAttributeValueCommandModel request, CancellationToken cancellationToken)
    {
        var attribute = await _attributeRepository.GetByID(request.AttributeId);
        if (attribute == null)
            return new Result<AttributeValue>().WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Attribute.Localize()));

        var attributeValueToBeAddedResult = attribute.AddAttributeValue(request.EnglishName, request.ArabicName);
        if (attributeValueToBeAddedResult.IsFailed)
            return attributeValueToBeAddedResult;

        _attributeRepository.Update(attribute);

        await attributeValueToBeAddedResult
            .WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeValueToBeAddedResult.IsFailed)
            return attributeValueToBeAddedResult;

        return attributeValueToBeAddedResult.ChangeType(attributeValueToBeAddedResult.Value.Id).WithCreated();
    }

    public async Task<IResultBase> Handle(EditAttributeValueCommandModel request, CancellationToken cancellationToken)
    {
        var attribute = await _attributeRepository.GetByID(request.AttributeId);
        if (attribute == null)
            return new Result<AttributeValue>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Attribute.Localize()));

        var attributeValueToBeEditedResult = attribute.UpdateAttributeValue(request.AttributeValueId, request.EnglishName, request.ArabicName);
        if (attributeValueToBeEditedResult.IsFailed)
            return attributeValueToBeEditedResult;

        _attributeRepository.Update(attribute);

        await attributeValueToBeEditedResult
            .WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeValueToBeEditedResult.IsFailed)
            return attributeValueToBeEditedResult;

        return attributeValueToBeEditedResult.WithUpdated();
    }

    public async Task<IResultBase> Handle(DeleteAttributeValueCommandModel request, CancellationToken cancellationToken)
    {
        var attribute = await _attributeRepository.GetByID(request.AttributeId);
        if (attribute == null)
            return new Result<AttributeValue>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Attribute.Localize()));

        var attributeValueToBeDeletedResult = attribute.RemoveAttributeValue(request.AttributeValueId);

        _attributeRepository.Update(attribute);

        await attributeValueToBeDeletedResult
            .WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (attributeValueToBeDeletedResult.IsFailed)
            return attributeValueToBeDeletedResult;

        return attributeValueToBeDeletedResult.WithDeleted();
    }
}
