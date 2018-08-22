using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public interface IUserFoodDAL
    {
		

		/// <summary>
		/// Makes a call to the API with name provided before saving foodItem to foods table and users_foods table.
		/// </summary>
		/// <param name="user"></param>
		/// <param name="name"></param>
		void SaveItemToUserFoodLog(int userId, Food food);

		/// <summary>
		/// Saves a foodItem to the foods table and users_foods table 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="foodItem"></param>
		/// <returns>User</returns>
		void SaveItemToUserFoodLog(int userId, Food food, int mealId, int numberOfServings);

		/// <summary>
		/// Retrieves all food entries for a user.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		IList<UserFood> GetUserFoods(int userId);

		/// <summary>
		/// Collects top 10 recently entered foods.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		IList<UserFood> GetRecentFoods(int userId);

		void DeleteFoodItem(User user, int userId, int rowId);
	}
}
