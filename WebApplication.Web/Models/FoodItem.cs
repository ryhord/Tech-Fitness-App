using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
	public class FoodItem
	{

		public Food[] foods { get; set; }

		public class Food
		{
			public string food_name { get; set; }
			public object brand_name { get; set; }
			public float serving_qty { get; set; }
			public string serving_unit { get; set; }
			public float serving_weight_grams { get; set; }
			public float nf_calories { get; set; }
			public float nf_total_fat { get; set; }
			public float nf_saturated_fat { get; set; }
			public float nf_cholesterol { get; set; }
			public float nf_sodium { get; set; }
			public float nf_total_carbohydrate { get; set; }
			public float nf_dietary_fiber { get; set; }
			public float nf_sugars { get; set; }
			public float nf_protein { get; set; }
			public object nf_potassium { get; set; }
			public object nf_p { get; set; }
			public Full_Nutrients[] full_nutrients { get; set; }
			public object nix_brand_name { get; set; }
			public object nix_brand_id { get; set; }
			public object nix_item_name { get; set; }
			public object nix_item_id { get; set; }
			public object upc { get; set; }
			public DateTime consumed_at { get; set; }
			public Metadata metadata { get; set; }
			public object source { get; set; }
			public object ndb_no { get; set; }
			public object tags { get; set; }
			public object alt_measures { get; set; }
			public object lat { get; set; }
			public object lng { get; set; }
			public int meal_type { get; set; }
			public Photo photo { get; set; }
			public object sub_recipe { get; set; }
			public string Name { get; set; }
			public string Imgurl { get; set; }
		}

		public class Metadata
		{
		}

		public class Photo
		{
			public string thumb { get; set; }
			public object highres { get; set; }
			public bool is_user_uploaded { get; set; }
		}

		public class Full_Nutrients
		{
			public int attr_id { get; set; }
			public float value { get; set; }
		}


	}
}
