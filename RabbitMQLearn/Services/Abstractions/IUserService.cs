using RabbitMQLearn.Models;

namespace RabbitMQLearn.Services.Abstractions
{
    public interface IUserService : IService<User>
    {
        public Task Publish(User user);
    }
}
