using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class UserFoodDAL : IUserFoodDAL
    {
		private readonly string connectionString;

		public UserFoodDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public User SaveItemToUserFoodLog(User user, FoodItem foodItem)
		{
			try
			{

				//// PROPERLY CONFIGURE DEP INJ ||| Think its done
				//// PASS THE FOOD PREVIEW SERVING INFO THROUGH


				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand( "SELECT * FROM foods WHERE foodName = @foodName", conn);
					cmd.Parameters.AddWithValue("@foodName", foodItem.foods[0].Name);

					var resultReturned = cmd.ExecuteReader();

					// Check if food is in already in food table -- by name
					if (!resultReturned.Read())
					{
						// add the food to the table

						conn.Open();
						SqlCommand newFood = new SqlCommand($"INSERT INTO foods (foodName, servingSize, calories, foodGroup) VALUES (@foodName, @servingSize, @calories, @foodGroup);", conn);
						newFood.Parameters.AddWithValue("@foodName", foodItem.foods[0].Name);
						newFood.Parameters.AddWithValue("@servingSize", foodItem.foods[0].Name); //not right
						newFood.Parameters.AddWithValue("@calories", foodItem.foods[0].nf_calories);
						newFood.Parameters.AddWithValue("@foodGroup", foodItem.foods[0].Name); // not right

						cmd.ExecuteNonQuery();

					}

					//just need to insert user food record
					UserFood userFood = new UserFood();
					userFood.UserId = user.Id;
					userFood.DateOfEntry = DateTime.Now;
					userFood.CaloriesPerServing = (float)foodItem.foods[0].nf_calories;
					userFood.MealId = 1; //This is a placeholder for now
					userFood.NumberOfServings = 1; // This needs to be user entered
					userFood.ServingSize = 1; // this neeeds to be retrieved/passed from the initial search results
					userFood.ServingUnit = "g"; // this neeeds to be retrieved/passed from the initial search results

					SqlCommand newUserFood = new SqlCommand($"INSERT INTO users_foods (userId, dateOfEntry, mealId, numberOfServings, servingSize, servingUnit) VALUES (@foodName, @servingSize, @calories, @foodGroup, @servingSize, @servingUnit);", conn);
					newUserFood.Parameters.AddWithValue("@userId", userFood.UserId);
					newUserFood.Parameters.AddWithValue("@dateOfEntry", userFood.DateOfEntry);
					newUserFood.Parameters.AddWithValue("@mealId", userFood.MealId);
					newUserFood.Parameters.AddWithValue("@numberOfServings", userFood.NumberOfServings);
					newUserFood.Parameters.AddWithValue("@servingSize", userFood.ServingSize);
					newUserFood.Parameters.AddWithValue("@servingUnit", userFood.ServingUnit);

					cmd.ExecuteNonQuery();

					return user;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}
    }
}
