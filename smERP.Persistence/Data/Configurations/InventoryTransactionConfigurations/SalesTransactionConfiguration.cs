﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smERP.Domain.Entities.InventoryTransaction;

namespace smERP.Persistence.Data.Configurations.InventoryTransactionConfigurations;

public class SalesTransactionConfiguration : IEntityTypeConfiguration<SalesTransaction>
{
    public void Configure(EntityTypeBuilder<SalesTransaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TransactionDate).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(x => x.StorageLocation).WithMany(x => x.SalesTransactions).HasForeignKey(x => x.StorageLocationId);

        builder.OwnsMany(x => x.Items, w =>
        {
            w.WithOwner().HasForeignKey(x => x.TransactionId);
            w.HasKey(x => new { x.TransactionId, x.Id });
        });

        builder.OwnsMany(x => x.Payments, w =>
        {
            w.WithOwner().HasForeignKey(x => x.TransactionId);
            w.HasKey(x => new { x.TransactionId, x.Id });
            w.Property(x => x.PaymentDate).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
