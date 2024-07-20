namespace smERP.Domain.Entities.Product;

public class AttributeValue : BaseEntity
{
    public int AttributeValueID { get; set; }
    public int AttributeID { get; set; }
    public string Name { get; set; }
    public ICollection<ProductAttribute> ProductAttributes { get; set; }
}
