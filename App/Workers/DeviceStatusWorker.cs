namespace IoTProject.App.Workers;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class DeviceStatusWorker : BackgroundService
    {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new MqttClientFactory();

        var client = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
        .WithTcpServer("localhost", 1883)
        .Build();

        client.ApplicationMessageReceivedAsync += e =>
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = e.ApplicationMessage.ConvertPayloadToString();

            return Task.CompletedTask;
        };

        await client.ConnectAsync(
            options,
            stoppingToken);

        var subscribeOptions =
        new MqttClientSubscribeOptionsBuilder()
        .WithTopicFilter("devices/telemetry")
        .Build();

        await client.SubscribeAsync(
            subscribeOptions,
            stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(
                1000,
                stoppingToken);
        }
    }
}

