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
            catch(SqlException ex)
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
                    SqlCommand cmd = new SqlCommand("UPDATE users SET password = @password, salt = @salt, role = @role WHERE id = @id;", conn);                    
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@role", user.Role);
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

        private User MapRowToUser (SqlDataReader reader)
        {
			return new User()
			{
				Id = Convert.ToInt32(reader["userId"]),
				Username = Convert.ToString(reader["userName"]),
				Email = Convert.ToString(reader["email"]),
				//FirstName = Convert.ToString(reader["userFirstName"]),
				//LastName = Convert.ToString(reader["userLastName"]),
				//BirthDate = Convert.ToDateTime(reader["birthday"]),
				//Age = Convert.ToInt32(reader["userAge"]),
				//Height = Convert.ToInt32(reader["userHeight"]),
				//CurrentWeight = Convert.ToInt32(reader["userCurrentWeight"]),
				//DesiredWeight = Convert.ToInt32(reader["userDesiredWeight"]),
				//RecommendedDailyCaloricIntake = Convert.ToInt32(reader["recommendedDailyCaloricIntake"]),
				//MealStreak = Convert.ToInt32(reader["mealStreak"]),
				Password = Convert.ToString(reader["password"]),
				Salt = Convert.ToString(reader["salt"]),
				Role = Convert.ToString(reader["role"])
			};
        }
    }
}
