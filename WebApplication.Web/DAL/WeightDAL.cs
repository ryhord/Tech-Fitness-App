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

		public IList<int> GetWeights(User user, DateTime startDate, DateTime endDate)
		{
			List<int> weightsList = new List<int>();

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
