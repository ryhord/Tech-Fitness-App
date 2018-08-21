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

		public User SaveItemToUserFoodLog(User user, Food food, int mealId, int numberOfServings)
		{
			try
			{

				//// PROPERLY CONFIGURE DEP INJ ||| Think its done
				//// PASS THE FOOD PREVIEW SERVING INFO THROUGH


				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand( "SELECT * FROM foods WHERE foodName = @foodName;", conn);
					cmd.Parameters.AddWithValue("@foodName", food.Name);

					var resultReturned = cmd.ExecuteReader();
					

					// Check if food is in already in food table -- by name
					if (!resultReturned.HasRows)
					{
						// add the food to the table
						conn.Close();
						conn.Open();
						SqlCommand newFood = new SqlCommand($"INSERT INTO foods(foodName, servingQuantity, servingUnit, calories, totalFat, saturatedFat, cholesterol, sodium, totalCarbohydrate, dietaryFiber, sugars, protein, potassium, imgurl) VALUES(@foodName, @servingQuantity, @servingUnit, @calories, @totalFat, @saturatedFat, @cholesterol, @sodium, @totalCarbohydrate, @dietaryFiber, @sugars, @protein, @potassium, @imgurl); ", conn);
						newFood.Parameters.AddWithValue("@foodName", food.Name);
						newFood.Parameters.AddWithValue("@servingQuantity", food.serving_qty);
						newFood.Parameters.AddWithValue("@servingUnit", food.serving_unit);
						newFood.Parameters.AddWithValue("@calories", food.nf_calories);
						newFood.Parameters.AddWithValue("@totalFat", food.nf_total_fat);
						newFood.Parameters.AddWithValue("@saturatedFat", food.nf_saturated_fat);
						newFood.Parameters.AddWithValue("@cholesterol", food.nf_cholesterol);
						newFood.Parameters.AddWithValue("@sodium", food.nf_sodium);
						newFood.Parameters.AddWithValue("@totalCarbohydrate", food.nf_total_carbohydrate);
						newFood.Parameters.AddWithValue("@dietaryFiber", food.nf_dietary_fiber);
						newFood.Parameters.AddWithValue("@sugars", food.nf_sugars);
						newFood.Parameters.AddWithValue("@protein", food.nf_protein);
						newFood.Parameters.AddWithValue("@potassium", food.nf_potassium);
						newFood.Parameters.AddWithValue("@imgurl", food.Imgurl);

						var rowsAffected = newFood.ExecuteNonQuery();

					}

					//just need to insert user food record
					UserFood userFood = new UserFood();
					userFood.UserId = user.Id;
					userFood.DateOfEntry = DateTime.Now;
					userFood.CaloriesPerServing = (float)food.nf_calories;
					userFood.MealId = mealId;
					userFood.NumberOfServings = numberOfServings;
					userFood.ServingQuantity = food.serving_qty;
					userFood.ServingUnit = food.serving_unit;
					userFood.FoodName = food.Name;

					conn.Close();
					conn.Open();

					// foodId was previous in this string if it needs to be added back
					SqlCommand newUserFood = new SqlCommand($"INSERT INTO users_foods (userId, dateOfEntry, mealId, caloriesPerServing, numberOfServings, servingQuantity, servingUnit, foodName) VALUES (@userId, @dateOfEntry, @mealId, @caloriesPerServing, @numberOfServings, @servingQuantity, @servingUnit, @foodName);", conn);
					newUserFood.Parameters.AddWithValue("@userId", userFood.UserId);
					newUserFood.Parameters.AddWithValue("@dateOfEntry", userFood.DateOfEntry);
					newUserFood.Parameters.AddWithValue("@mealId", userFood.MealId);
					newUserFood.Parameters.AddWithValue("@caloriesPerServing", userFood.CaloriesPerServing);
					newUserFood.Parameters.AddWithValue("@numberOfServings", userFood.NumberOfServings);
					newUserFood.Parameters.AddWithValue("@servingQuantity", userFood.ServingQuantity);
					newUserFood.Parameters.AddWithValue("@servingUnit", userFood.ServingUnit);
					//newUserFood.Parameters.AddWithValue("@foodId", foodId);
					newUserFood.Parameters.AddWithValue("@foodName", userFood.FoodName);

					var rowsAffected2 = newUserFood.ExecuteNonQuery();

					//conn.Close();
					//conn.Open();

					//SqlCommand getFoodId = new SqlCommand($"SELECT foods.foodId FROM foods INNER JOIN users_foods ON foods.foodName = users_foods.foodName WHERE foods.foodName = @foodName", conn);
					//getFoodId.Parameters.AddWithValue("@foodName", userFood.FoodName);

					//SqlDataReader reader = getFoodId.ExecuteReader();
					//int foodId = 0;
					//if (reader.Read())
					//{
					//	foodId = Convert.ToInt32(reader["foodId"]);
					//}





					return user;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		public IList<UserFood> GetUserFoods(int userId)
		{
			List<UserFood> userFoods = new List<UserFood>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM users_foods INNER JOIN foods ON users_foods.foodName = foods.foodName WHERE users_foods.userId = @userId;", conn);
					cmd.Parameters.AddWithValue("@userId", userId);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						UserFood userFood = new UserFood();
						userFood = MapRowToUser(reader);

						userFoods.Add(userFood);
					}
				}

				return userFoods;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		private UserFood MapRowToUser(SqlDataReader reader)
		{

			UserFood userFood = new UserFood();

			userFood.RowId = Convert.ToInt32(reader["rowId"]);
			userFood.UserId = Convert.ToInt32(reader["userId"]);
			userFood.DateOfEntry = Convert.ToDateTime(reader["dateOfEntry"]);
			userFood.MealId = Convert.ToInt32(reader["mealId"]);
			userFood.CaloriesPerServing = (float)Convert.ToDecimal(reader["caloriesPerServing"]);
			userFood.NumberOfServings = (float)Convert.ToDecimal(reader["numberOfServings"]);
			userFood.FoodName = Convert.ToString(reader["foodName"]);
			userFood.ServingQuantity = (float)Convert.ToDecimal(reader["servingQuantity"]);
			userFood.ServingUnit = Convert.ToString(reader["servingUnit"]);

			return userFood;
		}
	}
}
