// Copyright (C) 2014, All Rights Reserved, PokitDok, Inc.
// http://www.pokitdok.com
//
// Please see the LICENSE.txt file for more information.
// All other rights reserved.
//
//	PokitDok Platform Client for C#
//		Consume the REST based PokitDok platform API
//		https://platform.pokitdok.com/documentation/v4#/#overview

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace pokitdokcsharp
{
    /// <summary>
    ///	PokitDok Platform Client for C#
    ///		Consumes the REST based PokitDok platform API
    ///		https://platform.pokitdok.com/documentation/v4#/#overview
    /// </summary>
    public class PlatformClient: OauthApplicationClient
    {
        /// <summary>
        /// The default PokitDok API site url.
        /// </summary>
        private const string POKITDOK_PLATFORM_API_SITE = "https://platform.pokitdok.com";

        /// <summary>
        /// The default current PokitDok API version path.
        /// </summary>
        private const string POKITDOK_PLATFORM_API_VERSION_PATH = "/api/v4";
        /// <summary>
        /// The Oauth token path.
        /// </summary>
        private const string POKITDOK_PLATFORM_API_TOKEN_PATH = "/oauth2/token";

        /// <summary>
        /// URL for invalidating a user's session
        /// </summary>
        private const string POKITDOK_PLATFORM_API_TOKEN_LOGOUT = "/oauth2/revoke";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES = "/activities/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_AUTHORIZATIONS = "/authorizations/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_CCD = "/ccd/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS = "/claims/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS_CONVERT = "/claims/convert";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS_STATUS = "/claims/status";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_DEDUCTIBLE = "/deductible/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_ELIGIBILITY = "/eligibility/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT = "/enrollment/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT_SNAPSHOT = "/enrollment/snapshot";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_ICD_CONVERT = "/icd/convert";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY = "/identity/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY_VALIDATE = "/identity/proof/valid/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY_SCORE_QUESTIONS =
            "/identity/proof/questions/score/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY_GENERATE_QUESTIONS =
            "/identity/proof/questions/generate/";



        private const string POKITDOK_PLATFORM_API_ENDPOINT_PRICE_INSURANCE = "/prices/insurance";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_PRICE_CASH = "/prices/cash";

        private const string POKITDOK_PLATFORM_API_OOP_INSURANCE_ESTIMATE = "/oop/insurance-estimate";
        private const string POKITDOK_PLATFORM_API_OOP_INSURANCE_LOAD_PRICES = "/oop/insurance-load-price";


        private const string POKITDOK_PLATFORM_API_ENDPOINT_MPC = "/mpc/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_PAYERS = "/payers/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_PLANS = "/plans/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_PROVIDERS = "/providers/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_REFERRALS = "/referrals/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_SCHEDULERS = "/schedule/schedulers/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENT_TYPES = "/schedule/appointmenttypes/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_SCHEDULE_PATIENT = "/schedule/patient/";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_SLOTS = "/schedule/slots";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENTS = "/schedule/appointments/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_TRADING_PARTNERS = "/tradingpartners/";

        private const string POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_PLANS = "/pharmacy/plans";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_FORMULARY = "/pharmacy/formulary";
        private const string POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_NETWORK = "/pharmacy/network";

        private string _apiSite = POKITDOK_PLATFORM_API_SITE;
        private string _versionPath = POKITDOK_PLATFORM_API_VERSION_PATH;
        private string _tokenPath = POKITDOK_PLATFORM_API_TOKEN_PATH;
        private string _logoutPath = POKITDOK_PLATFORM_API_TOKEN_LOGOUT;

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
        /// <param name="redirectUrl">The application OAuth2 redirect url.</param>
        /// <param name="tokenRefresh">Method to invoke when access token refresh save occurs.</param>
        /// <param name="scope">Array of OAuth2 scopes requested</param>
        /// <param name="authCode">The code received from an authorization code grant flow.</param> 
        public PlatformClient(
                string clientId, 
                string clientSecret, 
                int requestTimeout = DEFAULT_TIMEOUT, 
                OauthAccessToken accessToken = null,
                Uri redirectUrl = null,
                TokenRefreshDelegate tokenRefresh = null,
                string[] scope = null,
                string authCode = null
            ) : base(clientId, clientSecret, requestTimeout, accessToken, redirectUrl, tokenRefresh, scope, authCode)
        {
            init();

            this.ApiBaseUrl = _apiSite + _versionPath;
            this.ApiTokenUrl = _apiSite + _tokenPath;
            this.ApiLogoutUrl = _apiSite + _logoutPath;

            this.UserAgent = ConstructUserAgent();
        }

        private static string ConstructUserAgent()
        {
            var version = typeof(PlatformClient).Assembly.GetName().Version.ToString(); // ex 1.17.38783
            var os = Environment.OSVersion.Platform.ToString(); // ex. Unix
            var osVersion = Environment.OSVersion.Version.ToString(); // ex. 15.6.0.0

            //[pokitdok-[client name]#[client version]#[operating system]#[operating system version]]
            return $"csharp-pokitdok#{version}#{os}#{osVersion}";

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
        /// Call the activities endpoint for a list of all current activities
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#activities
        /// </summary>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData activities()
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ACTIVITIES)); 
        }



        /// <summary>
        /// Call the activities endpoint for a specific activityId.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#activities
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
        /// Call the activities endpoint to get a listing of current activities,
        /// a query string parameter ‘parent_id’ may also be used with this API to get information about 
        /// sub-activities that were initiated from a batch file upload.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#activities
        /// </summary>
        /// <param name="parameters">
        /// Query parameters:
        /// _id, {string} ID of this Activity
        /// name, {string} Activity name
        /// callback_url, {string} URL that will be invoked to notify the client application that this Activity has completed.  
        /// 	We recommend that you always use https for callback URLs used by your application.
        /// history, {list} Historical status of the progress of this Activity
        /// state, {dict} Current state of this Activity
        /// transition_path, {list} The list of state transitions that will be used for this Activity.
        /// remaining_transitions, {list} The list of remaining state transitions that the activity has yet to go through.
        /// parameters, {dict} The parameters that were originally supplied to the activity
        /// units_of_work, {int} The number of ‘units of work’ that the activity is operating on.  
        /// 	This will typically be 1 for real-time requests like /eligibility. If an activity
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
        /// Submit an authorization request.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#authorizations
        /// </summary>
        /// <param name="postData">Dictionary representing JSON post data.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData authorizations(Dictionary<string, object> postData)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_AUTHORIZATIONS, postData));
        }

        /// <summary>
        /// File an EDI 834 benefit enrollment.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#enrollment
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
        /// Return a list of cash prices for a given procedure (by CPT Code) in a given region (by ZIP Code).
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#cashprices
        /// </summary>
        /// <param name="parameters">
        /// Query parameters:
        /// 	cpt_code, {string} The CPT code of the procedure in question.
        ///		zip_code, {string} Postal code in which to search for procedures
        /// </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData cashPrices(Dictionary<string, string> parameters = null)
        {
            return pricesCash(parameters);
        }

        /// <summary>
        /// Return a list of cash prices for a given procedure (by CPT Code) in a given region (by ZIP Code).
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#cashprices
        /// </summary>
        /// <param name="parameters">
        /// Query parameters:
        /// 	cpt_code, {string} The CPT code of the procedure in question.
        ///		zip_code, {string} Postal code in which to search for procedures
        /// </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Obsolete: Please use cashPrices().")]
        public ResponseData pricesCash(Dictionary<string, string> parameters = null)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PRICE_CASH, parameters));
        }

        /// <summary>
        /// Create a new claim, via the filing of an EDI 837 Professional Claims, to the designated Payer.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#claims
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
        /// Convert an X12 837 claims file into a claims request model
        /// See docs here: https://platform.pokitdok.com/documentation/v4/#claims-convert
        /// </summary>
        /// <param name="postData">Dictionary represnting JSON post data</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData claimsConvert(Dictionary<string, object> postData)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_CLAIMS_CONVERT, postData));

        }

        /// <summary>
        /// Ascertain the status of the specified claim, via the filing of an EDI 276 Claims Status.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#claimstatus
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
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#eligibility
        /// </summary>
        /// <param name="postData">Dictionary representing an EDI 270 Request For Eligibility.
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
        /// Submit a X12 834 file as the current snapshot of a group’s benefits enrollment data
        /// </summary>
        /// <param name="tradingPartnerId"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public ResponseData enrollmentSnapshot(string tradingPartnerId, string filepath)
        {
            
            init();

            Dictionary<string, string> postData = 
                new Dictionary<string, string> { { "trading_partner_id", tradingPartnerId } };

            return applyResponse(PostRequest(
                POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT_SNAPSHOT, 
                filepath,
                "file",
                "application/EDI-X12",
                postData
            ));
        }

        /// <summary>
        /// List enrollment snapshots owned by the current application
        /// </summary>
        /// <returns></returns>
        public ResponseData enrollmentSnapshot()
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT_SNAPSHOT));
        }

        /// <summary>
        /// Get information about a specific enrollment snapshot owned by the current application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseData enrollmentSnapshot(string id)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT_SNAPSHOT + '/' + id)); 
        }

        /// <summary>
        /// List enrollment data associated with a specific enrollment snapshot owned by the current application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseData enrollmentSnapshotData(string id)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ENROLLMENT_SNAPSHOT + '/' + id + "/data")); 
        }

        /// <summary>
        /// retrieve ICD-9 to ICD-10 mapping information
        /// </summary>
        /// <param name="icd9_code"></param>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData icdConvert(string icd9_code)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_ICD_CONVERT + '/' + icd9_code)); 
          
        }

        /// <summary>
        /// Creates an identity resource. Returns the created resource with a uuid
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData createIdentity(Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY, parameters)); 
        }
    
        /// <summary>
        /// Updates an identity resource with a given uuid. Returns the updated identity resource.
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="parameters"></param>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData updateIdentity(string uuid, Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PutRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY + uuid, parameters)); 
        }

        /// <summary>
        /// Queries for an identity with a given uuid.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData getIdentity(string uuid)
        {
            return identity(uuid);
        }

        /// <summary>
        /// Queries for an identity with a given uuid. 
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Obsolete: Please use getIdentity().")]
        public ResponseData identity(string uuid)
        {
            init();
            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY + uuid)); 
        }
   
        /// <summary>
        /// Queries for a historical identity record
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData identityHistory(string uuid)
        {
            init();
            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY + uuid + "/history"));
        }

        /// <summary>
        /// Return a list of insurance prices for a given procedure (by CPT Code) in a given region (by ZIP Code).
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#insuranceprices
        /// </summary>
        /// <returns>The insurance.</returns>
        /// <param name="parameters">
        /// Query parameters:
        /// 	cpt_code, {string} The CPT code of the procedure in question.
        ///		zip_code, {string} Postal code in which to search for procedures
        /// </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData insurancePrices(Dictionary<string, string> parameters = null)
        {
            return pricesInsurance(parameters);
        }

        /// <summary>
        /// Return a list of insurance prices for a given procedure (by CPT Code) in a given region (by ZIP Code).
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#insuranceprices
        /// </summary>
        /// <returns>The insurance.</returns>
        /// <param name="parameters">
        /// Query parameters:
        /// 	cpt_code, {string} The CPT code of the procedure in question.
        ///		zip_code, {string} Postal code in which to search for procedures
        /// </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Obsolete: Please use insurancePrices().")]
        public ResponseData pricesInsurance(Dictionary<string, string> parameters = null)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PRICE_INSURANCE, parameters));
        }

        /// <summary>
        /// The Medical Procedure Code resource provides access to clinical and consumer friendly information related
        /// to medical procedures.
        /// </summary>
        /// <param name="medical_procedure_code">Retrieve the data for a specific procedure code.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData mpc(string medical_procedure_code)
        {
            return medicalProcedureCode(medical_procedure_code);
        }


        /// <summary>
        /// The Medical Procedure Code resource provides access to clinical and consumer friendly information related 
        /// to medical procedures.
        /// </summary>
        /// <param name="medical_procedure_code">Retrieve the data for a specific procedure code.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Obsolete: Please use mpc().")]
        public ResponseData medicalProcedureCode(string medical_procedure_code)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_MPC + medical_procedure_code));
        }

        /// <summary>
        /// The Medical Procedure Code resource provides access to clinical and consumer friendly information related 
        /// to medical procedures.
        /// </summary>
        /// <param name="parameters">
        /// Query parameters:
        ///     name, Search medical procedure information by consumer friendly name 
        ///     description, A partial or full description to be used to locate medical procedure information 
        /// </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData medicalProcedureCode(Dictionary<string, string> parameters)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_MPC, parameters));
        }



        /// <summary>
        /// Load a price for a given cpt_bundle and trading_partner_id
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData oopInsuranceLoadPrice(Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_OOP_INSURANCE_LOAD_PRICES, parameters));
        }

        /// <summary>
        /// Delete a loaded price given the loaded price's uuid
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData oopInsuranceDeletePrice(string price_uuid)
        {
            init();

            return applyResponse(DeleteRequest(POKITDOK_PLATFORM_API_OOP_INSURANCE_LOAD_PRICES + "/" +  price_uuid));
        }


        /// <summary>
        /// Returns estimated out of pocket cost and eligibility information for a given procedure
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData oopInsuranceEstimate(Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_OOP_INSURANCE_ESTIMATE, parameters));
        }


        /// <summary>
        /// Validate’s the identity fields provided by the patient.
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData validateIdentity(Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY_VALIDATE, parameters));
        }

        /// <summary>
        /// Scores the patient’s response to a KBA question.
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData answerProofQuestion(Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY_SCORE_QUESTIONS, parameters));
        }

        /// <summary>
        /// Generates a new KBA questionnaire.
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData createProofQuestion(Dictionary<string, object> parameters)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_IDENTITY_GENERATE_QUESTIONS, parameters));
        }

        /// <summary>
        /// Use the /payers/ API to determine available payer_id values for use with other endpoints
        /// The Payers endpoint will be deprecated in v5. Use Trading Partners instead.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#payers
        /// </summary>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Warning: Will be removed in the 4.0 release.")]
        public ResponseData payers()
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PAYERS));
        }

        /// <summary>
        /// Retrieve data on plans based on the parameters given.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#plans
        /// </summary>
        /// <param name="parameters">
        /// Query parameters:
        /// 	trading_partner_id, The trading partner id of the payer offering the plan
        ///		county, The county in which the plan is available.
        ///		state, The state in which the plan is available.
        ///		plan_id, The identifier for the plan.
        ///		plan_type, The type of the plan (e.g. EPO, PPO, HMO, POS).
        ///		plan_name, The name of the plan.
        ///		metallic_level, The metal level of the plan.
        /// </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData plans(Dictionary<string, string> parameters = null)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PLANS, parameters));
        }

        /// <summary>
        /// Retrieve the data for a specified provider.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#providers
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
        /// Retrieve providers data matching specified query parameters
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#providers
        /// </summary>
        /// <param name="parameters">
        /// Query parameters:
        /// 	organization_name, The business practice name
        ///		first_name, Provider first name
        ///		last_name, Provider first name
        ///		specialty, Provider specialty name from NUCC/NPI taxonomy
        ///		city, Provider city
        ///		state, Provider state
        ///		zipcode, Provider 5-digit zip code
        ///		radius, Search distance from geographic centerpoint, with unit (e.g. “1mi” or “50mi”)
        ///			(Only used when city, state, or zipcode is passed)
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
        /// Request approval for a referral to another health care provider.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#referrals
        /// </summary>
        /// <param name="postData">Dictionary representing JSON post data.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData referrals(Dictionary<string, object> postData)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_REFERRALS, postData));
        }

        /// <summary>
        /// Get a list of supported scheduling systems and their UUIDs and descriptions.
        /// </summary>
        /// <param name="scheduler_uuid">Optional, Retrieve the data for a specified scheduling system.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData schedulers(string scheduler_uuid = "")
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_SCHEDULERS + scheduler_uuid));
        }
       
        /// <summary>
        /// Retrieve a list of trading partners or submit an id to get info for a
        /// specific trading partner.
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#tradingpartners
        /// </summary>
        /// <param name="npi">Trading Partner Identifier.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData tradingPartners(string npi = "")
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_TRADING_PARTNERS + npi));
        }

        /// <summary>
        /// Get a list of appointment types, their UUIDs, and descriptions.
        /// </summary>
        /// <param name="appointment_type_uuid">Optional, Retrieve the data for a specified appointment type.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData appointmentTypes(string appointment_type_uuid = "")
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENT_TYPES + appointment_type_uuid));
        }

        /// <summary>
        /// Create an available appointment slot in the PokitDok scheduler system
        /// </summary>
        /// <param name="postData">Available appointment slot details.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData scheduleSlots(Dictionary<string, object> parameters)
        {
            return createSlot(parameters);
        }

        /// <summary>
        /// Create an available appointment slot in the PokitDok scheduler system
        /// </summary>
        /// <param name="postData">Available appointment slot details.</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Obsolete: Please use scheduleSlots().")]
        public ResponseData createSlot(Dictionary<string, object> postData)
        {
            init();

            return applyResponse(PostRequest(POKITDOK_PLATFORM_API_ENDPOINT_SLOTS, postData));
        }


        /// <summary>
        /// Query for an open appointment slot or a booked appointment given a specific {pd_appointment_uuid},
        /// the (PokitDok unique appointment identifier).
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="appointment_uuid">The (PokitDok unique appointment identifier).</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData getAppointments(string appointment_uuid = "")
        {
            return appointments(appointment_uuid);
        }
        /// <summary>
        /// Query for an open appointment slot or a booked appointment given a specific {pd_appointment_uuid}, 
        /// the (PokitDok unique appointment identifier).
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="appointment_uuid">The (PokitDok unique appointment identifier).</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        [Obsolete("Obsolete: Please use getAppointments().")]
        public ResponseData appointments(string appointment_uuid = "")
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENTS + appointment_uuid));
        }

        /// <summary>
        /// Query for open appointment slots (using pd_provider_uuid and location) or booked appointments 
        /// (using patient_uuid) given query parameters. See https://platform.pokitdok.com/documentation
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="parameters">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData appointments(Dictionary<string, string> parameters)
        {
            init();

            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENTS, parameters));
        }

        /// <summary>
        /// Book appointment for an open slot. Post data contains patient attributes and description.
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="appointment_uuid">The (PokitDok unique appointment identifier).</param>
        /// <param name="putData">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData bookAppointment(string appointment_uuid, Dictionary<string, object> putData)
        {
            init();

            return applyResponse(PutRequest(POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENTS + appointment_uuid, putData));
        }

        /// <summary>
        /// Update appointment description.
        /// See https://platform.pokitdok.com/documentation 
        /// </summary>
        /// <param name="appointment_uuid">The (PokitDok unique appointment identifier).</param>
        /// <param name="putData">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData updateAppointment(string appointment_uuid, Dictionary<string, object> putData)
        {
            init();

            return applyResponse(PutRequest(POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENTS + appointment_uuid, putData));
        }

        /// <summary>
        /// Cancel appointment given its {pd_appointment_uuid}.
        /// See https://platform.pokitdok.com/documentation 
        /// </summary>
        /// <param name="appointment_uuid">The (PokitDok unique appointment identifier).</param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData cancelAppointment(string appointment_uuid)
        {
            init();

            return applyResponse(DeleteRequest(POKITDOK_PLATFORM_API_ENDPOINT_APPOINTMENTS + appointment_uuid));
        }


        /// <summary>
        /// Pharmacy Plan information
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="parameters">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        ///          The body is JSON formatted data.
        /// </returns>
        public ResponseData pharmacyPlans(Dictionary<string, string> parameters)
        {
            init();
        
            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_PLANS, parameters));
        }
        
        /// <summary>
        /// Pharmacy Formulary information
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="parameters">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        ///          The body is JSON formatted data.
        /// </returns>
        public ResponseData pharmacyFormulary(Dictionary<string, string> parameters)
        {
            init();
        
            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_FORMULARY, parameters));
        }
        
        /// <summary>
        /// Search for in-network pharmacies
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="parameters">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData pharmacyNetwork(Dictionary<string, string> parameters)
        {
            init();
        
            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_NETWORK, parameters));
        }
        
        /// <summary>
        /// Check if a pharmacy is in-network using an NPI
        /// See https://platform.pokitdok.com/documentation
        /// </summary>
        /// <param name="parameters">See https://platform.pokitdok.com/documentation </param>
        /// <exception cref="pokitdokcsharp.PokitDokException">Thrown when unknown system error occurs.</exception>
        /// <returns>The http response as a <see cref="pokitdokcsharp.ResponseData"/> object.
        /// 	The body is JSON formatted data.
        /// </returns>
        public ResponseData pharmacyNetwork(string npi, Dictionary<string, string> parameters)
        {
            init();
        
            return applyResponse(GetRequest(POKITDOK_PLATFORM_API_ENDPOINT_PHARMACY_NETWORK+"/"+npi, parameters));
        }

        /// <summary>
        /// POST a file to a given endpoint. 
        /// </summary>
        /// <param name="endpoint">e.g. `/activities`, `/eligibility`, etc.</param>
        /// <param name="postFilePath"></param>
        /// <param name="postFileContentDispositionName"></param>
        /// <param name="postFileContentType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResponseData request(
            string endpoint, 
            string postFilePath, 
            string postFileContentDispositionName, 
            string postFileContentType, 
            Dictionary<string, string> parameters = null)
        {
            init(); 

            string url = endpoint;
            return applyResponse(PostRequest(url, postFilePath, postFileContentDispositionName, postFileContentType, parameters)); 
        }

        /// <summary>
        /// Submit a request to any endpoint. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">e.g. `/activities`, `/eligibility`, etc.</param>
        /// <param name="method">POST, PUT, GET, DELETE</param>
        /// <param name="parameters">A dictionary of either (string, object) or (string, string)</param>
        /// <returns></returns>
        public ResponseData request(
                string endpoint, 
                string method, 
                object parameters = null)
        {
            init(); 

            string url = endpoint; 

            if (method == "POST")
            {
                return applyResponse(PostRequest(url, (Dictionary<string, object>)parameters));
            }

            else if (method == "PUT")
            {
                return applyResponse(PutRequest(url, (Dictionary<string, object>)parameters));
            }

            else if (method == "GET")
            {
                return applyResponse(GetRequest(url, (Dictionary<string, string>)parameters));
            }

            else if (method == "DELETE")
            {
                return applyResponse(DeleteRequest(url));
            }
            else throw new NotSupportedException("Must provide POST, PUT, GET, or DELETE method"); 

            return null; 
        }

        /// <summary>
        /// Usage statistics for most recent request
        /// See docs here: https://platform.pokitdok.com/documentation/v4#/#overview
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
            dynamic responseObject = null;

            try
            {
                responseObject = JsonConvert.DeserializeObject(response.body);
            }
            catch (Exception)
            {
                _usage = null;
                _data = null;
                _errors = "Error deserializing response body.";
                return response;
            }

            if (responseObject == null)
            {
                _usage = null;
                _data = null;
                _errors = null;
            } else {
                _usage = responseObject ["meta"];
                _data = responseObject ["data"];
                if (_data is Newtonsoft.Json.Linq.JObject) {
                    _errors = _data ["errors"];
                } else {
                    _errors = null;
                }
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
                this.ApiLogoutUrl = _apiSite +_logoutPath;
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

