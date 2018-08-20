using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    interface IUserFoodDAL
    {
		/// <summary>
		/// Saves a foodItem to the foods table and users_foods table 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="foodItem"></param>
		/// <returns>User</returns>
		User SaveItemToUserFoodLog(User user, FoodItem foodItem);
	}
}
