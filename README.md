pokitdok-csharp
===============

PokitDok [Platform API][apidocs] Client for C#

## Installation

Install via [NuGet][nuget]:
```
	PM> Install-Package PokitDokPlatformClient
```

Build source with [Xamarin][xamarin] Studio 5.0.
See dependencies section below.

[nuget]: https://www.nuget.org/packages/PokitDokPlatformClient
[xamarin]: http://xamarin.com/

## Tests
nunit-console.exe bin/Debug/pokitdok-csharp.exe

## Resources
* [Read the PokitDok API docs][apidocs]
* [View Source on GitHub][code]
* [Report Issues on GitHub][issues]

[apidocs]: https://platform.pokitdok.com/dashboard#/documentation
[code]: https://github.com/PokitDokInc/pokitdok-csharp
[issues]: https://github.com/PokitDokInc/pokitdok-csharp/issues

## Usage Example

	using pokitdokcsharp;

	PlatformClient client = new PlatformClient("your client id", "your client secret");
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
	Console.WriteLine(resp.body);


## Supported .Net Versions
This library aims to support and is tested against these .Net (ECMA-335 CLI) framework versions:

* Microsoft.Net 4.0
* Mono 3.4.0

You may have luck with other CLI frameworks - let us know how it goes.

## Dependencies
The PokitDok Platform API C# Client requires [Json.Net][jnk], a popular high-performance JSON framework for .NET.
* [Json.Net 6.0.3][json.net].

Tests written using [NUnit][nunit].
* [NUnit 2.6.3][nunit263]

[jnk]: http://james.newtonking.com/json
[json.net]: https://github.com/JamesNK/Newtonsoft.Json/tree/6.0.3
[nunit]: http://www.nunit.org/index.php?p=home
[nunit263]: http://launchpad.net/nunitv2/trunk/2.6.3/+download/NUnit-2.6.3.zip

## License
Copyright (c) 2014 PokitDok Inc. See [LICENSE][license] for details.

[license]: LICENSE.txt