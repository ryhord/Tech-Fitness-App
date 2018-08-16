using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{    
    public class AccountController : Controller
    {
        private readonly IAuthProvider authProvider;
		private readonly IUserDAL dal;
		public AccountController(IAuthProvider authProvider, IUserDAL dal)
        {
            this.authProvider = authProvider;
			this.dal = dal;
		}
        
        [HttpGet]
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var existingUser = dal.GetUser(loginViewModel.Username);
			if (existingUser == null)
			{
				ModelState.AddModelError("username-nonexistent", "An account is not registered to this username.");
			}

			// Ensure the fields were filled out
			if (ModelState.IsValid)
            {
				// Check that they provided correct credentials
				bool validLogin = authProvider.SignIn(loginViewModel.Username, loginViewModel.Password);
                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Dashboard");
                }
				else
				{
					ModelState.AddModelError("Password", "Invalid Credentials");
					return View(loginViewModel);
				}
            }

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            // Clear user from session
            authProvider.LogOff();

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel rvm)
        {
			var existingUser = dal.GetUser(rvm.Username);
			if (existingUser != null)
			{
				ModelState.AddModelError("username-taken", "An account is already registered to this username.");				
			}

			var existingEmail = dal.GetEmail(rvm.Email);
			if (existingUser != null)
			{
				ModelState.AddModelError("email-taken", "An account is already registered to this email.");
			}

			if (ModelState.IsValid)
            {
                // Register them as a new user (and set default role)
                authProvider.Register(rvm);

                // Redirect the user where you want them to go after registering
                return RedirectToAction("Index", "Dashboard");
            }

            return View(rvm);
        }

		public IActionResult ViewProfile()
		{
			User user = authProvider.GetCurrentUser();
			return View(user);
		}

		public IActionResult EditProfile()
		{
			User user = authProvider.GetCurrentUser();
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SaveChanges(User updatedUser)
		{
			User user = authProvider.GetCurrentUser();
			user.FirstName = updatedUser.FirstName;
			user.LastName = updatedUser.LastName;
			user.BirthDate = updatedUser.BirthDate;
			//user.Age = updatedUser.Age;
			user.Height = updatedUser.Height;
			user.CurrentWeight = updatedUser.CurrentWeight;
			user.DesiredWeight = updatedUser.DesiredWeight;
			user.RecommendedDailyCaloricIntake = updatedUser.RecommendedDailyCaloricIntake;
			dal.UpdateUser(user);

			return RedirectToAction("ViewProfile", "Account");
		}


	}
}