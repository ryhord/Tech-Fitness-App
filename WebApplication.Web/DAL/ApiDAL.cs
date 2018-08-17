using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApplication.Web.DAL
{
	public enum httpVerb
	{
		GET,
		POST,
		PUT,
		DELETE
	}

	public class ApiDAL
    {

		public string endpoint { get; set; }
		public httpVerb httpMethod { get; set; }

		public ApiDAL()
		{
			endpoint = "";
			httpMethod = httpVerb.GET;
		}

		public string makeRequest()
		{
			string strResponseValue = string.Empty;

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
	}


}
