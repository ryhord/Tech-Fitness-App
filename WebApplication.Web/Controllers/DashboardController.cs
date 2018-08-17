using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.Providers.Auth;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using System.Text;
using Newtonsoft.Json;


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
			string jsonRes = api.makeRequest();


			JsonResponseModel jobj = JsonConvert.DeserializeObject<JsonResponseModel>(jsonRes);

			var brandedResults = jobj.branded;
			var commonResults = jobj.common;

			SearchResults res = new SearchResults();
			

			foreach (var i in brandedResults)
			{
				FoodPreview preview = new FoodPreview();
				preview.Name = i.food_name;
				preview.PhotoUrl = i.photo.thumb;
				res.FoodSearchResults.Add(preview);
			}

			foreach (var i in commonResults)
			{
				FoodPreview preview = new FoodPreview();
				preview.Name = i.food_name;
				preview.PhotoUrl = i.photo.thumb;
				res.FoodSearchResults.Add(preview);
			}

			return View(res);
		}
	}	
}