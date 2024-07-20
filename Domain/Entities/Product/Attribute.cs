namespace smERP.Domain.Entities.Product;

public class Attribute : BaseEntity
{
    public string Name { get; set; }
    public ICollection<AttributeValue> AttributeValues { get; set; }
}
