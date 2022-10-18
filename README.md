# NKZSoft.Service.Configuration.MassTransit.RabbitMq

[![Nuget](https://img.shields.io/nuget/v/NKZSoft.Service.Configuration.MassTransit.RabbitMq?style=plastic)](https://www.nuget.org/packages/NKZSoft.Service.Configuration.MassTransit.RabbitMq/)

Provides the configuration for MassTransit in a microservices architecture

## Using
```csharp
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configuration, AddConsumers, AddReceiveEndpoints);
        return services;
    }

    private static void AddConsumers(IBusRegistrationConfigurator configurator)
    {
    }

    private static void AddReceiveEndpoints(IRabbitMqBusFactoryConfigurator busFactoryConfigurator, IRegistrationContext context)
    {
    }
```

### JSON appsettings.json configuration

```json
{
    "ConnectionStrings": {
        "RabbitMqConnection": {
            "ConnectionString": "rabbitmq://rabbitmq:rabbitmq@localhost:5672"
        }
    }
}
```
