using System.Reflection;
using Common.Service;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.RabbitMq;

public static class DependencyInjection
{
    public static IServiceCollection AddMassTransitWithRabbitMq (this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMicroservice (configuration);

        services.AddOptions<Options.RabbitMq> ()
                .Bind (configuration.GetSection (nameof (RabbitMq)))
                .ValidateDataAnnotations ();

        services.AddMassTransit (configurator =>
        {
            configurator.AddConsumers (Assembly.GetEntryAssembly ());

            configurator.UsingRabbitMq ((context, factoryConfigurator) =>
            {
                var service  = context.GetRequiredService<IOptions<Service.Options.Service>> ().Value;
                var rabbitMq = context.GetRequiredService<IOptions<Options.RabbitMq>> ().Value;

                factoryConfigurator.Host (rabbitMq.Host);
                factoryConfigurator.ConfigureEndpoints (context, new KebabCaseEndpointNameFormatter (service.Name, false));
                factoryConfigurator.UseMessageRetry(retryConfigurator =>
                {
                    retryConfigurator.Interval (3, TimeSpan.FromSeconds (5));
                });
            });
        }).AddMassTransitHostedService ();

        return services;
    }
}
