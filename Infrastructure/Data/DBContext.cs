using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smERP.Domain.Entities.Product;
using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.Transactions;
using smERP.Domain.Entities.User;

namespace smERP.Infrastructure.Data;

public class DBContext : IdentityDbContext<BaseUser>
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BaseTransaction>().UseTptMappingStrategy();

        builder.Entity<BaseUser>().UseTptMappingStrategy();

        builder.Entity<ProductAttribute>()
            .HasKey(e => new { e.ProductSkuId, e.AttributeValueId });

        builder.Entity<ProductSKU>()
            .HasKey(e => e.Id);

        builder.Entity<ProductSKU>()
            .HasIndex(e => e.ProductSKUNumber)
            .IsUnique();

        builder.Entity<ProductSKU>()
            .HasMany(e => e.ProductAttributes)
            .WithOne(e => e.ProductSKU)
            .HasForeignKey(e => e.ProductSkuId);

        builder.Entity<ProductSKU>()
            .HasOne(p => p.Product)
            .WithMany(p => p.ProductSKUs)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ProductSKU>()
            .HasMany(e => e.ProductImages)
            .WithOne(e => e.ProductSKU)
            .HasForeignKey(e => e.ProductSkuId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ProductSKUImage>()
        .HasKey(e => e.Id);

        builder.Entity<Department>()
            .HasMany(e => e.DepartmentEmployees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);

        builder.Entity<Department>()
            .HasOne(e => e.DepartmentHead)
            .WithMany()
            .HasForeignKey(e => e.DepartmentHeadId);

        builder.Entity<Branch>()
            .HasMany(e => e.BranchEmployees)
            .WithOne(e => e.Branch)
            .HasForeignKey(e => e.BranchId);

        builder.Entity<Branch>()
            .HasOne(e => e.BranchManager)
            .WithMany()
            .HasForeignKey(e => e.BranchManagerId);

        builder.Entity<BaseTransaction>()
            .HasMany(e => e.TransactionPayments)
            .WithOne(e => e.ReferencingTransaction)
            .HasForeignKey(e => e.ReferencingTransactionId);

        builder.Entity<ProductSupplier>()
            .HasKey(e => new { e.ProductId, e.SupplierId });

        builder.Entity<BaseTransaction>()
            .HasKey(e => e.Id);

        builder.Entity<TransactionDocument>()
            .HasKey(e => new {e.TransactionId, e.SensitiveDocumentId});

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSKU> ProductSKUs { get; set; }
    public DbSet<Domain.Entities.Product.Attribute> Attributes { get; set; }
    public DbSet<AttributeValue> AttributeValues { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductSupplier> ProductSuppliers { get; set; }

    public DbSet<Company> Company { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public DbSet<BaseTransaction> BaseTransactions { get; set; }
    public DbSet<TransactionChange> TransactionChanges { get; set; }
    public DbSet<TransactionItem> TransactionItems { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<BuyTransaction> BuyTransactions { get; set; }
    public DbSet<SellTransaction> SellTransactions { get; set; }
    public DbSet<ProductMoveTransaction> ProductMoveTransactions { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Branch> Branches { get; set; }

    public DbSet<TransactionDocument> TransactionDocuments { get; set; }
    public DbSet<SensitiveDocument> SensitiveDocuments { get; set; }



}
