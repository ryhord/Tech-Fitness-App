﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class User
    {
        /// <summary>
        /// The user's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        [MaxLength(50)]
        public string Username { get; set; }

		/// <summary>
		/// The user's email.
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string Email { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// The user's salt.
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// The user's role.
        /// </summary>
        public string Role { get; set; }
    }
}