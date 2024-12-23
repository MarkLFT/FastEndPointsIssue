using Microsoft.EntityFrameworkCore;
using RM.Data.Svc.Finance;


namespace ResortManager.Services.Finance.Extensions;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        var connectionStringLive = $"Data Source={config["SQLServer"]};Initial Catalog={config["LiveDatabaseName"]};User ID=GRManager;Password={config["SQLPassword"]};MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true";


        services.AddDbContext<IFinanceDbContext, FinanceDbContext>(options =>
        {
            options.UseSqlServer(connectionStringLive, opts =>
            {
                opts.EnableRetryOnFailure();
                opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: environment.IsDevelopment())
            .EnableDetailedErrors(environment.IsDevelopment());
        });

        return services;
    }
}