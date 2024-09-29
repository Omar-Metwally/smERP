using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smERP.Domain.Entities.Organization;

namespace smERP.Persistence.Data.Configurations.OrganizationConfigurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.StorageLocations).WithOne(x => x.Branch).HasForeignKey(x => x.BranchId);

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Arabic).IsRequired();
            name.Property(n => n.English).IsRequired();
            name.HasIndex(n => n.Arabic).IsClustered(false);
            name.HasIndex(n => n.English).IsClustered(false);
        });
    }
}
