using System.ComponentModel.DataAnnotations;

namespace AliProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordAgain { get; set; }
    }
}
