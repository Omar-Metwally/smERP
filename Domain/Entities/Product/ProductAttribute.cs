namespace smERP.Domain.Entities.Product;

public class ProductAttribute
{
    public int ProductSkuId { get; set; }
    public int ProductId { get; set; }
    public int AttributeValueId { get; set; }
    public virtual ProductSKU ProductSKU { get; set; }
    public virtual Product Product { get; set; }
    public virtual AttributeValue AttributeValue { get; set; }
}
