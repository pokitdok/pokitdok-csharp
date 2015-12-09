using Newtonsoft.Json;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models.Transactions
{
    /// <summary>
    /// Class ClaimsTransaction.
    /// </summary>
    public class ClaimsTransaction : BaseTransaction
    {
        /// <summary>
        /// Required: The type of claim baseTransaction that is being submitted (e.g. “chargeable”).
        /// </summary>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public TransactionCode TransactionCode { get; set; }

        /// <summary>
        /// Required: Information for the provider that is billing for services.
        /// </summary>
        /// <value>The billing provider.</value>
        public BillingProvider BillingProvider { get; set; }

        /// <summary>
        /// information representing a claim for services that have been performed by a health care provider for the patient.
        /// </summary>
        /// <value>The claim.</value>
        public Claim Claim { get; set; }

        /// <summary>
        /// Information about the patient that received services outlined in the claim. Patient information is only required when the patient is not the insurance subscriber.
        /// </summary>
        /// <value>The patient.</value>
        public Patient Patient { get; set; }

        /// <summary>
        /// Information about the insurance subscriber as it appears on their policy.
        /// </summary>
        /// <value>The subscriber.</value>
        public Subscriber Subscriber { get; set; }
    }
}