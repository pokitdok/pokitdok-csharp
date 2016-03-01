using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pokitdokcsharp.tests.data
{
    public static class ExampleRequests
    {
        // Example requests
        public static Dictionary<string, object> EligibilityRequest
        {
            get
            {
                return
                new Dictionary<string, object> {
                 {"member", new Dictionary<string, object> {
                    {"id", "W000000000"},
                    {"birth_date", "1970-01-01"},
                    {"first_name", "Jane"},
                    {"last_name", "Doe"}
                    }},
                {"provider", new Dictionary<string, object> {
                    {"npi", "1467560003"},
                    {"last_name", "AYA-AY"},
                    {"first_name", "JEROME"}
                    }},
                {"service_types", new string[] { "health_benefit_plan_coverage" }},
                {"trading_partner_id", "MOCKPAYER"}
            };
            }
        }

        public static Dictionary<string, object> ClaimsRequest
        {
            get
            {
                return
                new Dictionary<string, object> {
                {"transaction_code", "chargeable"},
                {"trading_partner_id", "MOCKPAYER"},
                {"billing_provider", new Dictionary<string, object> {
                        {"taxonomy_code", "207Q00000X"},
                        {"first_name", "Jerome"},
                        {"last_name", "Aya-Ay"},
                        {"npi", "1467560003"},
                        {"address", new Dictionary<string, object> {
                                {"address_lines", new string[] { "8311 WARREN H ABERNATHY HWY" }},
                                {"city", "SPARTANBURG"},
                                {"state", "SC"},
                                {"zipcode", "29301"}
                            }},
                        {"tax_id", "123456789"}
                    }},
                {"subscriber", new Dictionary<string, object> {
                        {"first_name", "Jane"},
                        {"last_name", "Doe"},
                        {"member_id", "W000000000"},
                        {"address", new Dictionary<string, object> {
                                {"address_lines", new string[] { "123 N MAIN ST" }},
                                {"city", "SPARTANBURG"},
                                {"state", "SC"},
                                {"zipcode", "29301"}
                            }},
                        {"birth_date", "1970-01-01"},
                        {"gender", "female"}
                    }},
                {"claim", new Dictionary<string, object> {
                        {"total_charge_amount", 60.0},
                        {"service_lines", new object[] {
                                new Dictionary<string, object> {
                                    {"procedure_code", "99213"},
                                    {"charge_amount", 60.0},
                                    {"unit_count", 1.0},
                                    {"diagnosis_codes", new string[] { "487.1" }},
                                    {"service_date", "2014-06-01"}
                    }}}}}
                };
            }
        }

        public static Dictionary<string, object> ClaimsStatusRequest
        {
            get
            {
                return
                new Dictionary<string, object> {
                {"patient", new Dictionary<string, object> {
                        {"id", "W000000000"},
                        {"birth_date", "1970-01-01"},
                        {"first_name", "Jane"},
                        {"last_name", "Doe"}
                    }},
                {"provider", new Dictionary<string, object> {
                        {"npi", "1467560003"},
                        {"last_name", "AYA-AY"},
                        {"first_name", "JEROME"}
                    }},
                {"service_date", "2014-01-01"},
                {"trading_partner_id", "MOCKPAYER"}
                };
            }
        }

        public static Dictionary<string, string> ProvidersRequest
        {
            get
            {
                return
                new Dictionary<string, string> {
                { "zipcode", "29307" },
                { "radius", "15mi" }
            };
            }
        }

        public static Dictionary<string, string> PlansRequest
        {
            get
            {
                return
                new Dictionary<string, string> {
                { "state", "TX" },
                { "plan_type", "PPO" }
                };
            }
        }

        public static Dictionary<string, string> PricesRequest
        {
            get
            {
                return
                new Dictionary<string, string> {
                { "zip_code", "30012" },
                { "cpt_code", "88142" }
                };
            }
        }

        public static List<string> FilesRequest
        {
            get
            {
                return new List<string> { "MOCKPAYER", "../../tests/files/general-physician-office-visit.270" };
            }

        }

        public static Dictionary<string, object> ReferralsRequest
        {
            get
            {
                return
                new Dictionary<string, object> {
                {"event", new Dictionary<string, object> {
                        {"category", "specialty_care_review"},
                        {"certification_type", "initial"},
                        {"delivery", new Dictionary<string, object> {
                            {"quantity", 1},
                            {"quantity_qualifier", "visits"}
                            }},
                        {"diagnoses", new Dictionary<string, string>[] {
                            new Dictionary<string, string> {
                                {"code", "384.20"},
                                {"date", "2014-09-30"}
                            }}},
                        {"place_of_service", "office"},
                        {"provider", new Dictionary<string, string> {
                            {"first_name", "JOHN"},
                            {"npi", "1154387751"},
                            {"last_name", "FOSTER"},
                            {"phone", "8645822900"}
                            }},
                        {"type", "consultation"},
                    }},
                {"patient", new Dictionary<string, string> {
                        {"birth_date", "1970-01-01"},
                        {"first_name", "JANE"},
                        {"last_name", "DOE"},
                        {"id", "1234567890"}
                    }},
                {"provider", new Dictionary<string, string> {
                        {"first_name", "CHRISTINA"},
                        {"last_name", "BERTOLAMI"},
                        {"npi", "1619131232"}
                    }},
                {"trading_partner_id", "MOCKPAYER"}
                };
            }
        }

        public static Dictionary<string, object> AuthorizationsRequet
        {
            get
            {
                return
                new Dictionary<string, object> {
                {"event", new Dictionary<string, object> {
                    {"category", "health_services_review"},
                    {"certification_type", "initial"},
                    {"delivery", new Dictionary<string, object> {
                        {"quantity", 1},
                        {"quantity_qualifier", "visits"}
                            }},
                        {"diagnoses", new Dictionary<string, object>[] {
                            new Dictionary<string, object> {
                                {"code", "789.00"},
                                {"date", "2014-10-01"}
                            }}},
                    {"place_of_service", "office"},
                    {"provider", new Dictionary<string, object> {
                            {"organization_name", "KELLY ULTRASOUND CENTER, LLC"},
                            {"npi", "1760779011"},
                            {"phone", "8642341234"}
                        }},
                    {"services", new Dictionary<string, object>[] {
                        new Dictionary<string, object> {
                            {"cpt_code", "76700"},
                            {"measurement", "unit"},
                            {"quantity", 1}
                        }}},
                    {"type", "diagnostic_imaging"}
                    }},
                {"patient", new Dictionary<string, object> {
                        {"birth_date", "1970-01-01"},
                        {"first_name", "JANE"},
                        {"last_name", "DOE"},
                        {"id", "1234567890"}
                    }},
                {"provider", new Dictionary<string, object> {
                        {"first_name", "JEROME"},
                        {"npi", "1467560003"},
                        {"last_name", "AYA-AY"}
                }},
                {"trading_partner_id", "MOCKPAYER"}
                };

            }
        }

        public static Dictionary<string, string> AppointmentsRequest
        {
            get
            {
                return
                new Dictionary<string, string> {
                {"appointment_type", "SS1"},
                {"start_date", "2015-01-14T08:00:00"},
                {"end_date", "2015-01-16T17:00:00"},
                {"patient_uuid", "8ae236ff-9ccc-44b0-8717-42653cd719d0"}
                };
            }
        }

        public static Dictionary<string, object> BookAppointmentRequest
        {
            get
            {
                return
                new Dictionary<string, object> {
                {"patient", new Dictionary<string, object> {
                    {"uuid", "500ef469-2767-4901-b705-425e9b6f7f83"},
                    {"email", "john@johndoe.com"},
                    {"phone", "800-555-1212"},
                    {"birth_date", "1970-01-01"},
                    {"first_name", "John"},
                    {"last_name", "Doe"},
                    {"member_id", "M000001"}}}
                };
            }
        }

        public static string MpcRequest
        {
            get
            {
                return "99211";
            }
        }

        public static Dictionary<string, object> CreateIdentityRequest
        {
            get
            {
                return new Dictionary<string, object> {
            { "prefix", "Mr." } ,
            { "first_name", "Oscar"},
            { "middle_name", "Harold"},
            { "last_name", "Whitmire"},
            { "suffix", "IV"},
            { "birth_date", "2000-05-01"},
            { "gender", "male"},
            { "email", "oscar@pokitdok.com"},
            { "phone", "555-555-5555"},
            { "secondary_phone", "333-333-4444"},
            { "address", new Dictionary<string, object> {
               { "address_lines", new List<string>() { "1400 Anyhoo Avenue"} } ,
               { "city", "Springfield" },
               { "state", "IL" },
               { "zipcode", "90210" }
              }
            },
            {"identifiers", new List<Dictionary<string, object>>
              {
                new Dictionary<string, object>
                {
                   { "location", new List<double> {-121.93831, 37.53901 } },
                   { "provider_uuid", "1917f12b-fb6a-4016-93bc-adeb83204c83" },
                   { "system_uuid", "967d207f-b024-41cc-8cac-89575a1f6fef" },
                   { "value", "W90100-IG-88" }
                }
              }
            }
           };

            }
        }

        public static Dictionary<string, object> BenefitsEnrollmentRequest
        {
            get
            {
                return
                    new Dictionary<string, object>
                    {
                    { "action", "Change"},
                     {"dependents", new List<string> { } },
                     {"master_policy_number", "ABCD012354"},
                     { "payer", new Dictionary<string, object> {
                            { "tax_id", "654456654" }
                         }
                     },
                     {   "purpose", "Original"},
                     {   "sponsor", new Dictionary<string, object> {
                            {"tax_id", "999888777" }
                     } },
                    { "subscriber", new Dictionary<string, object> {
                            { "address", new Dictionary<string, object> {
                            { "city", "CAMP HILL"},
                            { "county", "CUMBERLAND"},
                            { "line", "100 MARKET ST"},
                            { "line2", "APT 3G"},
                            { "postal_code", "17011"},
                            { "state", "PA" }
                            }},
                            { "benefit_status", "Active"},
                            { "benefits", new List<Dictionary<string, object>> {
                                            new Dictionary<string, object> {
                                                { "begin_date", "2015-01-01"},
                                                { "benefit_type", "Health"},
                                                { "coordination_of_benefits", new List<Dictionary<string, object>> {
                                                    new Dictionary<string, object> {
                                                            { "group_or_policy_number", "890111"},
                                                            { "payer_responsibility", "Primary"},
                                                            { "status", "Unknown" }
                                                        }
                                                    }
                                                },
                                                { "late_enrollment", false},
                                                { "maintenance_type", "Addition" }
                                            }, // benefits[0]
                                            new Dictionary<string, object> {
                                                { "begin_date", "2015-01-01"},
                                                { "benefit_type", "Dental"},
                                                { "late_enrollment", false},
                                                { "maintenance_type", "Addition" }
                                                },
                                            new Dictionary<string, object> {
                                                { "begin_date", "2015-01-01"},
                                                { "benefit_type", "Vision"},
                                                { "late_enrollment", false},
                                                { "maintenance_type", "Addition" }
                                                }} // End list
                                },
                                { "birth_date", "1940-01-01"},
                                { "contacts", new List<Dictionary<string, object>> {
                                        new Dictionary<string, object> {
                                            { "communication_number2", "7172341240"},
                                            { "communication_type2", "Work Phone Number"},
                                            { "primary_communication_number", "7172343334"},
                                            { "primary_communication_type", "Home Phone Number" }
                                            }
                                } },
                                { "eligibility_begin_date", "2014-01-01"},
                                { "employment_status", "Full-time"},
                                { "first_name", "JOHN"},
                                { "gender", "Male"},
                                { "group_or_policy_number", "123456001"},
                                { "handicapped", false},
                                { "last_name", "DOE"},
                                { "maintenance_reason", "Active"},
                                { "maintenance_type", "Addition"},
                                { "member_id", "123456789"},
                                { "middle_name", "P"},
                                { "relationship", "Self"},
                                { "ssn", "123456789"},
                                { "subscriber_number", "123456789"},
                                { "substance_abuse", false},
                                { "tobacco_use", false
                            }} },
                            { "trading_partner_id", "MOCKPAYER" }
                    };
            }
        }



    }
}
