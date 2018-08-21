using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.DAL
{
    public interface IApiDAL
    {
		/// <summary>
		/// Searches Nutritionix API for the food specified by the endpoint property of the ApiDal instance.
		/// </summary>
		/// <returns>Json response data as a string.</returns>
		string searchForFood();

		/// <summary>
		/// Gets the nutrition information for the specified food name.
		/// </summary>
		/// <param name="foodName"></param>
		/// <returns>Json response data as a string</returns>
		string getNutritionInfo(string foodName);
 

	}
}
