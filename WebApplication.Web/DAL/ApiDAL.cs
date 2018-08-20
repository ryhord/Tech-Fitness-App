using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
	public enum httpVerb
	{
		GET,
		POST,
		PUT,
		DELETE
	}

	public class ApiDAL : IApiDAL
    {

		public string endpoint { get; set; }
		public httpVerb httpMethod { get; set; }

		public ApiDAL()
		{
			endpoint = "";
			httpMethod = httpVerb.GET;
		}

		public string searchForFood()
		{
			string strResponseValue = string.Empty;
			httpMethod = httpVerb.GET;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);

			request.Method = httpMethod.ToString();
			request.Headers["Content-Type"] = "application/json";
			request.Headers["x-app-id"] = "fdc869f7";
			request.Headers["x-app-key"] = "e5969a224b73d9ea0899bb2d6934badc";
			

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new ApplicationException("Error code: " + response.StatusCode.ToString());
				}
				
				//Process the response stream

				using(Stream responseStream = response.GetResponseStream())
				{
					if(responseStream != null)
					{
						using(StreamReader reader = new StreamReader(responseStream))
						{
							strResponseValue = reader.ReadToEnd();
						}
					}
				} // End of using ResponseStream

				return strResponseValue;
			}// end of using response

		}

		public string getNutritionInfo(string foodName)
		{
			string strResponseValue = string.Empty;
			httpMethod = httpVerb.POST;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);

			request.Method = httpMethod.ToString();
			request.Headers["Content-Type"] = "application/json";
			request.Headers["x-app-id"] = "fdc869f7";
			request.Headers["x-app-key"] = "e5969a224b73d9ea0899bb2d6934badc";

			string body = "{\"query\": \"" + foodName + "\", \"num_servings\": 1,\"aggregate\": \"string\",\"line_delimited\": false,\"use_raw_foods\": false,\"include_subrecipe\": false,\"timezone\": \"US/Eastern\",\"consumed_at\": null,\"lat\": null,\"lng\": null,\"meal_type\": 0,\"use_branded_foods\": false,\"locale\": \"en_US\"}";
			NutritionInfoRequestModel reqbody = JsonConvert.DeserializeObject<NutritionInfoRequestModel>(body);
			byte[] byteArray = Encoding.UTF8.GetBytes(body);

			request.ContentLength = byteArray.Length;
			// Get the request stream.  
			Stream dataStream = request.GetRequestStream();
			// Write the data to the request stream.  
			dataStream.Write(byteArray, 0, byteArray.Length);
			// Close the Stream object.  
			dataStream.Close();
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new ApplicationException("Error code: " + response.StatusCode.ToString());
				}

				//Process the response stream

				using (Stream responseStream = response.GetResponseStream())
				{
					if (responseStream != null)
					{
						using (StreamReader reader = new StreamReader(responseStream))
						{
							strResponseValue = reader.ReadToEnd();
						}
					}
				} // End of using ResponseStream

				return strResponseValue;
			}// end of using response

		}
	}


}
