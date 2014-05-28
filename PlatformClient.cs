using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;


namespace pokitdokcsharp
{
	public class PlatformClient: OauthApplicationClient
	{
		public const string POKITDOK_PLATFORM_API_SITE = "https://platform.pokitdok.com";
		public const string POKITDOK_PLATFORM_API_VERSION_PATH = "/api/v3";
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


		public PlatformClient(
			string clientId, 
			string clientSecret, 
			int requestTimeout = DEFAULT_TIMEOUT, 
			OauthAccessToken accessToken = null) : base(clientId, clientSecret, requestTimeout, accessToken)
		{
			this.ApiBaseUrl = _apiSite + _versionPath;
			this.ApiTokenUrl = _apiSite + _tokenPath;

			this.UserAgent = string.Format("csharp-pokitdok/{0}", typeof(PlatformClient).Assembly.GetName().Version);
		}

		public ResponseData activities(string activityId)
		{
			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES + activityId));
		}

		public ResponseData activities(Dictionary<string, string> parameters)
		{
			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES, parameters));
		}

		public ResponseData cashPrices()
		{
			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PRICE_CASH));
		}

		public ResponseData claims(Dictionary<string, object> postData)
		{
			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS, postData));
		}

		public ResponseData claimsStatus(Dictionary<string, object> postData)
		{
			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS_STATUS, postData));
		}

		public ResponseData eligibility(Dictionary<string, object> postData)
		{
			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_ELIGIBILITY, postData));
		}

		public ResponseData enrollment(Dictionary<string, object> postData)
		{
			return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT, postData));
		}

		public ResponseData providers(string npi)
		{
			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PROVIDERS + npi));
		}

		public ResponseData providers(Dictionary<string, string> parameters)
		{
			return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PROVIDERS, parameters));
		}

		public ResponseData files(string trading_partner_id, string filename)
		{
			return applyResponse(
				PostRequest(
					POKITDOK_PLATFORM_API_ENDPOINT_FILES, 
					filename, 
					new Dictionary<string, string> {{"trading_partner_id", trading_partner_id}}
			));
		}

		public dynamic usage()
		{
			if (_usage == null) {
				eligibility (new Dictionary<string, object> { });
			}

			return _usage;
		}

		public dynamic Errors {
			get {
				return this._errors;
			}
			set {
				_errors = value;
			}
		}

		public dynamic Data {
			get {
				return this._data;
			}
			set {
				_data = value;
			}
		}

		public ResponseData applyResponse(ResponseData response)
		{
			dynamic responseObject = JsonConvert.DeserializeObject(response.body);
			_usage = responseObject.meta;
			_data = responseObject.data;
			_errors = _data.GetType().GetProperty("errors");

			return response;
		}

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

		public string VersionPath {
			get {
				return this._versionPath;
			}
			set {
				_versionPath = value;
				this.ApiBaseUrl = _apiSite + _versionPath;
			}
		}

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

