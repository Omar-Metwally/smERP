﻿using MediatR;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;
using smERP.Domain.Entities.InventoryTransaction;
using smERP.Persistence.Contracts;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.ProcurementTransactions.Commands.Handlers;

public class ProcurementTransactionCommandHandler(
    IProcurementTransactionRepository procurementTransactionRepository,
    IBranchRepository branchRepository,
    IProductRepository productRepository,
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<AddProcurementTransactionCommandModel, IResultBase>,
    IRequestHandler<EditProcurementTransactionCommandModel, IResultBase>,
    IRequestHandler<DeleteProcurementTransactionCommandModel, IResultBase>

{
    private readonly IProcurementTransactionRepository _procurementTransactionRepository = procurementTransactionRepository;
    private readonly IBranchRepository _branchRepository = branchRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IResultBase> Handle(AddProcurementTransactionCommandModel request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdWithStorageLocations(request.BranchId);
        if (branch == null)
            return new Result<ProcurementTransaction>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Branch.Localize()));

        var storageLocation = branch.StorageLocations.FirstOrDefault(x => x.Id == request.StorageLocationId);
        if (storageLocation == null)
            return new Result<ProcurementTransaction>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.StorageLocation.Localize()));

        var supplier = await _supplierRepository.GetByID(request.SupplierId);
        if (supplier == null)
            return new Result<ProcurementTransaction>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Supplier.Localize()));

        var productInstances = await _productRepository.GetProductInstancesWithProduct(request.Products.Select(x => x.ProductInstanceId));
        if (productInstances == null || productInstances.Count() != request.Products.Count)
            return new Result<ProcurementTransaction>()
                .WithBadRequest(SharedResourcesKeys.SomeItemsIn___ListAreNotCorrect.Localize(SharedResourcesKeys.Product.Localize()));

        var payments = new List<(decimal PayedAmount, string PaymentMethod)>();

        if (request.Payment != null)
        {
            payments.Add((request.Payment.PayedAmount, request.Payment.PaymentMethod));
        }

        var transactionItems = request.Products.Select(x =>
            (x.ProductInstanceId, x.Quantity, x.UnitPrice))
            .ToList();

        var procurementTransactionToBeCreatedResult = ProcurementTransaction.Create(storageLocation.Id, request.SupplierId, payments, transactionItems);
        if (procurementTransactionToBeCreatedResult.IsFailed)
            return procurementTransactionToBeCreatedResult;

        await _procurementTransactionRepository.Add(procurementTransactionToBeCreatedResult.Value, cancellationToken);

        var productToBeStored = request.Products.Select(x =>
        {
            var productInstance = productInstances.FirstOrDefault(z => z.IsTracked && z.ProductInstanceId == x.ProductInstanceId);

            return (
                x.ProductInstanceId,
                x.Quantity,
                productInstance.IsTracked,
                productInstance.ShelfLifeInDays,
                (x.Items ?? Enumerable.Empty<ProductItem>()).Select(m => (m.SerialNumber, m.ExpirationDate)).ToList()
            );
        }).ToList();

        var productToBeStoredResult = storageLocation.AddStoredProductInstances(productToBeStored);
        if (productToBeStoredResult.IsFailed)
            return productToBeStoredResult;

        await procurementTransactionToBeCreatedResult.WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (procurementTransactionToBeCreatedResult.IsFailed)
            return procurementTransactionToBeCreatedResult;

        return procurementTransactionToBeCreatedResult.ChangeType(procurementTransactionToBeCreatedResult.Value.Id);
    }

    public Task<IResultBase> Handle(EditProcurementTransactionCommandModel request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IResultBase> Handle(DeleteProcurementTransactionCommandModel request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}