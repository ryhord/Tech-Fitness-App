using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.SignIn(loginViewModel.Email, loginViewModel.Password);
                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Home");
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
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Register them as a new user (and set default role)
                //authProvider.Register(registerViewModel.Email, registerViewModel.Password, "Role");

				//IUSER DAL GETS USED HERE
				//UserSqlDAL dal = new UserSqlDAL(userDAL);
				// @"Data Source=.\SQLEXPRESS;Initial Catalog=HealthTrackDB;Integrated Security=True"
				//User user = new User();
				//user.Email = registerViewModel.Email;
				//user.Password = registerViewModel.Password;
				dal.CreateUser(user);

                // Redirect the user where you want them to go after registering
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}