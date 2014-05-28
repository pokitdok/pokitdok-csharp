using System;
using System.Collections.Generic;
using System.IO;
using pokitdokcsharp;

class MainClass
{
	public static void Main (string[] args)
	{
		try {

			PlatformClient client = new PlatformClient("LNrngr9X4zkwAPdwI8uf", "htr5ckvvhc9g83qqlapGt5APJE95a3yEsBZhUezV");
			client.ApiSite = "http://me.pokitdok.com:5002";

			client.Authenticate();

			Console.WriteLine(
				client.activities("").body
			);
			Console.WriteLine("=======");

			Console.WriteLine(
				client.eligibility(
					new Dictionary<string, object> {
						{  "payer_id", "MOCKPAYER" },
						{  "member_id", "W34237875729" },
						{  "provider_id", "1467560003" },
						{  "provider_name", "AYA-AY" },
						{  "provider_first_name", "JEROME" },
						{  "member_name", "JOHN DOE" },
						{  "provider_type", "Person" },
						{  "member_birth_date", "05/21/1975" },
						{  "service_types", new string[] { "Health Benefit Plan Coverage" } }
				}).body
			);
			Console.WriteLine("=======");

			Console.WriteLine(
				client.providers(
					new Dictionary<string, string> {
						{"zip_code", "29464"},
						{"radius", "15mi"}
				}).body
			);
			Console.WriteLine("=======");

			Console.WriteLine(
				client.providers("1467560003").body
			);
			Console.WriteLine("=======");

			Console.WriteLine(
				string.Format("rate_limit_amount = {0}", client.usage().rate_limit_amount)
			);
			Console.WriteLine("=======");

			ResponseData resp = client.files(
				"MOCKPAYER",
				"/Users/timdunlevy/pd/pokitdok-csharp/general-physician-office-visit.270"
			);
			Console.WriteLine(resp.body);
			Console.WriteLine(resp.status);
			Console.WriteLine("=======");
		}
		catch (Exception ex) 
		{
			Console.Error.WriteLine(ex.Message);
		}
	}
}
