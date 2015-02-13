pokitdok-csharp
===============

PokitDok [Platform API][apisite] Client for C#

## Installation

Install via [NuGet][nuget]:
```
PM> Install-Package PokitDokPlatformClient
```

Build source with [Xamarin][xamarin] Studio 5.7.1 or [Microsoft Visual Studio 2013][msvs].
See dependencies section below.

[nuget]: https://www.nuget.org/packages/PokitDokPlatformClient
[xamarin]: http://xamarin.com/
[msvs]: http://www.visualstudio.com/

## Tests
```
nunit-console.exe bin/Debug/pokitdok-csharp.dll
```

## Resources
* [Read the PokitDok API docs][apidocs]
* [View Source on GitHub][code]
* [Report Issues on GitHub][issues]

[apisite]: https://platform.pokitdok.com/
[apidocs]: https://platform.pokitdok.com/documentation/v4#/
[code]: https://github.com/pokitdok/pokitdok-csharp
[issues]: https://github.com/pokitdok/pokitdok-csharp/issues

## Quick Start

###Example Client
```c#
// Initialize the client
using System;
using System.Collections.Generic;
using System.IO;
using pokitdokcsharp;

class MainClass
{
	public static void Main (string[] args)
	{
		PlatformClient client = new PlatformClient("your api client id", "your api client secret");
		// ... client code
	}
}
```

###Example Method Calls
```c#
// Retrieve cash price information by zip and CPT code
client.pricesCash(
	new Dictionary<string, string> {
		{ "zip_code", "32218" },
		{ "cpt_code", "87799" }
});

// Retrieve insurance price information by zip and CPT code
client.pricesInsurance(
	new Dictionary<string, string> {
		{ "zip_code", "32218" },
		{ "cpt_code", "87799" }
});

// Retrieve provider information by NPI
client.providers("1467560003");

// Search providers by name (individuals)
client.providers(
	new Dictionary<string, string> {
		{"first_name", "Jerome"},
		{"last_name", "Aya-Ay"}
});

// Search providers by name (organizations)
client.providers(
        new Dictionary<string, string> {
                {"organization_name", "Qliance"}
});

// Search providers by location and/or specialty
client.providers(
	new Dictionary<string, string> {
		{"zipcode", "29307"},
		{"radius", "15mi"}
});
client.providers(
	new Dictionary<string, string> {
		{"zipcode", "29307"},
		{"radius", "15mi"},
		{"specialty", "RHEUMATOLOGY"}
});

// Submit a v4 eligibility request
client.eligibility(
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

// Submit a v4 claims request
client.claims(
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

// Check the status of a claim
client.claimsStatus(
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

// Submit X12 files directly for processing on the platform
client.files("MOCKPAYER", "../../tests/files/general-physician-office-visit.270");

// Check on pending platform activities

// check on a specific activity
client.activities("5362b5a064da150ef6f2526c");

// check on a batch of activities
client.activities(new Dictionary<string, object> {
	{"parent_id", "537cd4b240b35755f5128d5c"}
});

// retrieve an index of activities
client.activities();
```

###Example Usage Pattern
```c#
try 
{
	PlatformClient client = new PlatformClient("client id", "client secret");

	// execute the /providers/{id} endpoint, capture the response and access results
	// https://platform.pokitdok.com/documentation/v4#providers
	ResponseData resp = client.providers("1467560003");
	if (resp.status == 200) // this is the HTTP status code
	{
		// response HTTP headers are in a Dictionary in the returned ResponseData.header property
		foreach (KeyValuePair<string, string> entry in resp.header)
		{
			Console.WriteLine("response header <"+ entry.Key +"> = "+ entry.Value);
		}

		// raw response body is in the returned ResponseData.body property
		Console.WriteLine("response body= "+ resp.body);

		// the "data" element of the response body is deserialized and stored in the client.Data property as a dynamic value
		Console.WriteLine("provider= "+ client.Data["provider"]["first_name"]);
	}
	else
	{
		Console.WriteLine("error= "+ client.Errors["message"]);
	}

	// execute the /providers/ endpoint, capture the response and access results
	// https://platform.pokitdok.com/documentation/v4#providers
	resp = client.providers(new Dictionary<string, string> {
		{"first_name", "Jerome"},
		{"last_name", "Aya-Ay"}
	});
	if (resp.status == 200) // this is the HTTP status code
	{
		// the "data" element of the response body is deserialized and stored in the client.Data property as a dynamic value
		foreach (dynamic data in client.Data)
		{
			Console.WriteLine("provider="+ data["provider"]["first_name"]);
		}
	}    
	else
	{
		Console.WriteLine("error= "+ client.Errors["message"]);
	}

} catch (Exception ex) {
	Console.WriteLine("Error: " + ex.Message);
}
```

## Tested .Net Versions
This library aims to support and is tested against these Microsoft.Net (ECMA-335 CLI) framework versions:

* [Microsoft.Net 4.0][msnet]
* [Mono 3.5.0][mono]

[msnet][http://www.microsoft.com/net]
[mono][http://www.mono-project.com/]

You may have luck with other CLI framework versions - let us know how it goes.

## Dependencies
The PokitDok Platform API C# Client requires Json.Net, a popular high-performance JSON framework for .NET.
* [Json.Net 6.0.8][json.net].

Tests written using NUnit].
* [NUnit 2.6.4][nunit]

[jnk]: http://james.newtonking.com/json
[json.net]: http://www.newtonsoft.com/json
[nunit]: https://www.nuget.org/packages/NUnit/

## License
Copyright (c) 2014 PokitDok Inc. See [LICENSE][license] for details.

[license]: LICENSE.txt
