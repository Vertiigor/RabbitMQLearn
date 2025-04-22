using RabbitMQ.Client;

namespace RabbitMQLearn.Data.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        public Task<IConnection> GetConnectionAsync();
    }
}
