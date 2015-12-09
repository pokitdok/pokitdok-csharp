using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Information about the patient that received services outlined in the claim. Patient information is only required when the patient is not the insurance subscriber.
    /// </summary>
    public class Patient : BaseEntity
    {

        /// <summary>
        /// Required: The patient’s address information.
        /// </summary>
        /// <value>The address.</value>
        public Address Address { get; set; }

        /// <summary>
        /// The patient’s gender.
        /// </summary>
        /// <value>The gender.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public Gender Gender { get; set; }

        /// <summary>
        /// Required: the member identifier.
        /// </summary>
        /// <value>The member identifier.</value>
        public string MemberId { get; set; }

        /// <summary>
        /// Optional: The patient’s middle name.
        /// </summary>
        /// <value></value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Patient pregnancy indicator. Defaults to false
        /// </summary>
        /// <value><c>true</c> if pregnant; otherwise, <c>false</c>.</value>
        public bool Pregnant { get; set; }

        /// <summary>
        /// Required: The patient’s relationship to the subscriber
        /// </summary>
        /// <value>The relationship.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public Relationship Relationship { get; set; }
    }
}