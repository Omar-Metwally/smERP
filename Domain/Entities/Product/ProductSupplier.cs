using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Product;

public class ProductSupplier
{
    public int ProductId { get; set; }
    public string SupplierId { get; set; }
    public virtual Product Product { get; set; }
    public virtual Supplier Supplier { get; set; }
    public DateOnly FirstTimeSupplied { get; set; }
    public DateOnly LastTimeSupplied { get; set; }
}
