using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
	public class NutritionInfoRequestModel
	{
		public string query { get; set; }
		public int num_servings { get; set; }
		public string aggregate { get; set; }
		public bool line_delimited { get; set; }
		public bool use_raw_foods { get; set; }
		public bool include_subrecipe { get; set; }
		public string timezone { get; set; }
		public object consumed_at { get; set; }
		public object lat { get; set; }
		public object lng { get; set; }
		public int meal_type { get; set; }
		public bool use_branded_foods { get; set; }
		public string locale { get; set; }
	}
}
