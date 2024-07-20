namespace smERP.Domain.Entities.Product;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual Category Category { get; set; }
    public ICollection<ProductAttribute> ProductAttributes { get; set; }
    public ICollection<ProductSKU> ProductSKUs { get; set; }
    public ICollection<ProductSupplier>? ProductSuppliers { get; set; }
}
