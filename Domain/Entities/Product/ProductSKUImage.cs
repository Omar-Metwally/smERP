namespace smERP.Domain.Entities.Product;

public class ProductSKUImage : BaseEntity
{
    public int ProductSkuId { get; set; }
    public string ImagePath { get; set; }
    public virtual ProductSKU ProductSKU { get; set; }
}
