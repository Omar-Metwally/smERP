namespace smERP.Domain.Entities.Product;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public virtual Category? ParentCategory { get; set; }
}
