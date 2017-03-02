using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;


namespace ExampleApplication
{
    using pokitdokcsharp;

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
                    token = (OauthAccessToken) serializer.ReadObject(file.BaseStream);
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

        ///
        /// A series of example requets to the PokitDok API
        ///

        #region ExampleRequests
        public static void ActivitiesExample(PlatformClient client)
        {
            var activities = client.activities();
            print_response(activities, "all current activities");
        }
        public static void AppointmentsExample(PlatformClient client)
        {
            var resp = client.appointments(new Dictionary<string, string> {
                {"patient_uuid", "769cf5a1-e848-4ba5-a670-ab1d3a4c4fd4"},
                {"start_date", "2015-02-01T08:00:00"},
                {"end_date", "2015-02-15T17:00:00"},
                {"appointment_type", ""}
            });
            print_response(resp, "appointments");
        }
        public static void EligibilityExample(PlatformClient client)
        {
            var resp = client.eligibility(
                new Dictionary<string, object>
                {
                    {
                        "member", new Dictionary<string, object>
                        {
                            {"id", "W000000000"},
                            {"birth_date", "1970-01-25"},
                            {"first_name", "Jane"},
                            {"last_name", "Doe"}
                        }
                    },
                    {
                        "provider", new Dictionary<string, object>
                        {
                            {"npi", "1467560003"},
                            {"last_name", "AYA-AY"},
                            {"first_name", "JEROME"}
                        }
                    },
                    {"service_types", new string[] {"health_benefit_plan_coverage"}},
                    {"trading_partner_id", "MOCKPAYER"}
                });
            print_response(resp);
        }
        public static string OopLoadPriceExample(PlatformClient client)
        {
            // Return a string to use in the next example to delete
            // the price loaded in this example.
            var resp = client.oopInsuranceLoadPrice(new Dictionary<string, object>
            {
                {"price", new Dictionary<string, string>{{"amount", "750"}}},
                {"cpt_bundle", new string[] {"99385"}},
                {"trading_partner_id", "MOCKPAYER"}
            });

            JObject parsed = JObject.Parse(resp.body);
            var uuid = parsed["data"]["uuid"];

            return uuid.ToString();
        }
        public static void OopDeletePriceExample(PlatformClient client, string uuidToDelete)
        {
            // Must use trial / production application for this endpoint
            var resp = client.oopInsuranceDeletePrice(uuidToDelete);
            JObject parsed = JObject.Parse(resp.body);

            print_response(resp, "deleteCtpPriceBundle");
        }
        public static void OopInsuranceEstimateExample(PlatformClient client)
        {
            var resp = client.oopInsuranceEstimate(new Dictionary<string, object>()
            {
                {
                    "eligibility", new Dictionary<string, object>{
                        {
                            "member", new Dictionary<string, object> {
                                {"id", "W000000000"},
                                {"birth_date", "1970-01-25"},
                                {"first_name", "Jane"},
                                {"last_name", "Doe"}
                            }
                        },
                        {
                            "provider", new Dictionary<string, object> {
                                {"npi", "1467560003"},
                                {"last_name", "AYA-AY"},
                                {"first_name", "JEROME"}
                            }
                        }
                    }
                },
                {"cpt_bundle", new string[] { "99385" }},
                {"trading_partner_id", "MOCKPAYER"},
                {"zip_code", "29412"}
            });
            print_response(resp, "OOP Insurance Estimate");
        }
        public static void PharmacyFormularyExample(PlatformClient client)
        {
            var resp = client.pharmacyFormulary(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "H2256001"},
                {"drug",  "virazole"}
            });
            print_response(resp, "formulary");
        }
        public static void PharmacyNetworkExample(PlatformClient client)
        {
            // Search for in-network pharmacies
            var resp = client.pharmacyNetwork(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "S5601034"},
                {"pharmacy_type", "retail"}
            });
            print_response(resp, "network");

            // Check if pharmacy is in network using NPI
            resp = client.pharmacyNetwork("1275827032",
                new Dictionary<string, string> {
                    {"trading_partner_id", "medicare_national"},
                    {"plan_number", "S5601034"},
                });
            print_response(resp, "networkNPI");
        }
        public static void PharmacyPlansExample(PlatformClient client)
        {
            var resp = client.pharmacyPlans(new Dictionary<string, string> {
                {"trading_partner_id", "medicare_national"},
                {"plan_number", "S5601034"}
            });
            print_response(resp, "plans");
        }
        public static void ProvidersExample(PlatformClient client)
        {
            var resp = client.providers("1467560003");
            print_response(resp, "provider");
        }
        public static void ValidateIdentityExample(PlatformClient client)
        {
            var resp = client.validateIdentity(new Dictionary<string, object>
            {
                {"first_name", "Oscar"},
                {"middle_name", "Harold"},
                {"last_name", "Whitmire"},
                {"ssn", "123456789"},
                {
                    "birth_date", new Dictionary<string, int>
                    {
                        {"year", 1989},
                        {"month", 12},
                        {"day", 07}
                    }
                },
                {"email", "oscar@pokitdok.com"},
                {"phone", "555-555-5555"},
                {
                    "address", new Dictionary<string, string>
                    {
                        {"street1", "1400 Anyhoo Avenue"},
                        {"street2", "Apt 15"},
                        {"city", "Springfield"},
                        {"state_or_province", "IL"},
                        {"postal_code", "90210"},
                        {"country_code", "US"}
                    }
                }
            });
            print_response(resp);
        }
        public static void AnswerProofQuestionExample(PlatformClient client)
        {
            var res = client.answerProofQuestion(new Dictionary<string, object> {
                {"questionnaire_id", "a9ec1381-5ad6-499b-a3a6-644ece186363"},
                {"question_id", 1},
                {"answer",  2 }
            });
            print_response(res);
        }
        public static void CreateProofQuestionExample(PlatformClient client)
        {
            var res = client.createProofQuestion(new Dictionary<string, object>
            {
                {"first_name", "Oscar"},
                {"middle_name", "Harold"},
                {"last_name", "Whitmire"},
                {"ssn", "123456789"},
                {
                    "birth_date", new Dictionary<string, int>
                    {
                        {"year", 1989},
                        {"month", 12},
                        {"day", 07}
                    }
                },
                {"email", "oscar@pokitdok.com"},
                {"phone", "555-555-5555"},
                {
                    "address", new Dictionary<string, string>
                    {
                        {"street1", "1400 Anyhoo Avenue"},
                        {"street2", "Apt 15"},
                        {"city", "Springfield"},
                        {"state_or_province", "IL"},
                        {"postal_code", "90210"},
                        {"country_code", "US"}
                    }
                }
            });

        }

        #endregion

        static void Main(string[] args)
        {

            PlatformClient client = new PlatformClient(
                clientId: "",
                clientSecret: ""
            );

            EligibilityExample(client);

            client.Dispose();

        }

    }
}
