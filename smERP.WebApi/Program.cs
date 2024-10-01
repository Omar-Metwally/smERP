using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.Application;
using smERP.Persistence;
using System.Globalization;
using Serilog;
using smERP.WebApi.Middleware;
using smERP.Infrastructure;

namespace smERP.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Host.UseSerilog((context, loggerConfigs) =>
        {
            loggerConfigs.ReadFrom.Configuration(context.Configuration);
        });


        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Enrich.With()
            .WriteTo.File("log.txt",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
            .CreateLogger();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthentication();

        builder.Services.AddApplicationDependencies()
                        .AddInfrastructureDependencies(builder.Configuration)
                        .AddPersistenceDependencies(builder.Configuration);

        builder.Services.AddLocalization();
        builder.Services.AddLocalizationExtension();

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ar-EG") };
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        var app = builder.Build();

        app.UseExceptionHandler();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        LocalizationExtension.InitializeLocalizer(app.Services);

        //ResultConfiguration.ResultInitialize(app.Services);

        app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
