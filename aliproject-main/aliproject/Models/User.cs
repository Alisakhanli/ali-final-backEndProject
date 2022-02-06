using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AliProject.Models
{

    public class User : IdentityUser
    {

        [Required]
        public string FullName { get; set; }
    }
}
