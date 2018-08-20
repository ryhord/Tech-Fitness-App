using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
	public class UserSqlDAL : IUserDAL
	{
		private readonly string connectionString;

		public UserSqlDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		/// <summary>
		/// Saves the user to the database.
		/// </summary>
		/// <param name="user"></param>
		public void CreateUser(User user)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand($"INSERT INTO users (userName, userFirstName, userLastname, birthday, email, password, salt) VALUES (@username, @userFirstName, @userLastName, @birthday, @email, @password, @salt);", conn);
					cmd.Parameters.AddWithValue("@username", user.Username);
					cmd.Parameters.AddWithValue("@userFirstName", user.FirstName);
					cmd.Parameters.AddWithValue("@userLastName", user.LastName);
					cmd.Parameters.AddWithValue("@birthday", user.BirthDate);
					cmd.Parameters.AddWithValue("@email", user.Email);
					cmd.Parameters.AddWithValue("@password", user.Password);
					cmd.Parameters.AddWithValue("@salt", user.Salt);
					//cmd.Parameters.AddWithValue("@role", user.Role);

					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Deletes the user from the database.
		/// </summary>
		/// <param name="user"></param>
		public void DeleteUser(User user)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @id;", conn);
					cmd.Parameters.AddWithValue("@id", user.Id);

					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the user from the database.
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public User GetUser(string username)
		{
			User user = null;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM USERS WHERE username = @username;", conn);
					cmd.Parameters.AddWithValue("@username", username);

					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.Read())
					{
						user = MapRowToUser(reader);
					}
				}

				return user;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the user from the database.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public User GetEmail(string email)
		{
			User user = null;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM USERS WHERE email = @email;", conn);
					cmd.Parameters.AddWithValue("@email", email);

					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.Read())
					{
						user = MapRowToUser(reader);
					}
				}

				return user;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Updates the user in the database.
		/// </summary>
		/// <param name="user"></param>
		public void UpdateUser(User user)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("UPDATE users SET userName = @userName, userFirstName = @userFirstName, userLastName = @userLastName, userHeight = @userHeight, userCurrentWeight = @userCurrentWeight, userDesiredWeight = @userDesiredWeight, recommendedDailyCaloricIntake = @recommendedDailyCaloricIntake, password = @password, salt = @salt WHERE userId = @userId", conn);
					cmd.Parameters.AddWithValue("@userName", user.Username);
					cmd.Parameters.AddWithValue("@userFirstName", user.FirstName);
					cmd.Parameters.AddWithValue("@userLastName", user.LastName);
					cmd.Parameters.AddWithValue("@userHeight", user.Height);
					cmd.Parameters.AddWithValue("@userCurrentWeight", user.CurrentWeight);
					cmd.Parameters.AddWithValue("@userDesiredWeight", user.DesiredWeight);
					cmd.Parameters.AddWithValue("@recommendedDailyCaloricIntake", user.RecommendedDailyCaloricIntake);
					cmd.Parameters.AddWithValue("@password", user.Password);
					cmd.Parameters.AddWithValue("@salt", user.Salt);
					cmd.Parameters.AddWithValue("@userId", user.Id);

					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		private User MapRowToUser(SqlDataReader reader)
		{

			User user = new User();
			user.Id = Convert.ToInt32(reader["userId"]);
			user.Username = Convert.ToString(reader["userName"]);
			user.FirstName = Convert.ToString(reader["userFirstName"]);
			user.LastName = Convert.ToString(reader["userLastName"]);
			user.BirthDate = Convert.ToDateTime(reader["birthday"]);
			user.Email = Convert.ToString(reader["email"]);
			user.Password = Convert.ToString(reader["password"]);
			user.Salt = Convert.ToString(reader["salt"]);
			user.Role = Convert.ToString(reader["role"]);

			if (!DBNull.Value.Equals(reader["userHeight"]))
			{
				user.Height = Convert.ToInt32(reader["userHeight"]);
			}

			if (!DBNull.Value.Equals(reader["userCurrentWeight"]))
			{
				user.CurrentWeight = Convert.ToInt32(reader["userCurrentWeight"]);
			}

			if (!DBNull.Value.Equals(reader["userDesiredWeight"]))
			{
				user.DesiredWeight = Convert.ToInt32(reader["userDesiredWeight"]);
			}

			if (!DBNull.Value.Equals(reader["recommendedDailyCaloricIntake"]))
			{
				user.RecommendedDailyCaloricIntake = Convert.ToInt32(reader["recommendedDailyCaloricIntake"]);
			}

			if (!DBNull.Value.Equals(reader["mealStreak"]))
			{
				user.MealStreak = Convert.ToInt32(reader["mealStreak"]);
			}

			return user;
		}

	}
}
