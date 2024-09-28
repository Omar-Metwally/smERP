using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;
using smERP.Persistence.Repositories;

namespace smERP.Persistence;

public static class PersistenceDependencies
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
          b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddDbContext<ProductDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("ProductConnection"),
          b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)));

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<ICompanyRepository, CompanyRepository>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<IBrandRepository, BrandRepository>()
                .AddTransient<IAttributeRepository, AttributeRepository>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<ISupplierRepository, SupplierRepository>();
        return services;
    }
}
