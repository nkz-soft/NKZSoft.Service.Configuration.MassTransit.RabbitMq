namespace NKZSoft.Service.Configuration.MassTransit.RabbitMq.Configuration;

internal sealed record MassTransitRabbitMqConfigurationSection
{
    public ConnectionStringsConfiguration? ConnectionStrings { get; init; }
}
