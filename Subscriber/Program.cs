using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "guest", Password = "guest" };

            IConnection? connection = null;

            int retries = 20;
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    connection = factory.CreateConnection();
                    Console.WriteLine("Connected to RabbitMQ!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {i + 1}/{retries} failed: {ex.Message}");
                    Thread.Sleep(3000); // Wait 3 seconds before retrying
                }
            }

            if (connection == null)
            {
                Console.WriteLine("Could not connect to RabbitMQ. Exiting...");
                return;
            }

            using (connection)
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "users",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "users",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
