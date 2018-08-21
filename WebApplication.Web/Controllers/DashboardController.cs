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
		private readonly IUserFoodDAL dal;
		public DashboardController(IAuthProvider authProvider, IUserFoodDAL dal)
		{
			this.authProvider = authProvider;
			this.dal = dal;
		}

		public IActionResult Index()
		{
				var user = authProvider.GetCurrentUser();
				var userFoods = dal.GetUserFoods(user.Id);
				if (user != null)
				{
					Tuple<User, IList<UserFood>> data = new Tuple<User, IList<UserFood>>(user, userFoods);
					return View(data);
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
			string jsonRes = api.searchForFood(foodSearch.Name);


			JsonResponseModel jsonObj = JsonConvert.DeserializeObject<JsonResponseModel>(jsonRes);

			var brandedResults = jsonObj.branded;
			var commonResults = jsonObj.common;

			SearchResults res = new SearchResults();


			foreach (var i in brandedResults)
			{
				FoodPreview preview = new FoodPreview();
				preview.Name = i.food_name;
				preview.PhotoUrl = i.photo.thumb;
				preview.ServingQuantity = i.serving_qty;
				preview.ServingUnit = i.serving_unit;
				res.FoodSearchResults.Add(preview);
			}

			foreach (var i in commonResults)
			{
				FoodPreview preview = new FoodPreview();
				preview.Name = i.food_name;
				preview.PhotoUrl = i.photo.thumb;
				preview.ServingQuantity = i.serving_qty;
				preview.ServingUnit = i.serving_unit;
				res.FoodSearchResults.Add(preview);
			}

			return View(res);
		}

		[HttpGet]
		public IActionResult ViewFoodDetail(string name, string imgurl, string serving_unit, float serving_qty)
		{
			ApiDAL api = new ApiDAL();
			api.endpoint = "https://trackapi.nutritionix.com/v2/natural/nutrients/";
			string jsonNutrition = api.getNutritionInfo(name);
			FoodItem foodItem = JsonConvert.DeserializeObject<FoodItem>(jsonNutrition);
			foodItem.foods[0].Name = name;
			foodItem.foods[0].Imgurl = imgurl;
			foodItem.foods[0].serving_qty = serving_qty;
			foodItem.foods[0].serving_unit = serving_unit;
			return View(foodItem);
		}

		//[HttpPost]
		//public IActionResult AddFoodEditDetails()
		//{

		//}

		[HttpGet]
		public IActionResult RecentFoods()
		{
			var user = authProvider.GetCurrentUser();
			IList<UserFood> recentUserFoods = new List<UserFood>();
			recentUserFoods = dal.GetRecentFoods(user.Id);
			
			return View(recentUserFoods);
		}

		[HttpPost]
		public IActionResult SaveFood(Food foodItem, int mealId, int numberOfServings)
		{
			User user = authProvider.GetCurrentUser();
			dal.SaveItemToUserFoodLog(user, foodItem, mealId, numberOfServings);
		
			return RedirectToAction("Index", "Dashboard");
		}
	}	
}