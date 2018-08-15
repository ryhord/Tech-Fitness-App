using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{
    public class DashboardController : Controller
    {
		private readonly IAuthProvider authProvider;
		public DashboardController(IAuthProvider authProvider)
		{
			this.authProvider = authProvider;
		}

		public IActionResult Index()
        {
			var user = authProvider.GetCurrentUser();
			if (user != null)
			{ 
				return View(user);
			}

			return RedirectToAction("Index", "Home");
		}
	}	
}