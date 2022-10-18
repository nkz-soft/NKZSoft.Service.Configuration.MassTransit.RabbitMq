namespace NKZSoft.Service.Configuration.MassTransit.RabbitMq.Configuration;

internal sealed record ConnectionStringsConfiguration
{
    public RabbitMqConnectionConfiguration? RabbitMqConnection { get; init; }
}
