using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smERP.Domain.Entities.InventoryTransaction;

namespace smERP.Persistence.Data.Configurations.InventoryTransactionConfigurations;

public class ProcurementTransactionConfiguration : IEntityTypeConfiguration<ProcurementTransaction>
{
    public void Configure(EntityTypeBuilder<ProcurementTransaction> builder)
    {
        builder.Ignore(x => x.ExternalEntityId);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Supplier).WithMany().HasForeignKey(x => x.SupplierId);

        builder.Property(x => x.TransactionDate).HasDefaultValue(DateTime.UtcNow);

        builder.OwnsMany(x => x.Items, w =>
        {
            w.WithOwner().HasForeignKey(x => x.TransactionId);
            w.HasKey(x => new { x.TransactionId, x.Id });
        });

        builder.OwnsMany(x => x.Payments, w =>
        {
            w.WithOwner().HasForeignKey(x => x.TransactionId);
            w.HasKey(x => new { x.TransactionId, x.Id });
            w.Property(x => x.PaymentDate).HasDefaultValue(DateTime.UtcNow);
        });
    }
}
