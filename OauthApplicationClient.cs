using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using Newtonsoft.Json;

namespace pokitdokcsharp
{
	public class PokitDokException : Exception 
	{
		public PokitDokException(string message, Exception innerException) : 
			base(message, innerException) {
		}
		public PokitDokException(string message) : base(message) {
		}
	}

	public class ResponseData
	{
		public Dictionary<string, string> header { get; set; }
		public string body { get; set; }
		public int status { get; set; }

		public ResponseData()
		{
			init();
		}

		public void init()
		{
			header = new Dictionary<string, string>();
			body = "";
			status = 0;
		}
	}

	[System.Runtime.Serialization.DataContract]
	public class OauthAccessToken
	{
		[System.Runtime.Serialization.DataMember]
		public string access_token { get; set; }
		[System.Runtime.Serialization.DataMember]
		public string token_type { get; set; }
		[System.Runtime.Serialization.DataMember]
		public string expires { get; set; }
		[System.Runtime.Serialization.DataMember]
		public string expires_in { get; set; }
		[System.Runtime.Serialization.DataMember]
		public string error { get; set; }

		public OauthAccessToken()
		{
			init ();
		}

		public void init()
		{
			access_token = "RftNvQ4DmMewkbvSiq2niZxiobEtPgEKXfzqWCLF";
			token_type = "bearer";
			expires = "1400763854";
			expires_in = "3600";
			error = null;
		}
	}

	public class OauthApplicationClient
	{
		public const int DEFAULT_TIMEOUT = 90000; // milliseconds
		public const int REFRESH_TOKEN_DURATION = 55; // minutes

		private string _apiBaseUrl;
		private string _apiTokenUrl;
		private OauthAccessToken _accessToken = new OauthAccessToken();
		private System.Object _accessTokenLock = new System.Object();
		private Timer _accessTokenRenewer;
		private string _userAgent;

		private int _requestTimeout;

		private string _clientId;
		private string _clientSecret;

		private ResponseData _responseData = new ResponseData();

		public OauthApplicationClient(
			string clientId, 
			string clientSecret, 
			int requestTimeout = DEFAULT_TIMEOUT, 
			OauthAccessToken accessToken = null)
		{
			this._clientId = clientId;
			this._clientSecret = clientSecret;
			this._requestTimeout = requestTimeout;

			if (accessToken != null) {
				this.AccessToken = accessToken;
			}
		}

		public OauthAccessToken Authenticate()
		{
			try {

				HttpWebRequest webRequest = WebRequest.Create(this.ApiTokenUrl) as HttpWebRequest;
				webRequest.Timeout = _requestTimeout;
				webRequest.Headers["Authorization"] = 
					"Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret));
				webRequest.Method = "POST";
				webRequest.ContentType = "application/x-www-form-urlencoded";
				webRequest.UserAgent = this._userAgent;
				byte[] request_bytes = Encoding.UTF8.GetBytes("grant_type=client_credentials");
				webRequest.ContentLength = request_bytes.Length;

				using (Stream postStream = webRequest.GetRequestStream()) {
					postStream.Write(request_bytes, 0, request_bytes.Length);
					postStream.Close();
				}
				using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse)
				{
					if (webResponse.StatusCode != HttpStatusCode.OK) {
						throw new Exception(
							string.Format("HTTP {0}: {1}", webResponse.StatusCode, webResponse.StatusDescription)
						);
					}
					DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OauthAccessToken));
					this.AccessToken = (OauthAccessToken) serializer.ReadObject(webResponse.GetResponseStream());
				}
				
				_accessTokenRenewer = new Timer(
					new TimerCallback(OnTokenExpiredCallback), 
					this, 
					TimeSpan.FromMinutes(REFRESH_TOKEN_DURATION), 
					TimeSpan.FromMilliseconds(-1)
				);

			} catch (Exception ex) {
				throw new PokitDokException("Authentication Error: " + ex.Message, ex);
			}

			return this.AccessToken;
		}

		private void OnTokenExpiredCallback(object stateInfo)
		{
			try
			{
				Authenticate();
			}
			catch (Exception ex)
			{
				throw new PokitDokException(string.Format("Failed renewing access token: {0}", ex.Message), ex);
			}
			finally
			{
				try
				{
					_accessTokenRenewer.Change(
						TimeSpan.FromMinutes(REFRESH_TOKEN_DURATION), 
						TimeSpan.FromMilliseconds(-1)
					);
				}
				catch (Exception ex)
				{
					throw new PokitDokException(
						string.Format(
							"Failed to reschedule the timer to renew access token: {0}", 
							ex.Message), 
						ex
					);
				}
			}
		}

		private bool isAccessTokenExpired()
		{
			if (!(this.AccessToken is OauthAccessToken)) {
				return true;
			}

			return (
				((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds) > 
				(Convert.ToInt32(this.AccessToken.expires) - (_requestTimeout/1000))
			);
		}

		public OauthAccessToken AccessToken {
			get {
				return this._accessToken;
			}
			set {
				lock (_accessTokenLock) {
					_accessToken = value;
				}
			}
		}

		public ResponseData GetRequest(string requestPath, Dictionary<string,string> parameters = null)
		{
			if (isAccessTokenExpired()) {
				Authenticate();
			}

			string request_uri = _apiBaseUrl + requestPath;

			try {

				if (parameters != null) {
					request_uri += "?";
					bool first = true;
					foreach (KeyValuePair<string, string> query_param in parameters) {
						if (!first) {
							request_uri += "&";
						}
						first = false;
						request_uri += query_param.Key + "=" + HttpUtility.UrlEncode(query_param.Value);
					}
				}

				HttpWebRequest webRequest = WebRequest.Create(request_uri) as HttpWebRequest;
				webRequest.Timeout = _requestTimeout;
				webRequest.Headers["Authorization"] = "Bearer " + this.AccessToken.access_token;
				webRequest.Method = "GET";
				//webRequest.ContentType = "application/json";
				webRequest.UserAgent = this._userAgent;

				using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse) {
					ProcessResponse(response);
				}
			}
			catch (WebException wex) {
				ProcessResponse((HttpWebResponse)wex.Response);
			}
			catch (Exception ex) 
			{
				throw new PokitDokException(string.Format("GetRequest({0}) Error: {1}", request_uri, ex.Message), ex);
			}

			return _responseData;
		}

		public ResponseData PostRequest(string requestPath, string postFilePath, Dictionary<string,string> parameters = null)
		{
			if (isAccessTokenExpired()) {
				Authenticate();
			}

			string request_uri = _apiBaseUrl + requestPath;

			try {
						
				HttpWebRequest webRequest = WebRequest.Create(request_uri) as HttpWebRequest;

				webRequest.Timeout = _requestTimeout;
				webRequest.Headers["Authorization"] = "Bearer " + this.AccessToken.access_token;
				webRequest.Method = "POST";
				string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
				webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
				webRequest.UserAgent = this._userAgent;

				NameValueCollection formData = new NameValueCollection();
				if (parameters != null) {
					foreach (KeyValuePair<string, string> query_param in parameters) {
						formData[query_param.Key] = query_param.Value;
					}
				}

				using (Stream postDataStream = GetPostStream(postFilePath, formData, boundary)) {
					webRequest.ContentLength = postDataStream.Length;
					using (Stream reqStream = webRequest.GetRequestStream()) {
						postDataStream.Position = 0;
						byte[] buffer = new byte[1024];
						int bytesRead = 0;
						while ((bytesRead = postDataStream.Read(buffer, 0, buffer.Length)) != 0)
						{
							reqStream.Write(buffer, 0, bytesRead);
						}
						reqStream.Close();
					}
					postDataStream.Close();
				}

				using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse) {
					ProcessResponse(response);
				}
			} 
			catch (WebException wex) {
				ProcessResponse((HttpWebResponse)wex.Response);
			}
			catch (Exception ex) {
				throw new PokitDokException(string.Format("PostRequest({0}) Error: {1}", request_uri, ex.Message));
			}

			return _responseData;
		}

		public ResponseData PostRequest(string requestPath, Dictionary<string, object> postData)
		{
			if (isAccessTokenExpired()) {
				Authenticate();
			}

			string request_uri = _apiBaseUrl + requestPath;

			try {

				string request_json_data = JsonConvert.SerializeObject(postData);

				HttpWebRequest webRequest = WebRequest.Create(request_uri) as HttpWebRequest;
				webRequest.Timeout = _requestTimeout;
				webRequest.Headers["Authorization"] = "Bearer " + this.AccessToken.access_token;
				webRequest.Method = "POST";
				webRequest.ContentType = "application/json";
				webRequest.UserAgent = this._userAgent;
				byte[] request_bytes = Encoding.UTF8.GetBytes(request_json_data);
				webRequest.ContentLength = request_bytes.Length;

				using (Stream postStream = webRequest.GetRequestStream()) {
					postStream.Write(request_bytes, 0, request_bytes.Length);
					postStream.Close();
				}
				using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse) {
					ProcessResponse(response);
				}
			}
			catch (WebException wex) {
				ProcessResponse((HttpWebResponse)wex.Response);
			}
			catch (Exception ex)
			{
				throw new PokitDokException(string.Format("PostRequest({0}) Error: {1}", request_uri, ex.Message));
			}

			return _responseData;
		}

		private static Stream GetPostStream(string filePath, NameValueCollection formData, string boundary)
		{
			Stream postDataStream = new System.IO.MemoryStream();

			//adding form data
			string formDataHeaderTemplate = Environment.NewLine + "--" + boundary + Environment.NewLine +
				"Content-Disposition: form-data; name=\"{0}\";" + Environment.NewLine + Environment .NewLine + "{1}";

			foreach (string key in formData.Keys)
			{
				byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format(formDataHeaderTemplate,
					key, formData[key]));
				postDataStream.Write(formItemBytes, 0, formItemBytes.Length);
			}

			//adding file data
			FileInfo fileInfo = new FileInfo(filePath);

			string fileHeaderTemplate = Environment.NewLine + "--" + boundary + Environment.NewLine +
				"Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
				Environment.NewLine + "Content-Type: application/EDI-X12" + Environment.NewLine + Environment.NewLine;

			byte[] fileHeaderBytes = System.Text.Encoding.UTF8.GetBytes(string.Format(fileHeaderTemplate,
				"file", fileInfo.FullName));

			postDataStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

			FileStream fileStream = fileInfo.OpenRead();

			byte[] buffer = new byte[1024];

			int bytesRead = 0;

			while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
			{
				postDataStream.Write(buffer, 0, bytesRead);
			}

			fileStream.Close();

			byte[] endBoundaryBytes = System.Text.Encoding.UTF8.GetBytes("--" + boundary + "--");
			postDataStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);

			return postDataStream;
		}

		private ResponseData ProcessResponse(HttpWebResponse response)
		{
			if (!(_responseData is ResponseData)) {
				_responseData = new ResponseData();
			} else {
				_responseData.init();
			}

			using (StreamReader sr = new StreamReader(response.GetResponseStream())) {
				_responseData.body = (string) sr.ReadToEnd().Trim();
			}
			foreach (string key in response.Headers.AllKeys) {
				_responseData.header.Add(key, response.Headers[key]);
			}
			_responseData.status = (int) response.StatusCode;

			return _responseData;
		}

		public string ClientId {
			get {
				return this._clientId;
			}
			set {
				_clientId = value;
			}
		}

		public string ClientSecret {
			get {
				return this._clientSecret;
			}
			set {
				_clientSecret = value;
			}
		}

		public int RequestTimeout {
			get {
				return this._requestTimeout;
			}
			set {
				_requestTimeout = value;
			}
		}

		public string ApiTokenUrl {
			get {
				return this._apiTokenUrl;
			}
			set {
				_apiTokenUrl = value;
			}
		}

		public string ApiBaseUrl {
			get {
				return this._apiBaseUrl;
			}
			set {
				_apiBaseUrl = value;
			}
		}

		public string UserAgent {
			get {
				return this._userAgent;
			}
			set {
				_userAgent = value;
			}
		}
	}
}

