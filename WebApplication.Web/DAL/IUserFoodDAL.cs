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
		/// Saves a foodItem to the foods table and users_foods table 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="foodItem"></param>
		/// <returns>User</returns>
		User SaveItemToUserFoodLog(User user, Food food);
	}
}
