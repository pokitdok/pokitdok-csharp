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
		client = new PlatformClient ("LNrngr9X4zkwAPdwI8uf", "htr5ckvvhc9g83qqlapGt5APJE95a3yEsBZhUezV");
		client.ApiSite = "http://me.pokitdok.com:5002";
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
				{ "payer_id", "MOCKPAYER" },
				{ "member_id", "W34237875729" },
				{ "provider_id", "1467560003" },
				{ "provider_name", "AYA-AY" },
				{ "provider_first_name", "JEROME" },
				{ "member_name", "JOHN DOE" },
				{ "provider_type", "Person" },
				{ "member_birth_date", "05/21/1975" },
				{ "service_types", new string[] { "Health Benefit Plan Coverage" } }
		});

		StringAssert.Contains("\"provider_id\": \"1467560003\"", resp.body);
		StringAssert.Contains("\"payer_name\": \"MOCK PAYER INC\"", resp.body);
		StringAssert.Contains("\"member_birth_date\": \"Wed May 21 00:00:00 1975\"", resp.body);
		Assert.AreEqual(200, resp.status);
	}

	/// <summary>
	/// Activities test.
	/// </summary>
	[Test]
	public void Activities()
	{
		ResponseData resp = client.activities("");

		StringAssert.Contains("\"_type\": \"PlatformActivityModel\"", resp.body);
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
				{ "zip_code", "29464" },
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

		StringAssert.Contains("\"first_name\": \"JEROME\", \"last_name\": \"AYA-AY\", \"middle_name\": \"BENITEZ\"", resp.body);
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
		Assert.IsInstanceOf<Newtonsoft.Json.Linq.JValue>(usage.test_mode);
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
