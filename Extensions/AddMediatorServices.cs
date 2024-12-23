using System.Reflection;
using FluentValidation;

namespace ResortManager.Services.Finance.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        List<Assembly> assemblies = [typeof(Program).Assembly];

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies([.. assemblies]));

        services.AddValidatorsFromAssemblies(
                assemblies,
                filter: x => x.ValidatorType.BaseType?.GetGenericTypeDefinition() != typeof(FastEndpoints.Validator<>) //filter out FE validators
                );

        services.AddValidatorsFromAssemblyContaining<Program>(filter: x => x.ValidatorType.BaseType?.GetGenericTypeDefinition() != typeof(FastEndpoints.Validator<>)); //filter out FE validators

        return services;
    }
}
