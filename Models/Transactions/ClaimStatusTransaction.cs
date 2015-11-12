using System;
using Newtonsoft.Json;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models.Transactions
{
    /// <summary>
    /// The Claims Status Endpoint allows an application to request information about previously submitted claims. 
    /// You can send a request to a payer to determine where the claim is in their adjudication system and the status of the claim.
    /// The PokitDok Claims Status Endpoint can be used to query the status of multiple claims.
    /// To learn how to form such a request and understand how the Claims and Claims Status Endpoints work together, see claims API workflow.
    /// Please note that on average it takes 5-7 days for a claim to enter a payer’s adjudication system, thus it recommended to wait at least a week after submitting 
    /// a claim to check its status.
    /// </summary>
    public class ClaimStatusTransaction : BaseTransaction
    {
        /// <summary>
        /// Required: Information for the provider that is billing for services.
        /// </summary>
        /// <value>The billing provider.</value>
        public Provider Provider { get; set; }

        /// <summary>
        /// Information about the patient that received services outlined in the claim. Patient information is only required when the patient is not the insurance subscriber.
        /// </summary>
        /// <value>The patient.</value>
        public BasicEntity Patient { get; set; }

        /// <summary>
        /// Information about the insurance subscriber as it appears on their policy.
        /// </summary>
        /// <value>The subscriber.</value>
        public BasicEntity Subscriber { get; set; }

        /// <summary>
        /// The date services were performed or started for the claim service period.
        /// </summary>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime ServiceDate { get; set; }

        /// <summary>
        /// Optional: The date services ended for the claim service period.
        /// </summary>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime ServiceEndDate { get; set; }

        /// <summary>
        ///  Optional: The payer’s claim tracking id. Specify a tracking id to refine the search criteria for a specific claim. 
        /// </summary>
        public string TrackingId { get; set; }
    }
}