
using Consumer.Data.RabbitMQ.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer.Services
{
    public class RabbitMqListener : BackgroundService
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;

        public RabbitMqListener(IRabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var c = await _rabbitMqConnection.GetConnectionAsync();
                Console.WriteLine("✅ Connected to RabbitMQ successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to connect: {ex.Message}");
            }


            var connection = await _rabbitMqConnection.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "users",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
                // Acknowledge the message
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queue: "users",
                autoAck: false,
                consumer: consumer);
        }
    }
}
