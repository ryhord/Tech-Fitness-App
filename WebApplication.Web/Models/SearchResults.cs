using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
	public class SearchResults
	{
		public SearchResults()
		{
			FoodSearchResults = new List<FoodPreview>();
		}

		
		/// <summary>
		/// List of the foods found in the search.
		/// </summary>
		public List<FoodPreview> FoodSearchResults { get; set; }

		public string Name { get; set; }
    }
}
