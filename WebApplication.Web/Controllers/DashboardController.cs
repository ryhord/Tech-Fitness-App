using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;

namespace WebApplication.Web.Controllers
{
    public class DashboardController : Controller
    {

		
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult AddFood()
		{
			return View();
		}

		[HttpPost]
		public IActionResult FoodResults(Food food)
		{
			ApiDAL api = new ApiDAL();
			api.endpoint = "http://dry-cliffs-19849.herokuapp.com";
			food.Name = api.makeRequest();

			return View(food);
		}





	}
}