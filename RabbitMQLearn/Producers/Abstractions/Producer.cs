using RabbitMQ.Client;
using RabbitMQLearn.Data.RabbitMQ.Connection;
using System.Text;
using System.Text.Json;

namespace RabbitMQLearn.Producers.Abstractions
{
    public class Producer : IMessageProducer
    {
        private readonly IRabbitMqConnection _connection;

        public Producer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishMessageAsync<T>(T message)
        {
            var connection = await _connection.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "users",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: "",
                routingKey: "users",
                body: body);
        }
    }
}
