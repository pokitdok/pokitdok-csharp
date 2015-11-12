using Newtonsoft.Json;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Information about the insurance subscriber as it appears on their policy.
    /// </summary>
    public class BasicEntity : BaseEntity
    {
        /// <summary>
        /// Optional: The subscriber’s member identifier. Specify when the patient is not the subscriber.
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// Information about the insurance subscriber as it appears on their policy.
    /// </summary>
    public class Subscriber : BaseEntity
    {
        /// <summary>
        /// The subscriber’s address information as specified on their policy.
        /// </summary>
        /// <value>The address.</value>
        public Address Address { get; set; }

        /// <summary>
        /// Indicates the type of payment for the claim. It is an optional field and when left blank or not passed in the request, defaults to “mutually_defined”. 
        /// </summary>
        /// <value>The claim filing code.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public ClaimFilingCode ClaimFilingCode { get; set; }

        /// <summary>
        /// The subscriber’s gender as specified on their policy.
        /// </summary>
        /// <value>The gender.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Optional: The subscriber’s group or policy number as specified on their policy.
        /// </summary>
        /// <value>The group number.</value>
        public string GroupNumber { get; set; }

        /// <summary>
        /// Optional: The subscriber’s group name as specified on their policy.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName { get; set; }

        /// <summary>
        /// Required: The subscriber’s member identifier.
        /// </summary>
        /// <value>The member identifier.</value>
        public string MemberId { get; set; }
    }
}