using smERP.Domain.ValueObjects;
using smERP.SharedKernel.Responses;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace smERP.Domain.Entities.InventoryTransaction;

[NotMapped]
public abstract class InventoryTransaction : Entity
{
    public DateTime TransactionDate { get; protected set; }
    public decimal TotalAmount { get; protected set; }
    public bool IsCanceled { get; protected set; } = false;
    //public Discount? TransactionDiscount { get; protected set; }
    public ICollection<InventoryTransactionItem> Items { get; protected set; } = new List<InventoryTransactionItem>();

    protected InventoryTransaction(DateTime transactionDate, ICollection<InventoryTransactionItem> items)
    {
        TransactionDate = transactionDate;
        Items = items;
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

    protected virtual void RecalculateAmount()
    {
        decimal subtotal = Items.Sum(item => item.TotalPrice);
        TotalAmount = subtotal;
        //Amount = TransactionDiscount?.Apply(subtotal) ?? subtotal;
    }

    protected static IResult<List<InventoryTransactionItem>> CreateBaseDetails(List<(int ProductInstanceId, int Quantity, decimal? UnitPrice)> transactionItems)
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
}
