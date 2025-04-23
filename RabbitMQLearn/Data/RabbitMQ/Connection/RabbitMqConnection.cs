using RabbitMQ.Client;

namespace RabbitMQLearn.Data.RabbitMQ.Connection
{
    public class RabbitMqConnection : IRabbitMqConnection, IDisposable
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
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            return await factory.CreateConnectionAsync();
        }

        public Task<IConnection> GetConnectionAsync() => _connectionTask;

        public void Dispose()
        {
            if (_connectionTask.IsCompletedSuccessfully)
            {
                var connection = _connectionTask.Result;
                if (connection != null && connection.IsOpen)
                {
                    connection.Dispose();
                }
            }
        }
    }
}
