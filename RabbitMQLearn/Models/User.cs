using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RabbitMQLearn.Models
{
    public class User : IdentityUser
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
