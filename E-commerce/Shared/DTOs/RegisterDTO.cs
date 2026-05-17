using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class RegisterDTO
    {

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; init; }


        [Required(ErrorMessage = "Password is required")]

        public string Password { get; init; }
        public string? PhoneNumber { get; init; }


        [Required(ErrorMessage = "UserName is required")]

        public string UserName { get; init;}

        [Required(ErrorMessage = "DisplayName is required")]

        public string DisplayName { get; init; }


    }
}
