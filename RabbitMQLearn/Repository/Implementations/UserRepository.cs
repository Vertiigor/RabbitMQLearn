using RabbitMQLearn.Data;
using RabbitMQLearn.Models;
using RabbitMQLearn.Repository.Abstractions;

namespace RabbitMQLearn.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
