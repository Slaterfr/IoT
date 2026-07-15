namespace IoTProject.App.Workers;

using IoTProject.App.DTOs;
using IoTProject.App.Interfaces;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class MqttWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
    public MqttWorker (IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new MqttClientFactory();

        var client = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
        .WithClientId("TelemetryWorker")
        .WithTcpServer("192.168.100.206", 1883)
        .Build();

        client.ApplicationMessageReceivedAsync += async e =>
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = e.ApplicationMessage.ConvertPayloadToString();
            var dto = JsonSerializer.Deserialize<TelemetryEntry>(payload);

            using var scope = _scopeFactory.CreateScope();
            var telemetryService = scope.ServiceProvider.GetRequiredService<ITelemetryService>();
            Console.WriteLine("==========");
            Console.WriteLine($"Topic: {e.ApplicationMessage.Topic}");
            Console.WriteLine($"Retain: {e.ApplicationMessage.Retain}");
            Console.WriteLine($"QoS: {e.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"Payload: {e.ApplicationMessage.ConvertPayloadToString()}");
            Console.WriteLine(dto);
            
            await telemetryService.PostTelemetry(dto);
        };

        await client.ConnectAsync(
            options,
            stoppingToken);
        Console.WriteLine($"MQTT conectado: {client.IsConnected}");

        var subscribeOptions =
        new MqttClientSubscribeOptionsBuilder()
        .WithTopicFilter("devices/telemetry")
        .Build();

        await client.SubscribeAsync(
            subscribeOptions,
            stoppingToken);
        Console.WriteLine("Suscripción completada");

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"Worker activo - MQTT: {client.IsConnected}");
            await Task.Delay(5000, stoppingToken);
        }

        Console.WriteLine("Worker detenido");
    }
}

