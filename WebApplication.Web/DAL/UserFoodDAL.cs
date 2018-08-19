using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class UserFoodDAL
    {
		private readonly string connectionString;

		public User SaveItemToUserFoodLog(User user)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("UPDATE users SET userName = @userName, userFirstName = @userFirstName, userLastName = @userLastName, userHeight = @userHeight, userCurrentWeight = @userCurrentWeight, userDesiredWeight = @userDesiredWeight, recommendedDailyCaloricIntake = @recommendedDailyCaloricIntake, password = @password, salt = @salt WHERE userId = @userId", conn);
					cmd.Parameters.AddWithValue("@userName", user.Username);


					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}
    }
}
