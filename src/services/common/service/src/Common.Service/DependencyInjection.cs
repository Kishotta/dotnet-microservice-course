using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Service;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureMicroservice (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<Options.Service> ()
                .Bind (configuration.GetSection (nameof (Options.Service)))
                .ValidateDataAnnotations ();

        return services;
    }
}
