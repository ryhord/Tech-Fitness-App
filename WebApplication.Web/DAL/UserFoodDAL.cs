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

		public User SaveItemToUserFoodLog(User user, Food food)
		{
			try
			{

				//// PROPERLY CONFIGURE DEP INJ ||| Think its done
				//// PASS THE FOOD PREVIEW SERVING INFO THROUGH


				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand( "SELECT * FROM foods WHERE Name = @name", conn);
					cmd.Parameters.AddWithValue("@name", food.Name);

					var resultReturned = cmd.ExecuteReader();

					// Check if food is in already in food table -- by name
					if (!resultReturned.HasRows)
					{
						// add the food to the table
						conn.Close();
						conn.Open();
						SqlCommand newFood = new SqlCommand(//$" SET IDENTITY_INSERT foods ON " +
							$"INSERT INTO foods (foodName, " +
																				$"servingQuantity, " +
																				$"servingUnit, " +
																				$"calories, totalFat, " +
																				 $"saturatedFat, cholesterol, " +
																				 $"sodium, totalCarbohydrate, " +
																				 $"dietaryFiber, " +
																				 $"sugars, " +
																				 $"protein, " +
																				 $"potassium, " +
																				 $"imgurl, " +
																				$"mealClassification) " +
															$"VALUES (@foodName, " +
																				$"@servingQuantity, " +
																				$"@servingUnit, " +
																			   $"@calories," +
																			   $"@totalFat, " +
																			   $"@saturatedFat, " +
																			   $"@cholesterol, " +
																			   $"@sodium, " +
																			   $"@totalCarbohydrate, " +
																			   $"@dietaryFiber, " +
																			   $"@sugars," +
																			   $"@protein, " +
																			   $"@potassium, " +
																			   $"@imgurl, " +
																			   $"@mealClassification);", conn);
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
						//newFood.Parameters.AddWithValue("@mealClassification", foodItem.foods[0].Name);

						cmd.ExecuteNonQuery();

					}

					//just need to insert user food record
					UserFood userFood = new UserFood();
					userFood.UserId = user.Id;
					userFood.DateOfEntry = DateTime.Now;
					userFood.CaloriesPerServing = (float)food.nf_calories;
					userFood.MealId = 1; //This is a placeholder for now
					userFood.NumberOfServings = 1; // This needs to be user entered
					userFood.ServingQuantity = food.serving_qty;
					userFood.ServingUnit = food.serving_unit;

					SqlCommand newUserFood = new SqlCommand($"INSERT INTO users_foods (userId, dateOfEntry, mealId, caloriesPerServing, numberOfServings, servingQuantity, servingUnit) VALUES (@userId, @dateOfEntry, @mealId, @caloriesPerServing, @numberOfServings, @servingQuantity, @servingUnit);", conn);
					newUserFood.Parameters.AddWithValue("@userId", userFood.UserId);
					newUserFood.Parameters.AddWithValue("@dateOfEntry", userFood.DateOfEntry);
					newUserFood.Parameters.AddWithValue("@mealId", userFood.MealId);
					newUserFood.Parameters.AddWithValue("@caloriesPerServing", userFood.CaloriesPerServing);
					newUserFood.Parameters.AddWithValue("@numberOfServings", userFood.NumberOfServings);
					newUserFood.Parameters.AddWithValue("@servingQuantity", userFood.ServingQuantity);
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
