using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class WeightDAL : IWeightDAL
    {
		private readonly string connectionString;

		public WeightDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IList<UserWeight> GetWeights(User user, DateTime startDate, DateTime endDate)
		{
			List<UserWeight> weightsList = new List<UserWeight>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM users " +
                        "INNER JOIN dailyWeight ON users.userId = dailyWeight.userId " +
                        "WHERE users.userId = @userId " +
                        "AND dailyWeight.dateOfEntry > @startDate " +
                        "AND dailyWeight.dateOfEntry < @endDate;");

                    cmd.Parameters.AddWithValue("@userId", user.Id);
                    cmd.Parameters.AddWithValue("@startDate", startDate.ToShortDateString());
                    cmd.Parameters.AddWithValue("@endDate", endDate.ToShortDateString());

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserWeight userWeight = new UserWeight();

                        userWeight.UserId = Convert.ToInt32(reader["userId"]);
                        userWeight.TodaysWeight = Convert.ToInt32(reader["todaysWeight"]);
                        userWeight.DateOfEntry = Convert.ToDateTime(reader["dateOfEntry"]);

                        weightsList.Add(userWeight);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

			return weightsList;
		}
	}
}
