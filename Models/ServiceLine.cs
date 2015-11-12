using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// List of services that were performed as part of this claim.
    /// </summary>
    public class ServiceLine
    {
        /// <summary>
        /// The amount charged for this specific service. (e.g. 100.00)
        /// </summary>
        /// <value>The charge amount.</value>
        public float ChargeAmount { get; set; }

        /// <summary>
        /// A list of diagnosis codes related to this service. (e.g. 487.1)
        /// </summary>
        /// <value>The diagnosis codes.</value>
        public List<string> DiagnosisCodes { get; set; } = new List<string>();

        /// <summary>
        /// The CPT code for the service that was performed
        /// </summary>
        /// <value>The procedure code.</value>
        public string ProcedureCode { get; set; }

        /// <summary>
        /// Optional: List of modifier codes for the specified procedure. (e.g. [“GT”])
        /// </summary>
        /// <value>The procedure modifier codes.</value>
        public List<string> ProcedureModifierCodes { get; set; }

        /// <summary>
        /// The provider’s control number.
        /// </summary>
        /// <value>The provider control number.</value>
        public string ProviderControlNumber { get; set; }

        /// <summary>
        /// (Institutional claim specific) The revenue code related to this service. UB-04 field: 42. Revenue Code
        /// </summary>
        /// <value>The revenue code.</value>
        public string RevenueCode { get; set; }

        /// <summary>
        /// The date the service was performed.
        /// </summary>
        /// <value>The service date.</value>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime ServiceDate { get; set; }

        /// <summary>
        /// Number of units of this service. (e.g. 1.0)
        /// </summary>
        /// <value>The unit count.</value>
        public float UnitCount { get; set; }

        /// <summary>
        /// The type of unit being described for this particular service’s unit count. Possible values include: units, days
        /// </summary>
        /// <value>The type of the unit.</value>
        public string UnitType { get; set; }
    }
}