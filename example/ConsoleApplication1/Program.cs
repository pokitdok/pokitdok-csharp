using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace ConsoleApplication1
{
	using pokitdokcsharp;

	class Program
    {
		const string TOKEN_FILE = @"token.json";

		static void tokenRefresh(OauthAccessToken accessToken)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(TOKEN_FILE)) {
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OauthAccessToken));
				serializer.WriteObject(file.BaseStream, accessToken);
			}
		}

		static OauthAccessToken getToken()
		{
			OauthAccessToken token = null;
			try {
				using (System.IO.StreamReader file = new System.IO.StreamReader (TOKEN_FILE)) {
					DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OauthAccessToken));
					token = (OauthAccessToken) serializer.ReadObject(file.BaseStream);
				}
			} catch (FileNotFoundException) {
				;
			}

			return token;
		}

        static void Main(string[] args)
        {
			OauthAccessToken token = getToken();

            PlatformClient client = new PlatformClient(
				"QX5ZpPJTQNzIDGvNxtNl",
				"ihtTYpBlN0pQXedfWDji6CktnmputdpJAkstMEDZ",
                PlatformClient.DEFAULT_TIMEOUT,
				token,
                new Uri("http://localhost:5002/oauth2-code-grant-debug"),
				new TokenRefreshDelegate(tokenRefresh),
                new string[] { "user_schedule" },
				"5yuFbCuSEpGyDHI7Hz0MFMlleWgp4PRIN1MWCPma"
            );
			client.ApiSite = "http://me.pokitdok.com:5002";

            ResponseData resp = client.providers("1467560003");
            if (resp.status == 200) // this is the HTTP status code
            {
                foreach (KeyValuePair<string, string> entry in resp.header)
                {
                    Console.WriteLine("response header <" + entry.Key + "> = " + entry.Value);
                }
				Console.WriteLine("response body= "+ resp.body);
            }

			resp = client.appointments(new Dictionary<string, string> {
				{"patient_uuid", "769cf5a1-e848-4ba5-a670-ab1d3a4c4fd4"},
				{"start_date", "2015-02-01T08:00:00"},
				{"end_date", "2015-02-15T17:00:00"},
				{"appointment_type", ""}
			});
			if (resp.status == 200) // this is the HTTP status code
			{
				Console.WriteLine("response body= "+ resp.body);
			}
        }
    }
}
