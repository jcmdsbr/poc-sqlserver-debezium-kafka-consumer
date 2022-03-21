using System.Text.Json;
using Confluent.Kafka;

namespace Poc.Debezium.Kafka.Consumer;

public class Worker : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, IConsumer<string, string> consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        while (!stoppingToken.IsCancellationRequested) HandleMessage(stoppingToken);

    }

    private void HandleMessage(CancellationToken stoppingToken)
    {
        try
        {
            var cr = _consumer.Consume(stoppingToken);

            ArgumentNullException.ThrowIfNull(cr);

            var message = JsonSerializer.Deserialize<Message>(cr.Message.Value);

            if (message is null || message.Payload.After is null && message.Payload.Before is null) return;

            _logger.LogInformation(message.Payload.Before is null
                ? $"Register created - ID: ${message.Payload.After?.Id}"
                : $"Register changed - ID: ${message.Payload.After?.Id}");

        }
        catch (Exception e)
        {
           _logger.LogError(e.Message);
            throw;
        }
    }

    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}