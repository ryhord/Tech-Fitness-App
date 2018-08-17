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
		public IActionResult FoodResults(FoodPreview foodSearch)
		{
			ApiDAL api = new ApiDAL();
			api.endpoint = "https://trackapi.nutritionix.com/v2/search/instant?query=" + foodSearch.Name;
			foodSearch.Name = api.makeRequest();

			string jsonResponse = api.makeRequest();
			deserializedProduct = JsonConvert.DeserializeObject<Product>(output);

			SearchResults res = new SearchResults();
			res.FoodSearchResults.Add(foodSearch);
			int listLength = res.FoodSearchResults.Count;

			return View(res);
		}

	}
}