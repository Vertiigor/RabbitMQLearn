using RabbitMQ.Client;

namespace Consumer.Data.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        public Task<IConnection> GetConnectionAsync();
    }
}
