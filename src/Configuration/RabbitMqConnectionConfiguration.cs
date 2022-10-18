namespace NKZSoft.Service.Configuration.MassTransit.RabbitMq.Configuration;

internal sealed record RabbitMqConnectionConfiguration
{
    public string? ConnectionString { get; init; }
}
