using RabbitMQLearn.Models;
using RabbitMQLearn.Producers.Abstractions;
using RabbitMQLearn.Repository.Abstractions;
using RabbitMQLearn.Services.Abstractions;

namespace RabbitMQLearn.Services.Implementations
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageProducer _messageProducer;

        public UserService(IUserRepository userRepository, IMessageProducer messageProducer) : base(userRepository)
        {
            _userRepository = userRepository;
            _messageProducer = messageProducer;
        }

        public async Task Publish(User user)
        {
            await _messageProducer.PublishMessageAsync(user);
        }
    }
}
