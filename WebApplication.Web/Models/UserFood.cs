using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
	public class UserFood
	{
		public int RowId { get; set; }
		public int UserId { get; set; }
		public DateTime DateOfEntry { get; set; }
		public int MealId { get; set; }
		public float CaloriesPerServing { get; set; }
		public float NumberOfServings { get; set; }
		public float ServingSize { get; set; }
		public string ServingUnit { get; set; }
		public string FoodName { get; set; }
	}
		
}
