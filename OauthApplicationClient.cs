// Copyright (C) 2014, All Rights Reserved, PokitDok, Inc.
// http://www.pokitdok.com
//
// Please see the LICENSE.txt file for more information.
// All other rights reserved.
//
//	Oauth 2.0 Client implementing Client Credentials Grant Authorization
//		http://tools.ietf.org/html/rfc6749#section-4.4

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
using System.Security.Permissions;

namespace pokitdokcsharp
{
    /// <summary>
    /// Token refresh callback, invoked when the AccessToken is set. Use the callback to save the access token details,
    /// 	save refresh token for re-use.
    /// </summary>
    public delegate void TokenRefreshDelegate(OauthAccessToken accessToken);

    /// <summary>
    /// PokitDok exception.
    /// </summary>
    [Serializable]
    public class PokitDokException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="pokitdokcsharp.PokitDokException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public PokitDokException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="pokitdokcsharp.PokitDokException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public PokitDokException(string message) : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="pokitdokcsharp.PokitDokException"/> class.
        /// </summary>
        public PokitDokException()
        {
        }


        /// <summary>
        /// Used for deserialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PokitDokException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// Store http response headers, body and status code
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// Gets or sets the header values as a Dictionary.
        /// </summary>
        /// <value>The header.</value>
        public Dictionary<string, string> header { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string body { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status.
        /// </summary>
        /// <value>The status.</value>
        [Obsolete("Use status_code instead")]
        public int status { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status.
        /// </summary>
        /// <value>The status.</value>
        public int status_code { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="pokitdokcsharp.ResponseData"/> class.
        /// </summary>
        public ResponseData()
        {
            init();
        }

        /// <summary>
        /// Init this instance.
        /// </summary>
        public void init()
        {
            header = new Dictionary<string, string>();
            body = "";
            status = 0;
        }
    }

    /// <summary>
    /// Oauth access token
    ///		http://tools.ietf.org/html/rfc6749#section-4.4.3
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class OauthAccessToken
    {
        /// <summary>
        /// Gets or sets the access_token.
        /// </summary>
        /// <value>The access_token.</value>
        [System.Runtime.Serialization.DataMember]
        public string access_token { get; set; }

        /// <summary>
        /// Gets or sets the refresh_token.
        /// </summary>
        /// <value>The refresh_token.</value>
        [System.Runtime.Serialization.DataMember]
        public string refresh_token { get; set; }

        /// <summary>
        /// Gets or sets the token_type.
        /// </summary>
        /// <value>The token_type.</value>
        [System.Runtime.Serialization.DataMember]
        public string token_type { get; set; }

        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        /// <value>The expires.</value>
        [System.Runtime.Serialization.DataMember]
        public Int32 expires { get; set; }

        /// <summary>
        /// Gets or sets the expires_in.
        /// </summary>
        /// <value>The expires_in.</value>
        [System.Runtime.Serialization.DataMember]
        public Int32 expires_in { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        [System.Runtime.Serialization.DataMember]
        public string error { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="pokitdokcsharp.OauthAccessToken"/> class.
        /// </summary>
        public OauthAccessToken()
        {
            init();
        }

        /// <summary>
        /// Init this instance.
        /// </summary>
        public void init()
        {
            access_token = "";
            refresh_token = "";
            token_type = "Bearer";
            expires = 0;
            expires_in = 3600;
            error = null;
        }
    }

    /// <summary>
    /// Oauth 2.0 Client implementing Client Credentials Grant Authorization
    ///		http://tools.ietf.org/html/rfc6749#section-4.4
    /// </summary>
    public class OauthApplicationClient : IDisposable
    {
        /// <summary>
        /// The default http request timeout.
        /// </summary>
        public const int DEFAULT_TIMEOUT = 90000; // milliseconds

        /// <summary>
        /// The Oauth refresh token interval.
        /// </summary>
        public const int REFRESH_TOKEN_DURATION = 55; // minutes

        private string _apiBaseUrl;
        private string _apiTokenUrl;

        private OauthAccessToken _accessToken = new OauthAccessToken();
        private System.Object _accessTokenLock = new System.Object();
        public Timer _accessTokenRenewer;
        private string _userAgent;

        private int _requestTimeout;

        private string _clientId;
        private string _clientSecret;
        private string _authCode;

        private string[] _scope;
        private Uri _redirectUrl;
        TokenRefreshDelegate _tokenRefresh = null;

        private ResponseData _responseData = new ResponseData();


        /// <summary>
        /// Dispose of authentication resources
        /// </summary>
        ~OauthApplicationClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }

        private bool disposed = false;
        /// <summary>
        /// Protect against scenarios where the object has already been disposed of, 
        /// but the GC has not processed the object. Avoids disposing the object twice.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed) { 
                if (disposing) { 
                    DeAuthenticate();
                    _accessTokenRenewer?.Dispose();
                    disposed = true; 
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="pokitdokcsharp.OauthApplicationClient"/> class.
        /// </summary>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientSecret">Client secret.</param>
        /// <param name="requestTimeout">Request timeout.</param>
        /// <param name="accessToken">Access token.</param>
        /// <param name="redirectUrl">The Platform App redirect url.</param>
        /// <param name="tokenRefresh">Invoked when access token is refreshed.</param>
        /// <param name="scope">The requested scopes</param>
        /// <param name="authCode">The authorization code recieved by the scope grant of the Platform App</param>
        public OauthApplicationClient(
            string clientId,
            string clientSecret,
            int requestTimeout = DEFAULT_TIMEOUT,
            OauthAccessToken accessToken = null,
            Uri redirectUrl = null,
            TokenRefreshDelegate tokenRefresh = null,
            string[] scope = null,
            string authCode = null)
        {
            this._clientId = clientId;
            this._clientSecret = clientSecret;
            this._requestTimeout = requestTimeout;

            if (accessToken != null)
            {
                this.AccessToken = accessToken;
            }

            this._redirectUrl = redirectUrl;
            this._tokenRefresh = tokenRefresh;
            this._scope = scope;
            this._authCode = authCode;

            // Force use of TLS 1.2
            ServicePointManager.SecurityProtocol = (SecurityProtocolType) 3072;
        }

        /// <summary>
        /// Get an access token and setup a timer to refresh the token
        /// </summary>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when authentication fails or
        /// when some unknown system error occurs.</exception>
        public OauthAccessToken FetchAccessToken()
        {
            return Authenticate();
        }

        /// <summary>
        /// Get an access token and setup a timer to refresh the token
        /// </summary>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when authentication fails or
        /// when some unknown system error occurs.</exception>
        public OauthAccessToken Authenticate()
        {
            if (_accessToken.refresh_token.Length > 0)
            {
                return AuthenticateRefreshToken();
            }
            else if (_authCode == null)
            {
                return AuthenticateClientCredentials();
            }
            else
            {
                return AuthenticateAuthorizationCode();
            }
        }

        private OauthAccessToken AuthenticateRequest(WebRequest webRequest, byte[] request_bytes)
        {
            try
            {
                using (Stream postStream = webRequest.GetRequestStream())
                {
                    postStream.Write(request_bytes, 0, request_bytes.Length);
                    postStream.Close();
                }
                using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse)
                {
                    if (webResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new PokitDokException(
                            string.Format("HTTP {0}: {1}", webResponse.StatusCode, webResponse.StatusDescription)
                        );
                    }
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OauthAccessToken));
                    this.AccessToken = (OauthAccessToken) serializer.ReadObject(webResponse.GetResponseStream());
                    if (this.AccessToken.expires == 0)
                    {
                        this.AccessToken.expires =
                            (Int32) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds +
                            this.AccessToken.expires_in;
                    }

                    if (this._tokenRefresh != null)
                    {
                        _tokenRefresh(this.AccessToken);
                    }
                }

                _accessTokenRenewer = new Timer(
                    new TimerCallback(OnTokenExpiredCallback),
                    this,
                    TimeSpan.FromMinutes(REFRESH_TOKEN_DURATION),
                    TimeSpan.FromMilliseconds(-1)
                );
            }
            catch (Exception ex)
            {
                throw new PokitDokException("Authentication Error: " + ex.Message, ex);
            }

            return this.AccessToken;
        }

        private OauthAccessToken AuthenticateClientCredentials()
        {
            try
            {
                HttpWebRequest webRequest = WebRequest.Create(this.ApiTokenUrl) as HttpWebRequest;
                webRequest.Timeout = _requestTimeout;
                webRequest.Headers["Authorization"] =
                    "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret));
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.UserAgent = this._userAgent;
                byte[] request_bytes = Encoding.UTF8.GetBytes("grant_type=client_credentials" +
                                                              "&client_id=" + _clientId + "&client_secret=" +
                                                              _clientSecret);
                webRequest.ContentLength = request_bytes.Length;

                AuthenticateRequest(webRequest, request_bytes);
            }
            catch (Exception ex)
            {
                throw new PokitDokException("Authentication Error: " + ex.Message, ex);
            }

            return this.AccessToken;
        }

        private OauthAccessToken AuthenticateAuthorizationCode()
        {
            try
            {
                HttpWebRequest webRequest = WebRequest.Create(this.ApiTokenUrl) as HttpWebRequest;
                webRequest.Timeout = _requestTimeout;
                webRequest.Headers["Authorization"] = "Basic " +
                                                      Convert.ToBase64String(
                                                          Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret));
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.UserAgent = this._userAgent;

                byte[] request_bytes =
                    Encoding.UTF8.GetBytes("grant_type=authorization_code" +
                                           "&client_id=" + _clientId + "&client_secret=" + _clientSecret +
                                           "&redirect_uri=" + _redirectUrl +
                                           "&code=" + _authCode + "&scope=" + string.Join(" ", _scope));

                webRequest.ContentLength = request_bytes.Length;

                AuthenticateRequest(webRequest, request_bytes);
            }
            catch (Exception ex)
            {
                throw new PokitDokException("Authentication Error: " + ex.Message, ex);
            }

            return this.AccessToken;
        }

        private OauthAccessToken AuthenticateRefreshToken()
        {
            try
            {
                HttpWebRequest webRequest = WebRequest.Create(this.ApiTokenUrl) as HttpWebRequest;
                webRequest.Timeout = _requestTimeout;
                webRequest.Headers["Authorization"] =
                    "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret));
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.UserAgent = this._userAgent;
                byte[] request_bytes = Encoding.UTF8.GetBytes("grant_type=refresh_token" +
                                                              "&client_id=" + _clientId + "&client_secret=" +
                                                              _clientSecret + "&refresh_token=" +
                                                              _accessToken.refresh_token + "&scope=" +
                                                              string.Join(" ", _scope));
                webRequest.ContentLength = request_bytes.Length;

                AuthenticateRequest(webRequest, request_bytes);
            }
            catch (Exception ex)
            {
                throw new PokitDokException("Authentication Error: " + ex.Message, ex);
            }

            return this.AccessToken;
        }

        /// <summary>
        /// Calls an endpoint on the PokitDok server to remove client access tokens.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PokitDokException"></exception>
        public OauthAccessToken DeAuthenticate()
        {
            // Access token is obtained during the first request. PD server will return 406 if no token is sent over
            if (AccessToken.access_token == "")
            {
                return null;
            }
            try
            {
                var request = new Dictionary<string, object>()
                {
                    {"token", AccessToken.access_token}
                };

                var request_json_data = JsonConvert.SerializeObject(request);

                HttpWebRequest webRequest = CreateRequest(request, ApiLogoutUrl, "POST");

                byte[] request_bytes = Encoding.UTF8.GetBytes(request_json_data);

                using (Stream postStream = webRequest.GetRequestStream())
                {
                    postStream.Write(request_bytes, 0, request_bytes.Length);
                    postStream.Close();
                }
                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    ProcessResponse(response);
                }
            }
            catch (WebException ex)
            {
                // If 404, proceed without exception. This allows client update to proceed server updates.
                if (!ex.Message.Contains("404"))
                {
                    throw new PokitDokException("Error while deauthenticating client:  " + ex.Message, ex);
                }
            }
            return null;
        }

        /// <summary>
        /// Raises the token expired callback event.
        /// </summary>
        /// <param name="stateInfo">State info.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs</exception>
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

        /// <summary>
        /// Check if the access token is expired
        /// </summary>
        /// <returns><c>true</c>, if access token expired was ised, <c>false</c> otherwise.</returns>
        private bool isAccessTokenExpired()
        {
            if (!(this.AccessToken is OauthAccessToken))
            {
                return true;
            }

            return (Int32) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds >
                   this.AccessToken.expires - _requestTimeout / 1000;
        }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public OauthAccessToken AccessToken
        {
            get { return this._accessToken; }
            set
            {
                lock (_accessTokenLock)
                {
                    _accessToken = value;
                }
            }
        }

        /// <summary>
        /// Forms the HttpWebRequest
        /// </summary>
        /// <returns>The post stream.</returns>
        /// <param name="data">data to be converted to JSON. Need to know the length</param>
        /// <param name="url">URL</param>
        /// <param name="method">post, put, delete, get</param>
        /// <param name="content_type">should always be application/json (default)</param>
        public HttpWebRequest CreateRequest(Dictionary<string, object> data, string url, string method,
            string content_type = "application/json")
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Timeout = _requestTimeout;
            webRequest.Headers["Authorization"] = "Bearer " + this.AccessToken.access_token;
            webRequest.Method = method;
            webRequest.UserAgent = this._userAgent;

            // For GET, don't specify content type
            if (content_type != null)
            {
                webRequest.ContentType = content_type;
            }


            if (data != null)
            {
                string request_json_data = JsonConvert.SerializeObject(data);
                byte[] request_bytes = Encoding.UTF8.GetBytes(request_json_data);
                webRequest.ContentLength = request_bytes.Length;
            }

            return webRequest;
        }

        /// <summary>
        /// Perform a GET request given the http request path and a dictionary of query parameters.
        /// </summary>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.</returns>
        /// <param name="requestPath">Request path.</param>
        /// <param name="parameters">Dictionary of query parameters.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        public ResponseData GetRequest(string requestPath, Dictionary<string, string> parameters = null)
        {
            if (isAccessTokenExpired())
            {
                Authenticate();
            }

            string request_uri = _apiBaseUrl + requestPath;

            // Append all parameters to URL
            try
            {
                if (parameters != null)
                {
                    request_uri += "?";
                    bool first = true;
                    foreach (KeyValuePair<string, string> query_param in parameters)
                    {
                        if (!first)
                        {
                            request_uri += "&";
                        }
                        first = false;
                        request_uri += query_param.Key + "=" + HttpUtility.UrlEncode(query_param.Value);
                    }
                }

                HttpWebRequest webRequest = CreateRequest(null, request_uri, "GET", null);

                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    ProcessResponse(response);
                }
            }
            catch (WebException wex)
            {
                ProcessResponse((HttpWebResponse) wex.Response);
            }
            catch (Exception ex)
            {
                throw new PokitDokException(string.Format("GetRequest({0}) Error: {1}", request_uri, ex.Message), ex);
            }

            return _responseData;
        }

        /// <summary>
        /// Perform a POST request given the http request path and a dictionary representing the JSON post body.
        /// </summary>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.</returns>
        /// <param name="requestPath">Request path.</param>
        /// <param name="postData">Post data: dictionary representing JSON data</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        public ResponseData PostRequest(string requestPath, Dictionary<string, object> postData)
        {
            if (isAccessTokenExpired())
            {
                Authenticate();
            }

            string request_uri = _apiBaseUrl + requestPath;

            try
            {
                string request_json_data = JsonConvert.SerializeObject(postData);


                HttpWebRequest webRequest = CreateRequest(postData, request_uri, "POST");

                byte[] request_bytes = Encoding.UTF8.GetBytes(request_json_data);

                using (Stream postStream = webRequest.GetRequestStream())
                {
                    postStream.Write(request_bytes, 0, request_bytes.Length);
                    postStream.Close();
                }
                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    ProcessResponse(response);
                }
            }
            catch (WebException wex)
            {
                ProcessResponse((HttpWebResponse) wex.Response);
            }
            catch (Exception ex)
            {
                throw new PokitDokException(string.Format("PostRequest({0}) Error: {1}", request_uri, ex.Message), ex);
            }

            return _responseData;
        }

        /// <summary>
        /// Perform a POST request given the http request path and a file to post with optional form field parameters.
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="requestPath">Request path.</param>
        /// <param name="postFilePath">File system path of file data to be posted.</param>
        /// <param name="postFileContentDispositionName">Post file content disposition name.</param>
        /// <param name="postFileContentType">Post file content type.</param>
        /// <param name="parameters">Dictionary of form data parameters.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        public ResponseData PostRequest(string requestPath, string postFilePath, string postFileContentDispositionName,
            string postFileContentType, Dictionary<string, string> parameters = null)
        {
            if (isAccessTokenExpired())
            {
                Authenticate();
            }

            string request_uri = _apiBaseUrl + requestPath;

            try
            {
                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
                HttpWebRequest webRequest = CreateRequest(null, request_uri, "POST",
                    "multipart/form-data; boundary=" + boundary);

                NameValueCollection formData = new NameValueCollection();
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, string> query_param in parameters)
                    {
                        formData[query_param.Key] = query_param.Value;
                    }
                }

                using (Stream postDataStream = GetPostStream(
                    postFilePath,
                    postFileContentType,
                    postFileContentDispositionName,
                    formData,
                    boundary))
                {
                    webRequest.ContentLength = postDataStream.Length;
                    using (Stream reqStream = webRequest.GetRequestStream())
                    {
                        postDataStream.Position = 0;
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = postDataStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            reqStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    ProcessResponse(response);
                }
            }
            catch (WebException wex)
            {
                ProcessResponse((HttpWebResponse) wex.Response);
            }
            catch (Exception ex)
            {
                throw new PokitDokException(string.Format("PostRequest({0}) Error: {1}", request_uri, ex.Message), ex);
            }

            return _responseData;
        }

        /// <summary>
        /// Builds a Stream of multipart form data from file and form fields.
        /// </summary>
        /// <returns>The post stream.</returns>
        /// <param name="filePath">File path.</param>
        /// <param name="fileContentType">File content type.</param>
        /// <param name="fileContentDispositionName">File content disposition name.</param>
        /// <param name="formData">Form data.</param>
        /// <param name="boundary">Boundary.</param>
        private static Stream GetPostStream(
            string filePath,
            string fileContentType,
            string fileContentDispositionName,
            NameValueCollection formData,
            string boundary)
        {
            Stream postDataStream = new System.IO.MemoryStream();

            //add form data
            string formDataHeaderTemplate =
                Environment.NewLine +
                "--" + boundary + Environment.NewLine +
                "Content-Disposition: form-data; name=\"{0}\";" + Environment.NewLine +
                Environment.NewLine +
                "{1}";
            foreach (string key in formData.Keys)
            {
                byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(
                    string.Format(formDataHeaderTemplate, key, formData[key])
                );
                postDataStream.Write(formItemBytes, 0, formItemBytes.Length);
            }

            //add file data
            FileInfo fileInfo = new FileInfo(filePath);
            string fileHeaderTemplate =
                Environment.NewLine +
                "--" + boundary + Environment.NewLine +
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" + Environment.NewLine +
                "Content-Type: {2}" + Environment.NewLine +
                Environment.NewLine;
            byte[] fileHeaderBytes = System.Text.Encoding.UTF8.GetBytes(
                string.Format(fileHeaderTemplate, fileContentDispositionName, fileInfo.FullName, fileContentType)
            );
            postDataStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

            using (FileStream fileStream = fileInfo.OpenRead())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postDataStream.Write(buffer, 0, bytesRead);
                }
            }

            byte[] endBoundaryBytes = System.Text.Encoding.UTF8.GetBytes("--" + boundary + "--");
            postDataStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);

            return postDataStream;
        }

        /// <summary>
        /// Perform a PUT request given the uri and put data form fields
        /// </summary>
        /// <param name="requestPath">Request path.</param>
        /// <param name="putData">Put data: dictionary representing JSON data.</param>
        /// <returns></returns>
        public ResponseData PutRequest(string requestPath, Dictionary<string, object> putData)
        {
            if (isAccessTokenExpired())
            {
                Authenticate();
            }

            string request_uri = _apiBaseUrl + requestPath;

            try
            {
                string request_json_data = JsonConvert.SerializeObject(putData);
                HttpWebRequest webRequest = CreateRequest(putData, request_uri, "PUT");

                byte[] request_bytes = Encoding.UTF8.GetBytes(request_json_data);
                using (Stream postStream = webRequest.GetRequestStream())
                {
                    postStream.Write(request_bytes, 0, request_bytes.Length);
                    postStream.Close();
                }
                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    ProcessResponse(response);
                }
            }
            catch (WebException wex)
            {
                ProcessResponse((HttpWebResponse) wex.Response);
            }
            catch (Exception ex)
            {
                throw new PokitDokException(string.Format("PutRequest({0}) Error: {1}", request_uri, ex.Message), ex);
            }

            return _responseData;
        }

        /// <summary>
        /// Perform a DELETE request for the given resource
        /// </summary>
        /// <param name="requestPath">Request path/resource</param>
        /// <returns></returns>
        public ResponseData DeleteRequest(string requestPath)
        {
            if (isAccessTokenExpired())
            {
                Authenticate();
            }

            string request_uri = _apiBaseUrl + requestPath;

            try
            {
                HttpWebRequest webRequest = CreateRequest(null, request_uri, "DELETE", null);

                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    ProcessResponse(response);
                }
            }
            catch (WebException wex)
            {
                ProcessResponse((HttpWebResponse) wex.Response);
            }
            catch (Exception ex)
            {
                throw new PokitDokException(string.Format("DeleteRequest({0}) Error: {1}", request_uri, ex.Message), ex);
            }

            return _responseData;
        }

        /// <summary>
        /// Processes the http response into a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// </summary>
        /// <returns>The response as a <see cref="pokitdokcsharp.ResponseData"/> object.</returns>
        /// <param name="response">Response.</param>
        private ResponseData ProcessResponse(HttpWebResponse response)
        {
            if (!(_responseData is ResponseData))
            {
                _responseData = new ResponseData();
            }
            else
            {
                _responseData.init();
            }

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                _responseData.body = (string) sr.ReadToEnd().Trim();
            }
            foreach (string key in response.Headers.AllKeys)
            {
                _responseData.header.Add(key, response.Headers[key]);
            }
            _responseData.status = (int) response.StatusCode;
            _responseData.status_code = (int) response.StatusCode;

            return _responseData;
        }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public string ClientId
        {
            get { return this._clientId; }
            set { _clientId = value; }
        }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>The client secret.</value>
        public string ClientSecret
        {
            get { return this._clientSecret; }
            set { _clientSecret = value; }
        }

        /// <summary>
        /// Gets or sets the request timeout.
        /// </summary>
        /// <value>The request timeout.</value>
        public int RequestTimeout
        {
            get { return this._requestTimeout; }
            set { _requestTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the API token URL.
        /// </summary>
        /// <value>The API token URL.</value>
        public string ApiTokenUrl
        {
            get { return this._apiTokenUrl; }
            set { _apiTokenUrl = value; }
        }

        public string ApiLogoutUrl { get; set; }

        /// <summary>
        /// Gets or sets the API base URL.
        /// </summary>
        /// <value>The API base URL.</value>
        public string ApiBaseUrl
        {
            get { return this._apiBaseUrl; }
            set { _apiBaseUrl = value; }
        }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent
        {
            get { return this._userAgent; }
            set { _userAgent = value; }
        }
    }
}

