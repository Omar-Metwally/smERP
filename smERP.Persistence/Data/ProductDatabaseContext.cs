using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using smERP.Domain.Entities;
using smERP.Domain.Entities.Product;
using smERP.Persistence.Data.Configurations;
using smERP.Persistence.Data.Interceptors;
using Attribute = smERP.Domain.Entities.Product.Attribute;
using smERP.Domain.Entities.ExternalEntities;
using smERP.Domain.Entities.InventoryTransaction;

namespace smERP.Persistence.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new ChangeLogInterceptor());
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    public virtual DbSet<Attribute> Attributes { get; set; }

    //public virtual DbSet<AttributeValue> AttributeValues { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    //public virtual DbSet<DuplicateProductInstance> DuplicateProductInstances { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductInstance> ProductInstances { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<ProcurementTransaction> ProcurementTransactions { get; set; }

    public virtual DbSet<SalesTransaction> SalesTransactions { get; set; }

    public virtual DbSet<AdjustmentTransaction> AdjustmentTransactions { get; set; }



    //public virtual DbSet<ProductInstanceAttribute> ProductInstanceAttributes { get; set; }

    public virtual DbSet<ChangeLog> ChangeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Apply global query filter for soft delete to all entities implementing ISoftDelete
        Expression<Func<ISoftDelete, bool>> filterExpr = e => !e.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            if (mutableEntityType.ClrType.IsAssignableTo(typeof(ISoftDelete)))
            {
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);
                modelBuilder.Entity(mutableEntityType.ClrType).HasQueryFilter(lambdaExpression);
            }
        }

        //modelBuilder.Entity<Attribute>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__ATTRIBUT__9090C9BBA59D0FE3");

        //    entity.ToTable("ATTRIBUTE");

        //    entity.Property(e => e.Id).HasColumnName("attribute_id");
        //    //entity.Ignore(e => e.Name);
        //    //entity.Property(e => e.Name.EnglishName).HasColumnName("english_name");
        //    //entity.Property(e => e.Name.ArabicName).HasColumnName("arabic_name");

        //    entity
        //        .OwnsOne(p => p.Name, w =>
        //        {
        //            w.WithOwner();

        //            w.Property(wt => wt.Arabic);

        //            w.Property(wt => wt.English);
        //        });
        //});

        //modelBuilder.Entity<AttributeValue>(entity =>
        //{
        //    entity.HasKey(e => new { e.AttributeId, e.Id }).HasName("PK_Attribute_AttributeValue");

        //    entity.ToTable("ATTRIBUTE_VALUE");

        //    //entity.HasIndex(e => new { e.AttributeId, e.Value.EnglishName, e.Value.ArabicName }, "UQ_AttributeValue").IsUnique();

        //    entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
        //    entity.Property(e => e.Id)
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("attribute_value_id");

        //    entity
        //        .OwnsOne(p => p.Value, w =>
        //        {
        //            w.WithOwner();

        //            w.Property(wt => wt.Arabic);

        //            w.Property(wt => wt.English);
        //        });

        //    entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeValues)
        //        .HasForeignKey(d => d.AttributeId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_AttributeValue_Attribute");
        //});

        //modelBuilder.Entity<Brand>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__BRAND__5E5A8E278C0B3942");

        //    entity.ToTable("BRAND");

        //    entity.Property(e => e.Id).HasColumnName("brand_id");

        //    entity
        //        .OwnsOne(p => p.Name, w =>
        //        {
        //            w.WithOwner();

        //            w.Property(wt => wt.Arabic);

        //            w.Property(wt => wt.English);
        //        });
        //});

        ////modelBuilder.Entity<Category>(entity =>
        ////{
        ////    entity.HasKey(e => e.Id).HasName("PK__CATEGORY__D54EE9B41D18D948");

        ////    entity.ToTable("CATEGORY");

        ////    entity.Property(e => e.Id).HasColumnName("category_id");
        ////    entity
        ////        .OwnsOne(p => p.Name, w =>
        ////        {
        ////            w.WithOwner();

        ////            w.Property(wt => wt.Arabic);

        ////            w.Property(wt => wt.English);
        ////        });

        ////    entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

        ////    entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
        ////        .HasForeignKey(d => d.ParentCategoryId)
        ////        .HasConstraintName("FK_Category_Parent");
        ////});

        //modelBuilder.Entity<DuplicateProductInstance>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToView("DuplicateProductInstances");

        //    entity.Property(e => e.InstanceCount).HasColumnName("instance_count");
        //    entity.Property(e => e.ProductId).HasColumnName("product_id");
        //    entity.Property(e => e.Sku)
        //        .HasMaxLength(255)
        //        .HasColumnName("SKU");
        //});

        //modelBuilder.Entity<Product>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__PRODUCT__47027DF55174BF11");

        //    entity.ToTable("PRODUCT");

        //    entity.HasIndex(e => new { e.ModelNumber, e.BrandId }, "UQ_Product_ModelBrand").IsUnique();

        //    entity.Property(e => e.Id).HasColumnName("product_id");
        //    entity.Property(e => e.BrandId).HasColumnName("brand_id");
        //    entity.Property(e => e.CategoryId).HasColumnName("category_id");
        //    entity.Property(e => e.Description).HasColumnName("description");
        //    entity.Property(e => e.ModelNumber)
        //        .HasMaxLength(100)
        //        .HasColumnName("model_number");

        //    entity
        //        .OwnsOne(p => p.Name, w =>
        //        {
        //            w.WithOwner();

        //            w.Property(wt => wt.Arabic);

        //            w.Property(wt => wt.English);
        //        });

        //    entity.HasOne(d => d.Brand).WithMany(p => p.Products)
        //        .HasForeignKey(d => d.BrandId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_Product_Brand");

        //    entity.HasOne(d => d.Category).WithMany(p => p.Products)
        //        .HasForeignKey(d => d.CategoryId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_Product_Category");
        //});

        //modelBuilder.Entity<ProductInstance>(entity =>
        //{
        //    entity.HasKey(e => e.ProductInstanceId).HasName("PK__PRODUCT___A5F21395EFA3FF0C");

        //    entity.ToTable("PRODUCT_INSTANCE");

        //    entity.HasIndex(e => e.Sku, "UQ_ProductInstance_SKU").IsUnique();

        //    entity.Property(e => e.ProductInstanceId).HasColumnName("product_instance_id");
        //    entity.Property(e => e.Price)
        //        .HasColumnType("decimal(10, 2)")
        //        .HasColumnName("price");
        //    entity.Property(e => e.ProductId).HasColumnName("product_id");
        //    entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");
        //    entity.Property(e => e.Sku)
        //        .HasMaxLength(255)
        //        .HasColumnName("SKU");

        //    entity.HasOne(d => d.Product).WithMany(p => p.ProductInstances)
        //        .HasForeignKey(d => d.ProductId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ProductInstance_Product");
        //});

        //modelBuilder.Entity<ProductInstanceAttribute>(entity =>
        //{
        //    entity.HasKey(e => new { e.ProductInstanceId, e.AttributeId }).HasName("PK_ProductInstanceAttribute");

        //    entity.ToTable("PRODUCT_INSTANCE_ATTRIBUTE", tb => tb.HasTrigger("TR_GenerateSKU"));

        //    entity.Property(e => e.ProductInstanceId).HasColumnName("product_instance_id");
        //    entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
        //    entity.Property(e => e.AttributeValueId).HasColumnName("attribute_value_id");

        //    entity.HasOne(d => d.ProductInstance).WithMany(p => p.ProductInstanceAttributes)
        //        .HasForeignKey(d => d.ProductInstanceId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ProductInstanceAttribute_ProductInstance");

        //    entity.HasOne(d => d.AttributeValue).WithMany(p => p.ProductInstanceAttributes)
        //        .HasForeignKey(d => new { d.AttributeId, d.AttributeValueId })
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ProductInstanceAttribute_AttributeValue");
        //});

        //var userRole = "user";//_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        //if (userRole != "Admin")
        //{
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        if (typeof(ICommonEntitiesAttributes).IsAssignableFrom(entityType.ClrType))
        //        {
        //            var parameter = Expression.Parameter(entityType.ClrType, "e");
        //            var sensitiveProperties = new[] { nameof(ICommonEntitiesAttributes.CreatedAt), nameof(ICommonEntitiesAttributes.UpdatedAt) };

        //            var trueExpression = Expression.Constant(true);
        //            var filterExpression = trueExpression;

        //            foreach (var propertyName in sensitiveProperties)
        //            {
        //                var property = Expression.Property(parameter, propertyName);
        //                var nullValue = Expression.Constant(null, property.Type);
        //                var isNullExpression = Expression.Equal(property, nullValue);
        //                filterExpression = Expression.AndAlso(filterExpression, isNullExpression);
        //            }

        //            var lambda = Expression.Lambda(filterExpression, parameter);
        //            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        //        }
        //    }
        //}




        //    var isAdmin = false;

        //    Expression<Func<ICommonEntitiesAttributes, bool>> filterExpr1 = e => isAdmin;

        //    foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        if (typeof(ICommonEntitiesAttributes).IsAssignableFrom(mutableEntityType.ClrType))
        //        {
        //            var parameter = Expression.Parameter(mutableEntityType.ClrType);
        //            var body = ReplacingExpressionVisitor.Replace(filterExpr1.Parameters.First(), parameter, filterExpr1.Body);


        //            //Introduce a conditional check to clear properties if isAdmin is false
        //            //var conditionalBody = Expression.Condition(
        //            //    Expression.Constant(isAdmin),
        //            //    body,
        //            //    Expression.MemberInit(
        //            //        Expression.New(mutableEntityType.ClrType),
        //            //        new MemberBinding[]
        //            //        {
        //            //    Expression.Bind(typeof(ICommonEntitiesAttributes).GetProperty("CreatedAt"), Expression.Default(typeof(DateTime))),
        //            //    Expression.Bind(typeof(ICommonEntitiesAttributes).GetProperty("CreatedBy"), Expression.Constant(0)),
        //            //    Expression.Bind(typeof(ICommonEntitiesAttributes).GetProperty("UpdatedAt"), Expression.Default(typeof(DateTime))),
        //            //    Expression.Bind(typeof(ICommonEntitiesAttributes).GetProperty("UpdatedBy"), Expression.Constant(0))
        //            //        }
        //            //    )
        //            //);

        //            var conditionalBody = Expression.Condition(
        //                Expression.Constant(isAdmin),
        //                body, // Keep the original filter logic for admins
        //                Expression.MemberInit( // Create a new instance and set properties to default values for non-admins
        //                    Expression.New(mutableEntityType.ClrType),
        //                    GetDefaultPropertyBindings(typeof(ICommonEntitiesAttributes))
        //                )
        //            );

        //            var lambdaExpression = Expression.Lambda(conditionalBody, parameter);
        //            modelBuilder.Entity(mutableEntityType.ClrType).HasQueryFilter(lambdaExpression);
        //        }
        //    }
        //}

        //private static IEnumerable<MemberBinding> GetDefaultPropertyBindings(Type interfaceType)
        //{
        //    var properties = interfaceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (var propertyInfo in properties)
        //    {
        //        yield return Expression.Bind(
        //            propertyInfo,
        //            Expression.Default(propertyInfo.PropertyType)
        //        );
        //    }
        //}
    }
}
