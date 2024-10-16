using MediatR;
using smERP.Application.Contracts.Persistence;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;
using smERP.Domain.Entities.InventoryTransaction;
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
    //IRequestHandler<EditProcurementTransactionCommandModelOld, IResultBase>,
    IRequestHandler<DeleteProcurementTransactionCommandModel, IResultBase>,
    IRequestHandler<EditProcurementTransactionCommandModel, IResultBase>

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
        {
            var productInstance = productInstances.FirstOrDefault(z => z.IsTracked && z.ProductInstanceId == x.ProductInstanceId);

            return (
                x.ProductInstanceId,
                x.Quantity,
                x.UnitPrice,
                productInstance.IsTracked,
                x.Units?.Select(item => item.SerialNumber).ToList()
            );
        }).ToList();

        var procurementTransactionToBeCreatedResult = ProcurementTransaction.Create(storageLocation.Id, request.SupplierId, payments, transactionItems);
        if (procurementTransactionToBeCreatedResult.IsFailed)
            return procurementTransactionToBeCreatedResult;

        await _procurementTransactionRepository.Add(procurementTransactionToBeCreatedResult.Value, cancellationToken);

        await procurementTransactionToBeCreatedResult.WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (procurementTransactionToBeCreatedResult.IsFailed)
            return procurementTransactionToBeCreatedResult;

        return procurementTransactionToBeCreatedResult.ChangeType(procurementTransactionToBeCreatedResult.Value.Id).WithCreated();
    }

    //public async Task<IResultBase> Handle(EditProcurementTransactionCommandModel request, CancellationToken cancellationToken)
    //{
    //    var procurementTransactionToBeEdited = await _procurementTransactionRepository.GetByID(request.ProcurementTransactionId);
    //    if (procurementTransactionToBeEdited == null)
    //        return new Result<ProcurementTransaction>()
    //            .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.ProcurementTransaction.Localize()));

    //    if (request.SupplierId != null && request.SupplierId.HasValue)
    //    {
    //        var doesSupplierExist = await _supplierRepository.DoesExist(request.SupplierId.Value);
    //        if (!doesSupplierExist)
    //            return new Result<ProcurementTransaction>()
    //                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Supplier.Localize()));

    //        procurementTransactionToBeEdited.UpdateSupplier(request.SupplierId.Value);
    //    }

    //    if (request.Products != null && request.Products.Count > 0)
    //    {
    //        var productInstances = await _productRepository.GetProductInstancesWithProduct(request.Products.Select(x => x.ProductInstanceId));
    //        if (productInstances == null || productInstances.Count() != request.Products.Count)
    //            return new Result<ProcurementTransaction>()
    //                .WithBadRequest(SharedResourcesKeys.SomeItemsIn___ListAreNotCorrect.Localize(SharedResourcesKeys.Product.Localize()));

    //        var transactionItemsToBeEdited = request.Products.Select(x =>
    //        {
    //            var productInstance = productInstances.FirstOrDefault(z => z.IsTracked && z.ProductInstanceId == x.ProductInstanceId);

    //            return (
    //                x.ProductInstanceId,
    //                x.Quantity,
    //                x.UnitPrice,
    //                productInstance.IsTracked,
    //                x.Items?.Select(item => item.SerialNumber).ToList()
    //            );
    //        }).ToList();

    //        //var ProductInstancesToBeEdited = request.Products.Select(x => (x.ProductInstanceId, x.Quantity, x.UnitPrice, x.Items?.Select(z => z.SerialNumber).ToList())).ToList();

    //        var procurementTransactionToBeEditedResult = procurementTransactionToBeEdited.UpdateTransactionItems(transactionItemsToBeEdited);
    //        if (procurementTransactionToBeEditedResult.IsFailed)
    //            return procurementTransactionToBeEditedResult;
    //    }

    //    if (request.Payments != null && request.Payments.Count > 0)
    //    {
    //        var ProductInstancesToBeEdited = request.Payments.Select(x => (x.PaymentTransactionId, x.PayedAmount, x.PaymentMethod)).ToList();

    //        var procurementTransactionToBeEditedResult = procurementTransactionToBeEdited.UpdateTransactionPayments(ProductInstancesToBeEdited);
    //        if (procurementTransactionToBeEditedResult.IsFailed)
    //            return procurementTransactionToBeEditedResult;
    //    }

    //    return new Result<ProcurementTransaction>().WithUpdated();
    //}

    public Task<IResultBase> Handle(DeleteProcurementTransactionCommandModel request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IResultBase> Handle(EditProcurementTransactionCommandModel request, CancellationToken cancellationToken)
    {
        var procurementTransactionToBeEdited = await _procurementTransactionRepository.GetByID(request.TransactionId);
        if (procurementTransactionToBeEdited == null)
            return new Result<ProcurementTransaction>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.ProcurementTransaction.Localize()));

        if (request.SupplierId != null && request.SupplierId.HasValue)
        {
            var doesSupplierExist = await _supplierRepository.DoesExist(request.SupplierId.Value);
            if (!doesSupplierExist)
                return new Result<ProcurementTransaction>()
                    .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Supplier.Localize()));

            procurementTransactionToBeEdited.UpdateSupplier(request.SupplierId.Value);
        }

        var productInstancesId = new[]
        {
            request.NewItems?.Select(x => x.ProductInstanceId) ?? [],
            request.ItemUpdates?.Select(x => x.ProductInstanceId) ?? [],
            request.ItemsToRemove ?? Enumerable.Empty<int>()
        }.SelectMany(x => x).Distinct().ToList();

        var productInstances = await _productRepository.GetProductInstancesWithProduct(productInstancesId);
        if (productInstances == null || productInstances.Count() != productInstancesId.Count)
            return new Result<ProcurementTransaction>()
                .WithBadRequest(SharedResourcesKeys.SomeItemsIn___ListAreNotCorrect.Localize(SharedResourcesKeys.Product.Localize()));

        if (request.ItemsToRemove != null && request.ItemsToRemove.Count > 0)
        {
            var removingItemsResult = procurementTransactionToBeEdited.RemoveItems(request.ItemsToRemove);
            if (removingItemsResult.IsFailed)
                return removingItemsResult;
        }

        if (request.ItemUpdates != null && request.ItemUpdates.Count > 0)
        {
            var itemUpdates = request.ItemUpdates.Select(x =>
            {
                var productInstance = productInstances.FirstOrDefault(z => z.IsTracked && z.ProductInstanceId == x.ProductInstanceId);

                return (
                    x.ProductInstanceId,
                    x.Quantity,
                    x.UnitPrice,
                    productInstance.IsTracked,
                    x.UnitUpdates?.ToAdd?.Select(unitUpdate => (unitUpdate.SerialNumber, unitUpdate.ExpirationDate)).ToList(),
                    x.UnitUpdates?.ToRemove?.ToList()
                );
            }).ToList();

            var updatingItemsResult = procurementTransactionToBeEdited.UpdateItems(itemUpdates);
            if (updatingItemsResult.IsFailed)
                return updatingItemsResult;
        }

        if (request.NewItems != null && request.NewItems.Count > 0)
        {
            var newItems = request.NewItems.Select(x =>
            {
                var productInstance = productInstances.FirstOrDefault(z => z.IsTracked && z.ProductInstanceId == x.ProductInstanceId);

                return (
                    x.ProductInstanceId,
                    x.Quantity,
                    x.UnitPrice,
                    productInstance.IsTracked,
                    x.Units?.Select(unit => (unit.SerialNumber, unit.ExpirationDate)).ToList()
                );
            }).ToList();

            var addingNewItemsResult = procurementTransactionToBeEdited.AddItems(newItems);
            if (addingNewItemsResult.IsFailed)
                return addingNewItemsResult;
        }

        if (request.PaymentsToRemove != null && request.PaymentsToRemove.Count > 0)
        {
            var paymentsRemovingResult = procurementTransactionToBeEdited.RemoveTransactionPayments(request.PaymentsToRemove);
            if (paymentsRemovingResult.IsFailed)
                return paymentsRemovingResult;
        }

        if (request.PaymentUpdates != null && request.PaymentUpdates.Count > 0)
        {
            var paymentsUpdatingResult = procurementTransactionToBeEdited.UpdateTransactionPayments(request.PaymentUpdates.Select(payment => (payment.PaymentTransactionId, payment.PayedAmount, payment.PaymentMethod)).ToList());
            if (paymentsUpdatingResult.IsFailed)
                return paymentsUpdatingResult;
        }

        if (request.NewPayments != null && request.NewPayments.Count > 0)
        {
            var paymentsAddResult = procurementTransactionToBeEdited.AddTransactionPayments(request.NewPayments.Select(payment => (payment.PayedAmount, payment.PaymentMethod)).ToList());
            if (paymentsAddResult.IsFailed)
                return paymentsAddResult;
        }

        _procurementTransactionRepository.Update(procurementTransactionToBeEdited);

        var savingProcurementTransactionUpdatesResult = await new Result<ProcurementTransaction>().WithTask(() => _unitOfWork.SaveChangesAsync(cancellationToken), SharedResourcesKeys.DatabaseError);
        if (savingProcurementTransactionUpdatesResult.IsFailed)
            return savingProcurementTransactionUpdatesResult;

        return savingProcurementTransactionUpdatesResult.WithUpdated();
    }
}