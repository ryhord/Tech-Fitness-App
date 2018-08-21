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

		/// <summary>
		/// The unit a serving is measured by.
		/// </summary>
		public string ServingUnit { get; set; }

		/// <summary>
		/// The quantity of serving units that make up a single serving.
		/// </summary>
		public float ServingQuantity { get; set; }

		// serving_unit
		// serving_qty
	}

}
