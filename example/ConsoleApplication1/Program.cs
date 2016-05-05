using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace ExampleApplication
{
    using Newtonsoft.Json;
    using pokitdokcsharp;
    using System.Xml;
    class Program
    {
        const string TOKEN_FILE = @"token.json";

        static void tokenRefresh(OauthAccessToken accessToken)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(TOKEN_FILE))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OauthAccessToken));
                serializer.WriteObject(file.BaseStream, accessToken);
            }
        }

        static OauthAccessToken getToken()
        {
            OauthAccessToken token = null;
            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(TOKEN_FILE))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OauthAccessToken));
                    token = (OauthAccessToken)serializer.ReadObject(file.BaseStream);
                }
            }
            catch (FileNotFoundException)
            {
                ;
            }

            return token;
        }

        private static void print_response(ResponseData resp, string query_type = "response body")
        {
            if (resp.status == 200) // this is the HTTP status code
            {
                foreach (KeyValuePair<string, string> entry in resp.header)
                {
                    Console.WriteLine("response header <" + entry.Key + "> = " + entry.Value);
                }
                Console.WriteLine(query_type + "= " + resp.body + "\n\n");
            }

        }


        static void Main(string[] args)
        {
            OauthAccessToken token = getToken();

            PlatformClient client = new PlatformClient(
                    clientId:  "GWXUqSQ7C1jMhvKqAG4p",
                    clientSecret: "YXPKhDRFlaWopREHo2mDIbQi3B1h800jBcfoO1pe"
                );

            client.ApiSite = "https://platform.pokitdok.com/";


            // Example activities query
            ResponseData activity = client.activities();
            print_response(activity, "all current activities");

            // Example providers query            
            ResponseData resp = client.providers("1467560003");
            print_response(resp, "provider");

            // Example appointments query
            resp = client.appointments(new Dictionary<string, string> {
                {"patient_uuid", "769cf5a1-e848-4ba5-a670-ab1d3a4c4fd4"},
                {"start_date", "2015-02-01T08:00:00"},
                {"end_date", "2015-02-15T17:00:00"},
                {"appointment_type", ""}
            });
            print_response(resp, "appointments");

            // Example pharmacy APIs
            resp = client.pharmacyPlans(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "S5601034"}
            });
            print_response(resp, "plans");

            resp = client.pharmacyFormulary(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "H2256001"},
                {"drug",  "virazole"}
            });
            print_response(resp, "formulary");

            resp = client.pharmacyDrugCost(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "H2256001"},
                {"drug",  "virazole"}
            });
            print_response(resp, "cost");

            resp = client.pharmacyNetwork(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "S5601034"},
                {"pharmacy_type", "retail"}
            });
            print_response(resp, "network");

            resp = client.pharmacyNetwork("1275827032", 
                new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "S5601034"},
                });
            print_response(resp, "networkNPI");


            Console.Read();

        }

    }



}
