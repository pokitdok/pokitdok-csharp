using System.Collections.Generic;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Class Address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The patient’s city information. (e.g. “SAN MATEO”)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The patient’s street address information. (e.g. [“123 N MAIN ST”])
        /// </summary>
        public List<string> AddressLines { get; set; } = new List<string>();

        /// <summary>
        /// The patient’s state information. (e.g. “CA”)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The patient’s zip/postal code. (e.g. “94401”)
        /// </summary>
        public string Zipcode { get; set; }
    }
}