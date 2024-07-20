namespace smERP.Domain.Entities.Transactions;

public class TransactionItem : BaseEntity
{
    public int TransactionID { get; set; }
    public int ProductSKUID { get; set; }
    public int Quantity { get; set; }
    public int UsableQuantity { get; set; }
    public int PricePerUnitInCents { get; set; }
    public int? DiscountInCents { get; set; }
    public bool IsCanceled { get; set; }
    public virtual BaseTransaction BaseTransaction { get; set; }
}
