namespace smERP.Domain.Entities.Product;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public int? ParentCategoryID { get; set; }
    public virtual Category? ParentCategory { get; set; }
}
