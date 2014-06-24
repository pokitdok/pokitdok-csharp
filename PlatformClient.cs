// Copyright (C) 2014, All Rights Reserved, PokitDok, Inc.
// http://www.pokitdok.com
//
// Please see the LICENSE.txt file for more information.
// All other rights reserved.
//
//	PokitDok Platform Client for C#
//		Consume the REST based PokitDok platform API
//		https://platform.pokitdok.com/login#/documentation

using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;


namespace pokitdokcsharp
{
	/// <summary>
	///	PokitDok Platform Client for C#
	///		Consumes the REST based PokitDok platform API
	///		https://platform.pokitdok.com/login#/documentation
	/// </summary>
	public class PlatformClient: OauthApplicationClient
	{
		/// <summary>
		/// The default PokitDok API site url.
		/// </summary>
		public const string POKITDOK_PLATFORM_API_SITE = "https://platform.pokitdok.com";
		/// <summary>
		/// The default current PokitDok API version path.
		/// </summary>
		public const string POKITDOK_PLATFORM_API_VERSION_PATH = "/api/v4";
		/// <summary>
		/// The Oauth token path.
		/// </summary>
		public const string POKITDOK_PLATFORM_API_TOKEN_PATH = "/oauth2/token";

		private const string POKITDOK_PLATFORM_API_ENDPOINT_ELIGIBILITY = "/eligibility/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_PROVIDERS = "/providers/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS = "/claims/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS_STATUS = "/claims/status/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT = "/enrollment/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_DEDUCTIBLE = "/deductible/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_PAYERS = "/payers/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_PRICE_INSURANCE = "/price/insurance/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_PRICE_CASH = "/price/cash/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES = "/activities/";
		private const string POKITDOK_PLATFORM_API_ENDPOINT_FILES = "/files/";

		private string _apiSite = POKITDOK_PLATFORM_API_SITE;
		private string _versionPath = POKITDOK_PLATFORM_API_VERSION_PATH;
		private string _tokenPath = POKITDOK_PLATFORM_API_TOKEN_PATH;

		private dynamic _usage = null;
		private dynamic _data = null;
		private dynamic _errors = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="pokitdokcsharp.PlatformClient"/> class.
		/// </summary>
		/// <param name="clientId">Client identifier.</param>
		/// <param name="clientSecret">Client secret.</param>
		/// <param name="requestTimeout">Request timeout.</param>
		/// <param name="accessToken">Access token.</param>
		public PlatformClient(
			string clientId, 
			string clientSecret, 
			int requestTimeout = DEFAULT_TIMEOUT, 
			OauthAccessToken accessToken = null) : base(clientId, clientSecret, requestTimeout, accessToken)
		{
			init();

			this.ApiBaseUrl = _apiSite + _versionPath;
			this.ApiTokenUrl = _apiSite + _tokenPath;

			this.UserAgent = string.Format("csharp-pokitdok/{0}", typeof(PlatformClient).Assembly.GetName().Version);
		}

		/// <summary>
		/// Init this instance.
		/// </summary>
		public void init()
		{
			_usage = null;
			_data = null;
			_errors = null;
		}

		/// <summary>
		/// Call the activities endpoint for a specific activityId.
		/// </summary>
		/// <param name="activityId">Activity identifier.</param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData activities(string activityId)
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES + activityId));
		}

		/// <summary>
		/// Retrieve details for supported Payers.
		/// </summary>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData payers()
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PAYERS));
		}

		/// <summary>
		/// Call the activities endpoint to get a listing of current activities,
		/// a query string parameter ‘parent_id’ may also be used with this API to get information about 
		/// sub-activities that were initiated from a batch file upload.
		/// </summary>
		/// <param name="parameters">
		/// Query parameters:
		/// _id, {string} ID of this Activity
		/// name, {string} Activity name
		/// callback_url, {string} URL that will be invoked to notify the client application that this Activity has completed.  
		/// 	We recommend that you always use https for callback URLs used by your application.
		/// file_url, {string} URL where batch transactions that were uploaded to be processed within this activity are stored.  
		/// 	X12 files uploaded via the /files endpoint are stored here.
		/// history, {list} Historical status of the progress of this Activity
		/// state, {dict} Current state of this Activity
		/// transition_path, {list} The list of state transitions that will be used for this Activity.
		/// remaining_transitions, {list} The list of remaining state transitions that the activity has yet to go through.
		/// parameters, {dict} The parameters that were originally supplied to the activity
		/// units_of_work, {int} The number of ‘units of work’ that the activity is operating on.  
		/// 	This will typically be 1 for real-time requests like /eligibility.  
		/// 	When uploading batch X12 files via the /files endpoint, this will be the number of ‘transactions’ within 
		/// 	that file.  For example, if a client application POSTs a file of 20 eligibility requests to the /files API, 
		/// 	the units_of_work value for that activity will be 20 after the X12 file has been analyzed.  If an activity 
		/// 	does show a value greater than 1 for units_of_work, the client application can fetch detailed information 
		/// 	about each one of the activities processing those units of work by using the 
		/// 	/activities/?parent_id=&lt;activity_id&gt; API
		/// </param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData activities(Dictionary<string, string> parameters = null)
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES, parameters));
		}

		/// <summary>
		/// Return a list of cash prices for a given procedure (by CPT Code) in a given region (by ZIP Code).
		/// (Not yet implemented)
		/// </summary>
		/// <param name="parameters">
		/// Query parameters:
		/// 	cpt_code, {string} The CPT code of the procedure in question.
		///		zipcode, {string} Postal code in which to search for procedures
		/// </param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData priceCash(Dictionary<string, string> parameters = null)
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PRICE_CASH, parameters));
		}

		/// <summary>
		/// Return a list of insurance prices for a given procedure (by CPT Code) in a given region (by ZIP Code).
		/// (Not yet implemented)
		/// </summary>
		/// <returns>The insurance.</returns>
		/// <param name="parameters">
		/// Query parameters:
		/// 	cpt_code, {string} The CPT code of the procedure in question.
		///		zipcode, {string} Postal code in which to search for procedures
		/// </param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData priceInsurance(Dictionary<string, string> parameters = null)
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PRICE_INSURANCE, parameters));
		}

		/// <summary>
		/// Create a new claim, via the filing of an EDI 837 Professional Claims, to the designated Payer.
		/// (Not yet implemented)
		/// </summary>
		/// <param name="postData">Dictionary representing JSON post data.</param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData claims(Dictionary<string, object> postData)
		{
			init();

			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS, postData));
		}

		/// <summary>
		/// Ascertain the status of the specified claim, via the filing of an EDI 276 Claims Status.
		/// (Not yet implemented)
		/// </summary>
		/// <returns>The status.</returns>
		/// <param name="postData">Dictionary representing JSON post data.</param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData claimsStatus(Dictionary<string, object> postData)
		{
			init();

			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS_STATUS, postData));
		}

		/// <summary>
		/// Determine eligibility via an EDI 270 Request For Eligibility.
		/// </summary>
		/// <param name="postData">
		/// Dictionary Post data form fields:
		/// 	trading_partner_id, Unique id for the intended trading partner, as specified by the Payers resource.
		/// 	provider_id, Unique identifier for the provider , such as NPI, a PokitDok provider id, etc.
		/// 	member_id, The named insured’s member identifier
		/// 	member_name, The named insured’s name as specified on their policy
		/// 	member_birth_date, The named insured’s birth date as specified on their policy
		/// 	service_types, The line of coverage in the insurance policy this eligibility request is being made against
		/// </param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData eligibility(Dictionary<string, object> postData)
		{
			init();

			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_ELIGIBILITY, postData));
		}

		/// <summary>
		/// File an EDI 834 benefit enrollment.
		/// (Not yet implemented)
		/// </summary>
		/// <param name="postData">Post data.</param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData enrollment(Dictionary<string, object> postData)
		{
			init();

			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT, postData));
		}

		/// <summary>
		/// Retrieve the data for a specified provider.
		/// </summary>
		/// <param name="npi">Provider NPI identifier.</param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData providers(string npi)
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PROVIDERS + npi));
		}

		/// <summary>
		/// Get a list of providers meeting certain search criteria.
		/// </summary>
		/// <param name="parameters">
		/// Query parameters:
		/// 	name, Provider full name, any or all parts
		///		first_name, Provider first name
		///		middle_name, Provider middle name
		///		last_name, Provider first name
		///		specialty, Provider specialty name from NUCC/NPI taxonomy
		///		city, Provider city
		///		state, Provider state
		///		zipcode, Provider 5-digit zip code
		///		radius, Search distance from geographic centerpoint, with unit (e.g. “1mi” or “50mi”)
		///			(Only used when city, state, or zipcode is passed)
		///		gender, Provider gender
		///		npi_id, Provider NPI identifier
		/// </param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData providers(Dictionary<string, string> parameters)
		{
			init();

			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PROVIDERS, parameters));
		}

		/// <summary>
		/// Submit X12 formatted EDI file for batch processing.
		/// </summary>
		/// <param name="tradingPartnerId">Trading_partner_id.</param>
		/// <param name="filepath">Full filesytem path of file to be submitted.</param>
		/// <param name="callbackUrl">Optional notification url to be called when the asynchronous processing is complete.</param>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
		/// 	The body is JSON formatted data.
		/// </returns>
		public ResponseData files(string tradingPartnerId, string filepath, string callbackUrl = null)
		{
			init();

			Dictionary<string, string> postData = 
				new Dictionary<string, string> { { "trading_partner_id", tradingPartnerId } };
			if (callbackUrl != null) {
				postData.Add ("callback_url", callbackUrl);
			}

			return applyResponse(
				PostRequest(
					POKITDOK_PLATFORM_API_ENDPOINT_FILES, 
					filepath, 
					"file",
					"application/EDI-X12",
					postData
			));
		}

		/// <summary>
		/// Usage statistics for most recent request.
		/// </summary>
		/// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
		/// <returns>
		/// dynamic object with members:
		/// 	rate_limit_cap, {int} The amount of requests available per hour
		/// 	rate_limit_reset, {int} The time (Unix Timestamp) when the rate limit amount resets
		///		rate_limit_amount, {int} The amount of requests made during the current rate limit period
		///		credits_billed, {int} The amount of credits billed for this request
		///		credits_remaining, {int} The amount of credits remaining on your API account
		///		processing_time, {int} The time to process the request in milliseconds
		///		next, {string}, A url pointing to the next page of results
		///		previous, {string} A url pointing to the previous page of results 
		/// </returns>
		public dynamic usage()
		{
			if (_usage == null) {
				eligibility(new Dictionary<string, object> {
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
			}

			return _usage;
		}

		/// <summary>
		/// Applies the response data to extract usage, data and error members.
		/// </summary>
		/// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.</returns>
		/// <param name="response">The a <see cref="pokitdokcsharp.ResponseData"/> object returned from raw API calls.</param>
		private ResponseData applyResponse(ResponseData response)
		{
			dynamic responseObject = JsonConvert.DeserializeObject(response.body);
			_usage = responseObject["meta"];
			_data = responseObject["data"];

			if (_data is Newtonsoft.Json.Linq.JObject) {
				_errors = _data["errors"];
			} else {
				_errors = null;
			}

			return response;
		}

		/// <summary>
		/// Gets application API errors.
		/// </summary>
		/// <value>The errors.</value>
		public dynamic Errors {
			get {
				return this._errors;
			}
		}

		/// <summary>
		/// Gets application API data.
		/// </summary>
		/// <value>The data.</value>
		public dynamic Data {
			get {
				return this._data;
			}
		}

		/// <summary>
		/// Gets or sets the API base url site.
		/// </summary>
		/// <value>The API site.</value>
		public string ApiSite {
			get {
				return this._apiSite;
			}
			set {
				_apiSite = value;
				this.ApiBaseUrl = _apiSite + _versionPath;
				this.ApiTokenUrl = _apiSite + _tokenPath;
			}
		}

		/// <summary>
		/// Gets or sets the version path.
		/// </summary>
		/// <value>The version path.</value>
		public string VersionPath {
			get {
				return this._versionPath;
			}
			set {
				_versionPath = value;
				this.ApiBaseUrl = _apiSite + _versionPath;
			}
		}

		/// <summary>
		/// Gets or sets the token path.
		/// </summary>
		/// <value>The token path.</value>
		public string TokenPath {
			get {
				return this._tokenPath;
			}
			set {
				_tokenPath = value;
				this.ApiTokenUrl = _apiSite + _tokenPath;
			}
		}
	}
}

