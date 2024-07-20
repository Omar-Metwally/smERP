namespace smERP.Domain.Entities.Product;

public class AttributeValue : BaseEntity
{
    public int AttributeValueId { get; set; }
    public int AttributeId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductAttribute> ProductAttributes { get; set; }
}
