namespace smERP.Domain.Entities.Product;

public class ProductSKU : BaseEntity
{
    public string ProductSKUNumber { get; set; }
    public int ProductVariantID { get; set; }
    public int ProductID { get; set; }
    public int QuantityInStock { get; set; }
    public int Price { get; set; }
    public int? DiscountedPrice { get; set; }
    public string? MainImage { get; set; }
    public virtual Product Product { get; set; }
    public ICollection<ProductAttribute> ProductAttributes { get; set; }
    public ICollection<ProductSKUImage>? ProductImages { get; set; }

}
