using smERP.Domain.Entities.Product;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Net;

namespace smERP.Domain.Entities.InventoryTransaction;

public class InventoryTransactionItem : Entity
{
    public int TransactionId { get; private set; }
    public int ProductInstanceId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    //public Discount? ItemDiscount { get; private set; }
    public decimal TotalPrice => Quantity * UnitPrice; //ItemDiscount?.Apply(Quantity * UnitPrice) ?? (Quantity * UnitPrice);
    public virtual ProductInstance ProductInstance { get; private set; } = null!;

    private InventoryTransactionItem(int productInstanceId, int quantity, decimal unitPrice)
    {
        ProductInstanceId = productInstanceId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    //public void ApplyDiscount(Discount discount)
    //{
    //    ItemDiscount = discount;
    //}

    public static IResult<InventoryTransactionItem> Create(int productInstanceId, int quantity, decimal unitPrice)
    {
        if (quantity < 0)
            return new Result<InventoryTransactionItem>()
                .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Quantity.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        if (unitPrice < 0)
            return new Result<InventoryTransactionItem>()
                .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Price.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        return new Result<InventoryTransactionItem>(new InventoryTransactionItem(productInstanceId, quantity, unitPrice));
    }

    public IResult<InventoryTransactionItem> Update(decimal? unitPrice, int? quantity)
    {
        if (quantity != null && quantity.HasValue)
        {
            if (quantity < 0)
                return new Result<InventoryTransactionItem>()
                    .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Quantity.Localize()))
                    .WithStatusCode(HttpStatusCode.BadRequest);

            Quantity = quantity.Value;
        }

        if (unitPrice != null && unitPrice.HasValue)
        {
            if (unitPrice < 0)
                return new Result<InventoryTransactionItem>()
                    .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Price.Localize()))
                    .WithStatusCode(HttpStatusCode.BadRequest);

            UnitPrice = unitPrice.Value;
        }

        return new Result<InventoryTransactionItem>(this);
    }
}
