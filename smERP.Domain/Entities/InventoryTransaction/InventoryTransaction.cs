using smERP.Domain.Entities.Organization;
using smERP.Domain.ValueObjects;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace smERP.Domain.Entities.InventoryTransaction;

[NotMapped]
public abstract class InventoryTransaction : Entity
{
    public int StorageLocationId { get; protected set; }
    public DateTime TransactionDate { get; protected set; }
    public decimal TotalAmount { get; protected set; }
    public bool IsCanceled { get; protected set; } = false;
    public bool IsTransactionProcessed { get; protected set; } = false;
    public virtual StorageLocation StorageLocation { get; protected set; } = null!;
    //public Discount? TransactionDiscount { get; protected set; }
    public ICollection<InventoryTransactionItem> Items { get; protected set; } = new List<InventoryTransactionItem>();

    protected InventoryTransaction(int storageLocationId, DateTime transactionDate, ICollection<InventoryTransactionItem> items)
    {
        StorageLocationId = storageLocationId;
        TransactionDate = transactionDate;
        Items = items;
        RecalculateAmount();
    }

    protected InventoryTransaction() { }

    public abstract string GetTransactionType();

    //public void ApplyTransactionDiscount(Discount discount)
    //{
    //    TransactionDiscount = discount;
    //    RecalculateAmount();
    //}

    public void AddItem(InventoryTransactionItem item)
    {
        Items.Add(item);
        RecalculateAmount();
    }

    public void TransactionProcessed()
    {
        IsTransactionProcessed = true;
    }

    public void UnderTransactionProcessing()
    {
        IsTransactionProcessed = false;
    }

    protected virtual void RecalculateAmount()
    {
        decimal subtotal = Items.Sum(item => item.TotalPrice);
        TotalAmount = subtotal;
        //Amount = TransactionDiscount?.Apply(subtotal) ?? subtotal;
    }

    protected static IResult<List<InventoryTransactionItem>> CreateBaseDetails(List<(int ProductInstanceId, int Quantity, decimal UnitPrice)> transactionItems)
    {
        var inventoryTransactionItemsList = new List<InventoryTransactionItem>();

        foreach (var (productInstanceId, quantity, unitPrice) in transactionItems)
        {
            var inventoryTransactionItemsResult = InventoryTransactionItem.Create(productInstanceId, quantity, unitPrice);
            if (inventoryTransactionItemsResult.IsFailed)
                return new Result<List<InventoryTransactionItem>>()
                    .WithErrors(inventoryTransactionItemsResult.Errors)
                    .WithStatusCode(HttpStatusCode.BadRequest);

            inventoryTransactionItemsList.Add(inventoryTransactionItemsResult.Value);
        }

        return new Result<List<InventoryTransactionItem>>(inventoryTransactionItemsList);
    }

    public IResult<List<InventoryTransactionItem>> UpdateTransactionItems(List<(int ProductInstanceId, int Quantity, decimal UnitPrice)> transactionItems)
    {
        foreach (var (productInstanceId, quantity, unitPrice) in transactionItems)
        {
            var existingInventoryTransactionItem = Items.FirstOrDefault(x => x.ProductInstanceId == productInstanceId);
            if (existingInventoryTransactionItem != null)
            {
                var updateResult = existingInventoryTransactionItem.Update(unitPrice, quantity);
                if (updateResult.IsFailed)
                    return new Result<List<InventoryTransactionItem>>()
                        .WithErrors(updateResult.Errors)
                        .WithStatusCode(HttpStatusCode.BadRequest);

                continue;
            }

            var newInventoryTransactionItem = InventoryTransactionItem.Create(productInstanceId, quantity, unitPrice);
            if (newInventoryTransactionItem.IsFailed)
                return new Result<List<InventoryTransactionItem>>()
                    .WithErrors(newInventoryTransactionItem.Errors)
                    .WithStatusCode(HttpStatusCode.BadRequest);

            Items.Add(newInventoryTransactionItem.Value);
        }
        //it would be set false until these updates are reflected in the stored products
        IsTransactionProcessed = false;

        RecalculateAmount();

        return new Result<List<InventoryTransactionItem>>(Items.ToList());
    }

    public IResultBase RemoveTransactionItems(List<int> productInstanceIds)
    {
        if (productInstanceIds.Distinct().Count() == productInstanceIds.Count)
            return new Result<List<InventoryTransactionItem>>()
                .WithError(SharedResourcesKeys.___ListCannotContainDuplicates.Localize(SharedResourcesKeys.Product.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        foreach (var productInstanceId in productInstanceIds)
        {
            var inventoryTransactionItemToBeRemoved = Items.FirstOrDefault(x => x.ProductInstanceId == productInstanceId);
            if (inventoryTransactionItemToBeRemoved == null)
                return new Result<List<InventoryTransactionItem>>()
                    .WithError(SharedResourcesKeys.SomeItemsIn___ListAreNotCorrect.Localize(SharedResourcesKeys.Product.Localize()))
                    .WithStatusCode(HttpStatusCode.BadRequest);

            Items.Remove(inventoryTransactionItemToBeRemoved);
        }
        //it would be set false until these updates are reflected in the stored products
        IsTransactionProcessed = false;

        RecalculateAmount();

        return new Result<List<InventoryTransactionItem>>(Items.ToList());
    }
}
