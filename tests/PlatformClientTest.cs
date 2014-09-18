﻿using System;
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
		client = new PlatformClient("s6g5HVrcHfUDc4GDRTMQ", "L121rl427P1USFi5s1u65wZ3wF39dltWEg8UGduw");
//		client.ApiSite = "http://me.pokitdok.com:5002";
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

		StringAssert.Contains("\"payer\": {\"id\": \"MOCKPAYER\", \"name\": \"MOCK PAYER INC\"}", resp.body);
		StringAssert.Contains("\"service_types\": [\"professional_physician_visit_office\"]", resp.body);
		Assert.AreEqual(200, resp.status);
	}

	/// <summary>
	/// Payers this instance.
	/// </summary>
	[Test]
	public void Payers()
	{
		ResponseData resp = client.payers();

		StringAssert.Contains("\"payer_name\": \"Mock Payer for Testing\", \"payer_key\": \"MOCKPAYER\", \"production_status\": false, \"trading_partner_id\": \"MOCKPAYER\"", resp.body);
		Assert.AreEqual(200, resp.status);
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
					}}}}},
				{"payer", 
					new Dictionary<string, object> {
						{"organization_name", "Acme Ins Co"},
						{"plan_id", "1234567890"}
					}}
			});

		StringAssert.Contains("\"units_of_work\": 1, \"name\": \"claims\"", resp.body);
		Assert.AreEqual(200, resp.status);
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
	}

	/// <summary>
	/// Activities test.
	/// </summary>
	[Test]
	public void Activities()
	{
		ResponseData resp = client.activities();

		StringAssert.Contains("\"units_of_work\"", resp.body);
		Assert.AreEqual(200, resp.status);
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

		StringAssert.Contains("\"provider\"", resp.body);
		Assert.AreEqual(200, resp.status);
	}

	/// <summary>
	/// Providers identifier test.
	/// </summary>
	[Test]
	public void ProvidersId()
	{
		ResponseData resp = client.providers("1467560003");

		StringAssert.Contains("\"first_name\": \"JEROME\", \"last_name\": \"AYA-AY\", "+
			"\"middle_name\": \"BENITEZ\"", resp.body);
		Assert.AreEqual(200, resp.status);
	}

	///<summary>
	/// Trading Partners list test.
	/// </summary>
	[Test]
	public void TradingPartnersList()
	{
		ResponseData resp = client.tradingPartners("");

		Assert.AreEqual(200, resp.status);
		StringAssert.Contains ("MOCKPAYER", resp.body);
		StringAssert.Contains ("MOCKPAYER_ACK", resp.body);
		StringAssert.Contains ("MOCKPAYER_REJECTION", resp.body);
	}

	///<summary>
	/// Trading Partners specific id test.
	/// </summary>
	[Test]
	public void TradingPartnersGet()
	{
		ResponseData resp = client.tradingPartners("MOCKPAYER");

		Assert.AreEqual(200, resp.status);
		StringAssert.Contains("\"id\": \"MOCKPAYER\", \"name\": \"Mock Payer for Testing\"", resp.body);
	}

	///<summary>
	/// Plans no argument test.
	/// </summary>
	[Test]
	public void PlansNoArgs()
	{
		ResponseData resp = client.plans();

		Assert.AreEqual(200, resp.status);
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

		StringAssert.Contains("\"units_of_work\": 1, \"name\": \"batch file\"", resp.body);
		Assert.AreEqual(200, resp.status);
	}
}
