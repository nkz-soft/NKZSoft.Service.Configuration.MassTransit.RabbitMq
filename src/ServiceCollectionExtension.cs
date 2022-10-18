namespace NKZSoft.Service.Configuration.MassTransit.RabbitMq;

using Configuration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configureConsumersAction = null,
        Action<IRabbitMqBusFactoryConfigurator, IRegistrationContext>? configureEndpointsAction = null)
    {
        var massTransitRabbitMqConfiguration = configuration.Get<MassTransitRabbitMqConfigurationSection>();

        if (massTransitRabbitMqConfiguration == null)
        {
            return services;
        }

        services.AddMassTransit(x =>
        {
            configureConsumersAction?.Invoke(x);

            x.UsingRabbitMq((registrationContext, factoryConfigurator) =>
            {
                ConfigureRabbitMqHost(factoryConfigurator, massTransitRabbitMqConfiguration);
                configureEndpointsAction?.Invoke(factoryConfigurator, registrationContext);
            });
        });
        return services;
    }

    private static void ConfigureRabbitMqHost(this IRabbitMqBusFactoryConfigurator configurator,
        MassTransitRabbitMqConfigurationSection configuration)
    {
        var connectionString = configuration.ConnectionStrings?.RabbitMqConnection?.ConnectionString;

        if (!string.IsNullOrEmpty(connectionString))
        {
            var regex = new Regex(@"(?:rabbitmq|amqp):\/\/(\w*):(\w*)@(\w*):(\d*)");
            var matches = regex.Matches(connectionString);

            if (matches.Any() && matches.First().Groups.Count == 5)
            {
                var groups = matches.First().Groups;

                configurator.Host(new Uri($"rabbitmq://{groups[3].Value}:{groups[4].Value}"), hostConfig =>
                {
                    hostConfig.Username(groups[1].Value);
                    hostConfig.Password(groups[2].Value);
                });
                return;
            }
        }
        throw new ArgumentException($"The RabbitMq connection string {connectionString} is invalid.");
    }
}
