using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class FoodPreview
    {
		/// <summary>
		/// The name of the food result.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The url for provided thumbnail.
		/// </summary>
		public string PhotoUrl { get; set; }
    }
}
