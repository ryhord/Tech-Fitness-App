using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.DAL
{
    public class WeightDAL : IWeightDAL
    {
		private readonly string connectionString;

		public WeightDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IList<int> GetWeights(DateTime startDate, DateTime endDate)
		{
			List<int> weightsList = new List<int>();

			return weightsList;
		}
	}
}
