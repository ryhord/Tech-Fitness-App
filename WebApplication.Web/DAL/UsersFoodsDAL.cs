using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
	public class UsersFoodsDAL : IUsersFoodsDAL
	{
		private readonly string connectionString;

		public UsersFoodsDAL(string connectionString)
		{
			this.connectionString = connectionString;
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