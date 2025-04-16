using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace S6_ASPSEC_04.Models
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }  // bv. "Admin" of "Gebruiker"

    }
}
