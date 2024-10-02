using MediatR;
using smERP.Application.Contracts.Persistence;
using smERP.Application.Features.ProductInstances.Commands.Models;
using smERP.Domain.Entities.Product;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Collections.Generic;

namespace smERP.Application.Features.ProductInstances.Commands.Handlers;

public class ProductInstanceCommandHandler(IProductRepository productRepository, IAttributeRepository attributeRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<AddProductInstanceCommandModel, IResultBase>,
    IRequestHandler<EditProductInstanceCommandModel, IResultBase>,
    IRequestHandler<DeleteProductInstanceCommandModel, IResultBase>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IResultBase> Handle(AddProductInstanceCommandModel request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdWithProductInstances(request.ProductId);
        if (product == null)
            return new Result<ProductInstance>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Product.Localize()));

        var attributeValuesList = request.ProductInstanceAttributeValues.Select(av => (av.AttributeId, av.AttributeValueId)).ToList();
        var doesAttributeValuesExist = await _attributeRepository.DoesListExist(attributeValuesList);
        if (!doesAttributeValuesExist)
            return new Result<ProductInstance>()
                .WithBadRequest(SharedResourcesKeys.SomeItemsIn___ListAreNotCorrect.Localize(SharedResourcesKeys.AttributeList.Localize()));

        var productInstanceToBeCreatedResult = product.AddProductInstance(request.SellingPrice ?? 0, attributeValuesList);
        if (productInstanceToBeCreatedResult.IsFailed)
            return productInstanceToBeCreatedResult;

        _productRepository.Update(product);

        await productInstanceToBeCreatedResult.WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (productInstanceToBeCreatedResult.IsFailed)
            return productInstanceToBeCreatedResult;

        return productInstanceToBeCreatedResult.ChangeType(productInstanceToBeCreatedResult.Value.Id).WithCreated();
    }

    public async Task<IResultBase> Handle(EditProductInstanceCommandModel request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdWithProductInstances(request.ProductId);
        if (product == null)
            return new Result<ProductInstance>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Product.Localize()));

        var productInstanceToBeEdited = product.ProductInstances.FirstOrDefault(x => x.Id == request.ProductInstanceId);
        if (productInstanceToBeEdited == null)
            return new Result<ProductInstance>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Product.Localize()));

        IResult<ProductInstance> productInstanceToBeEditedResult = new Result<ProductInstance>(productInstanceToBeEdited);

        //if (request.QuantityInStock != null && request.QuantityInStock.HasValue && request.QuantityInStock > -1)
        //{
        //    productInstanceToBeEditedResult = productInstanceToBeEdited.UpdateQuantity(request.QuantityInStock.Value);
        //    if (productInstanceToBeEditedResult.IsFailed)
        //        return productInstanceToBeEditedResult;
        //}

        //if (request.BuyingPrice != null && request.BuyingPrice.HasValue && request.BuyingPrice > -1)
        //{
        //    productInstanceToBeEditedResult = productInstanceToBeEdited.UpdateBuyingPrice(request.BuyingPrice.Value);
        //    if (productInstanceToBeEditedResult.IsFailed)
        //        return productInstanceToBeEditedResult;
        //}

        if (request.SellingPrice != null && request.SellingPrice.HasValue && request.SellingPrice > -1)
        {
            productInstanceToBeEditedResult = productInstanceToBeEdited.UpdateSellingPrice(request.SellingPrice.Value);
            if (productInstanceToBeEditedResult.IsFailed)
                return productInstanceToBeEditedResult;
        }

        if (request.ProductInstanceAttributeValues != null && request.ProductInstanceAttributeValues.Count > 0)
        {
            var attributeValuesList = request.ProductInstanceAttributeValues.Select(av => (av.AttributeId, av.AttributeValueId)).ToList();
            productInstanceToBeEditedResult = product.UpdateProductInstanceAttribute(request.ProductInstanceId, attributeValuesList);
            if (productInstanceToBeEditedResult.IsFailed)
                return productInstanceToBeEditedResult;
        }

        _productRepository.Update(product);

        await productInstanceToBeEditedResult.WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (productInstanceToBeEditedResult.IsFailed)
            return productInstanceToBeEditedResult;

        return productInstanceToBeEditedResult.WithUpdated();
    }

    public async Task<IResultBase> Handle(DeleteProductInstanceCommandModel request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdWithProductInstances(request.ProductId);
        if (product == null)
            return new Result<ProductInstance>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Product.Localize()));

        var productToBeDeletedResult = product.RemoveProductInstance(request.ProductInstanceId);
        if (productToBeDeletedResult.IsSuccess)
            return productToBeDeletedResult;

        _productRepository.Update(product);

        await productToBeDeletedResult.WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (productToBeDeletedResult.IsFailed)
            return productToBeDeletedResult;

        return productToBeDeletedResult.WithDeleted();
    }
}
