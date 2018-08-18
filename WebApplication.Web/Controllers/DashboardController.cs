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
		public IActionResult SearchForFood()
		{
			return View();
		}

		[HttpGet]
		public IActionResult FoodResults(FoodPreview foodSearch)
		{
			ApiDAL api = new ApiDAL();
			api.endpoint = "https://trackapi.nutritionix.com/v2/search/instant?query=" + foodSearch.Name;
			string jsonRes = api.searchForFood();


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

		[HttpGet]
		public IActionResult ViewFoodDetail(string name, string imgurl)
		{
			//FoodPreview foodPreview = new FoodPreview();
			
			ApiDAL api = new ApiDAL();
			api.endpoint = "https://trackapi.nutritionix.com/v2/natural/nutrients/";
			string jsonNutrition = api.getNutritionInfo(name);
			FoodItem foodItem = JsonConvert.DeserializeObject<FoodItem>(jsonNutrition);
			foodItem.foods[0].Name = name;
			foodItem.foods[0].Imgurl = imgurl;
			return View(foodItem);
		}

		//[HttpPost]
		//public IActionResult 
	}	
}