using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Class BaseEntity.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// The named insured’s first name as specified on their policy.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// The named insured’s last name as specified on their policy.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// The named insured’s birth date as specified on their policy. May be omitted if member.id is provided.
        /// </summary>
        /// <value>The birth date.</value>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime BirthDate { get; set; }
    }
}