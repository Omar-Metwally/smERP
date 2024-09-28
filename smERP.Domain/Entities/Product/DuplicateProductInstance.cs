using System;
using System.Collections.Generic;

namespace smERP.Domain.Entities.Product;

public partial class DuplicateProductInstance
{
    public int ProductId { get; set; }

    public string? Sku { get; set; }

    public int? InstanceCount { get; set; }
}
