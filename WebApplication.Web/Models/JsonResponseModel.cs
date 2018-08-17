using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
	public class JsonResponseModel
	{


			public Branded[] branded { get; set; }
			public Common[] common { get; set; }

		public class Branded
		{
			public string food_name { get; set; }
			public string serving_unit { get; set; }
			public string nix_brand_id { get; set; }
			public string brand_name_item_name { get; set; }
			public int serving_qty { get; set; }
			public float nf_calories { get; set; }
			public Photo photo { get; set; }
			public string brand_name { get; set; }
			public int region { get; set; }
			public int brand_type { get; set; }
			public string nix_item_id { get; set; }
			public string locale { get; set; }
		}

		public class Photo
		{
			public string thumb { get; set; }
			public object highres { get; set; }
			public bool is_user_uploaded { get; set; }
		}

		public class Common
		{
			public string food_name { get; set; }
			public string serving_unit { get; set; }
			public int serving_qty { get; set; }
			public Photo1 photo { get; set; }
			public string tag_id { get; set; }
			public string locale { get; set; }
		}

		public class Photo1
		{
			public string thumb { get; set; }
		}

	}
}
