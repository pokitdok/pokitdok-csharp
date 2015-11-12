using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// (Institutional claim specific) A dictionary of information related to the occurrence/frequency of the claim.
    /// </summary>
    public class OccurenceInformation
    {
        /// <summary>
        /// (Institutional claim specific) The type of claim-related occurrence for specifc dates.
        /// </summary>
        /// <value>The type of the occurence.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public OccurenceType OccurenceType { get; set; }

        /// <summary>
        /// (Institutional claim specific) The specific dates for the claim-related occurrence type. 
        /// </summary>
        /// <value>The occurrence dates.</value>
        public List<DateTime> OccurrenceDates { get; set; }
    }
}