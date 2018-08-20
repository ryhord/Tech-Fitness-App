using System;
using System.Collections.Generic;
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
			throw new NotImplementedException();
		}
	}
}
