using System;
using System.Collections.Generic;
using System.IO;
using pokitdokcsharp;
using NUnit.Framework;

/// <summary>
/// Platform client test.
/// </summary>
[TestFixture]
class PlatformClientTest
{
	PlatformClient client;

	/// <summary>
	/// Init this instance.
	/// </summary>
	[SetUp]
	public void Init()
	{
		client = new PlatformClient(
            "dB6bOvgCNOAhKt5lWir1",
            "aLSjaEkr9H2f74Zk2lTB6yPlaL5j1sH53wv0gzp0"
		);
		client.ApiSite = "https://platform.pokitdok.com";
		client.Authenticate();
	}

	/// <summary>
	/// Eligibility test using MOCKPAYER.
	/// </summary>
	[Test]
	public void Eligibility()
	{
		ResponseData resp = client.eligibility (
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
		});

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual("MOCKPAYER", (String) client.Data["payer"]["id"]);
		Assert.AreEqual("MOCK PAYER INC", (String) client.Data["payer"]["name"]);
	}

	/// <summary>
	/// Claims test using MOCKPAYER.
	/// </summary>
	[Test]
	public void Claims()
	{
		ResponseData resp = client.claims (
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
			});

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual(1, (int) client.Data["units_of_work"]);
		Assert.AreEqual("claims", (String) client.Data["name"]);
	}

	[Test]
	public void ClaimsStatus()
	{
		ResponseData resp = client.claimsStatus(
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
			});

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual(false, (Boolean) client.Data["patient"]["claims"][0]["applied_to_deductible"]);
	}

	/// <summary>
	/// Activities test.
	/// </summary>
	[Test]
	public void Activities()
	{
		ResponseData resp = client.activities();

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual(1, (int) client.Data[0]["units_of_work"]);
	}

	/// <summary>
	/// Providers JSON test.
	/// </summary>
	[Test]
	public void ProvidersJson()
	{
		ResponseData resp = client.providers(
			new Dictionary<string, string> {
				{ "zipcode", "29307" },
				{ "radius", "15mi" }
		});

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual("JENTA", (String) client.Data[0]["provider"]["first_name"]);
		Assert.AreEqual("SHEN", (String) client.Data[0]["provider"]["last_name"]);
	}

	/// <summary>
	/// Providers identifier test.
	/// </summary>
	[Test]
	public void ProvidersId()
	{
		ResponseData resp = client.providers("1467560003");

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual("JENTA", (String) client.Data["provider"]["first_name"]);
		Assert.AreEqual("SHEN", (String) client.Data["provider"]["last_name"]);
	}

	///<summary>
	/// Trading Partners list test.
	/// </summary>
	[Test]
	public void TradingPartnersList()
	{
		ResponseData resp = client.tradingPartners();
		Assert.AreEqual(200, resp.status);
	}

	///<summary>
	/// Trading Partners specific id test.
	/// </summary>
	[Test]
	public void TradingPartnersGet()
	{
		ResponseData resp = client.tradingPartners("MOCKPAYER");
		Assert.AreEqual(200, resp.status);
	}

	///<summary>
	/// Plans no argument test.
	/// </summary>
	[Test]
	public void PlansNoArgs()
	{
		ResponseData resp = client.plans();

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual("PPO", (String) client.Data[0]["plan_type"]);
	}

	///<summary>
	/// Plans test.
	/// </summary>
	[Test]
	public void Plans()
	{
		ResponseData resp = client.plans(
			new Dictionary<string, string> {
				{ "state", "TX" },
				{ "plan_type", "PPO" }
			}
		);

		Assert.AreEqual(200, resp.status);
	}

	/// <summary>
	/// Cash Prices test.
	/// </summary>
	[Test]
	public void PricesCash()
	{
		ResponseData resp = client.pricesCash(
			new Dictionary<string, string> {
				{ "zip_code", "30012" },
				{ "cpt_code", "88142" }
			});

		StringAssert.Contains("high", resp.body);
		Assert.AreEqual(200, resp.status);
	}

	/// <summary>
	/// Insurance Prices test.
	/// </summary>
	[Test]
	public void PricesInsurance()
	{
		ResponseData resp = client.pricesInsurance(
			new Dictionary<string, string> {
				{ "zip_code", "32218" },
				{ "cpt_code", "87799" }
			});
		StringAssert.Contains("amounts", resp.body);
		Assert.AreEqual(200, resp.status);
	}

	/// <summary>
	/// Usage test.
	/// </summary>
	[Test]
	public void Usage()
	{
		dynamic usage = client.usage();

		Assert.IsInstanceOf<Newtonsoft.Json.Linq.JValue>(usage.rate_limit_amount);
		Assert.IsInstanceOf<Newtonsoft.Json.Linq.JValue>(usage.rate_limit_reset);
		Assert.IsInstanceOf<Newtonsoft.Json.Linq.JValue>(usage.rate_limit_cap);
		Assert.IsInstanceOf<Newtonsoft.Json.Linq.JValue>(usage.credits_remaining);
		Assert.IsInstanceOf<Newtonsoft.Json.Linq.JValue>(usage.credits_billed);
	}

	/// <summary>
	/// Files test.
	/// </summary>
	[Test]
	public void Files()
	{
		ResponseData resp = client.files(
			"MOCKPAYER",
			"../../tests/files/general-physician-office-visit.270"
		);

        Assert.AreEqual(200, resp.status);
        StringAssert.Contains("\"units_of_work\": 1, \"_type\": \"PlatformActivityModel\", \"name\": \"batch file\"", resp.body);
	}


	/// <summary>
	/// Referrals this instance.
	/// </summary>
	[Test]
	public void Referrals()
	{
		ResponseData resp = client.referrals(
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
			});

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual("AUTH0001", (String) client.Data["event"]["review"]["certification_number"]);
		Assert.AreEqual("certified_in_total", (String) client.Data["event"]["review"]["certification_action"]);
	}

	/// <summary>
	/// Authorizations this instance.
	/// </summary>
	[Test]
	public void Authorizations()
	{
		ResponseData resp = client.authorizations(
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
			});

		Assert.AreEqual(200, resp.status);
		Assert.AreEqual("AUTH0001", (String) client.Data["event"]["review"]["certification_number"]);
		Assert.AreEqual("certified_in_total", (String) client.Data["event"]["review"]["certification_action"]);
	}

    /// <summary>
    /// Test retrieving Schedulers list and by uuid
    /// </summary>
    [Test]
    public void Schedulers()
    {
        ResponseData resp = client.schedulers();
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("PokitDok", client.Data[0]["name"].ToString());
		Assert.AreEqual("Greenway", client.Data[1]["name"].ToString());

		resp = client.schedulers("967d207f-b024-41cc-8cac-89575a1f6fef");
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("967d207f-b024-41cc-8cac-89575a1f6fef", client.Data[0]["scheduler_uuid"].ToString());
    }

    /// <summary>
    /// Test retrieving AppointmentTypes list and by uuid
    /// </summary>
    [Test]
    public void AppointmentTypes()
    {
        ResponseData resp = client.appointmentTypes();
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("a3a45130-4adb-4d2c-9411-85a9d9ac4aa2", (String) client.Data[0]["appointment_type_uuid"]);

		resp = client.appointmentTypes("a3a45130-4adb-4d2c-9411-85a9d9ac4aa2");
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("a3a45130-4adb-4d2c-9411-85a9d9ac4aa2", (String) client.Data[0]["appointment_type_uuid"]);
    }

    /// <summary>
    /// Test querying for Appointments and getting by a uuid
    /// </summary>
    [Test]
    public void Appointments()
    {
        ResponseData resp = client.appointments(
            new Dictionary<string, string> {
                {"appointment_type", "SS1"},
                {"start_date", "2015-01-14T08:00:00"},
                {"end_date", "2015-01-16T17:00:00"},
                {"patient_uuid", "8ae236ff-9ccc-44b0-8717-42653cd719d0"}
            });
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("SS1", (String) client.Data[0]["appointment_type"]);
		Assert.AreEqual("01/14/2015 08:00:00", (String) client.Data[0]["start_date"]);

		resp = client.appointments("bf8440b1-fd20-4994-bb28-e3981833e796");
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("bf8440b1-fd20-4994-bb28-e3981833e796", (String) client.Data[0]["pd_appointment_uuid"]);
		Assert.AreEqual("OV1", (String) client.Data[0]["appointment_type"]);
		Assert.AreEqual("peg@emailprovider.com", (String) client.Data[0]["patient"]["email"]);
    }

    /// <summary>
    /// Test booking an appointment
    /// </summary>
    [Test]
    public void BookAppointment()
    {
        ResponseData resp = client.bookAppointment(
            "ef987691-0a19-447f-814d-f8f3abbf4859", 
            new Dictionary<string, object> {
                {"patient", new Dictionary<string, object> {
                    {"uuid", "500ef469-2767-4901-b705-425e9b6f7f83"},
                    {"email", "john@johndoe.com"},
                    {"phone", "800-555-1212"},
                    {"birth_date", "1970-01-01"},
                    {"first_name", "John"},
                    {"last_name", "Doe"},
                    {"member_id", "M000001"}}}
            });
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("OV1", (String) client.Data["appointment_type"]);
		Assert.AreEqual("john@johndoe.com", (String) client.Data["patient"]["email"]);
		Assert.AreEqual(true, (bool) client.Data["booked"]);
    }

    /// <summary>
    /// Test updating an appointment description
    /// </summary>
    [Test]
    public void UpdateAppointment()
    {
        ResponseData resp = client.updateAppointment(
            "ef987691-0a19-447f-814d-f8f3abbf4859",
            new Dictionary<string, object> { 
                {"description", "Welcome to M0d3rN Healthcare"}
            });
        Assert.AreEqual(200, resp.status);
		Assert.AreEqual("OV1", (String) client.Data["appointment_type"]);
		Assert.AreEqual(false, (bool) client.Data["booked"]);
		Assert.AreEqual("Welcome to M0d3rN Healthcare", (String) client.Data["description"]);
    }

    /// <summary>
    /// Test canceliing an appointment
    /// </summary>
    [Test]
    public void CancelAppointment()
    {
        ResponseData resp = client.cancelAppointment("ef987691-0a19-447f-814d-f8f3abbf4859");
        Assert.AreEqual(204, resp.status);       
    }

    [Test]
    public void MedicalProcedureCode()
    {
        ResponseData resp = client.medicalProcedureCode("99211");
        Assert.AreEqual(200, resp.status);
        Assert.AreEqual("Established patient office or other outpatient visit, typically 15 minutes", (String) client.Data["name"]);
        resp = client.medicalProcedureCode(
            new Dictionary<string, string> { 
                {"name", "Established patient office or other outpatient visit, typically 15 minutes"}
            });
        Assert.AreEqual("99211", (String) client.Data[0]["code"]);
    }
}
