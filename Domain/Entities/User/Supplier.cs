using smERP.Domain.Entities.Product;

namespace smERP.Domain.Entities.User;

public class Supplier : BaseUser
{
    public ICollection<ProductSupplier>? SuppliedProducts { get; set; }
}
