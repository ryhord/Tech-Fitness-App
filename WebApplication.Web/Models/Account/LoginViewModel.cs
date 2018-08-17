using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Models.Account
{

    public class LoginViewModel
    {
        [Required]
		[MinLength(2, ErrorMessage = "The username must be at least 2 characters long.")]
		[Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
        public string Password { get; set; }
    }
}
