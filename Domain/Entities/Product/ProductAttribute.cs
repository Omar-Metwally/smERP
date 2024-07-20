namespace smERP.Domain.Entities.Product;

public class ProductAttribute
{
    public int ProductSKUID { get; set; }
    public int ProductID { get; set; }
    public int AttributeValueID { get; set; }
    public virtual ProductSKU ProductSKU { get; set; }
    public virtual Product Product { get; set; }
    public virtual AttributeValue AttributeValue { get; set; }
}
