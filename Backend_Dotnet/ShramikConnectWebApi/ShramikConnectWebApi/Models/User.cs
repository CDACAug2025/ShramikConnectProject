using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public Worker? Worker { get; set; }
        public Client? Client { get; set; }
        public Organization? Organization { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }


}