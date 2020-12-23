using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
