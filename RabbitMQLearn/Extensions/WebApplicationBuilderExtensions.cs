using Microsoft.EntityFrameworkCore;
using RabbitMQLearn.Data;

namespace RabbitMQLearn.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
