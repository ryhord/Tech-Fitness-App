using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.DAL;

namespace WebApplication.Web.Models.Account
{
    public class RegisterViewModel
    {

		/// <summary>
		/// The user's username.
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string Username { get; set; }

		[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

		/// <summary>
		/// The user's firt name.
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		/// <summary>
		/// The user's last name.
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		/// <summary>
		/// The user's birth date.
		/// </summary>
		[Required]
        [DataType(DataType.DateTime, ErrorMessage = "A valid Date or Date and Time must be entered")]
        public DateTime BirthDate { get; set; }
	}
}
