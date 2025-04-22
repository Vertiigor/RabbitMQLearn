namespace RabbitMQLearn.Producers.Abstractions
{
    public interface IMessageProducer
    {
        public Task PublishMessageAsync<T>(T message);
    }
}
