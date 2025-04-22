using RabbitMQ.Client;

namespace RabbitMQLearn.Data.RabbitMQ.Connection
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly Task<IConnection> _connectionTask;

        public RabbitMqConnection()
        {
            _connectionTask = InitializeConnectionAsync();
        }

        private async Task<IConnection> InitializeConnectionAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            return await factory.CreateConnectionAsync();
        }

        public Task<IConnection> GetConnectionAsync() => _connectionTask;
    }
}
