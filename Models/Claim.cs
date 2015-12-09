using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using NUnit.Framework;
using pokitdokcsharp.JSONFormatters;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// information representing a claim for services that have been performed by a health care provider for the patient.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// (Institutional claim specific) The date the patient was admitted. UB-04 field: 12. Admission Date
        /// </summary>
        /// <value>The admission date.</value>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime AdmissionDate { get; set; }

        /// <summary>
        /// (Institutional claim specific) The source of the patient’s admission.
        /// </summary>
        /// <value>The admission source.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public AdmissionSource AdmissionSource { get; set; }

        /// <summary>
        /// (Institutional claim specific) The admission/type priority of the patient’s admission. 
        /// </summary>
        /// <value>The type of the admission.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public AdmissionType AdmissionType { get; set; }

        /// <summary>
        /// (Institutional claim specific) The type of facility where the patient was admitted
        /// </summary>
        /// <value>The type of the facility.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public FacilityType FacilityType { get; set; }

        /// <summary>
        /// The patient’s medical record number.
        /// </summary>
        /// <value>The medical record number.</value>
        public string MedicalRecordNumber { get; set; }

        /// <summary>
        /// Optional: the date of first symptoms for the illness.
        /// </summary>
        /// <value>The onset date.</value>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime OnsetDate { get; set; }

        /// <summary>
        /// The location where services were performed (e.g. office)
        /// </summary>
        /// <value>The place of service.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public PlaceOfService PlaceOfService { get; set; }

        /// <summary>
        /// Optional: The amount the patient has already paid the provider for the services listed in the claim.
        /// When reporting cash payment encounters for the purpose of contributing those amounts toward the member’s deductible, 
        /// the patient_paid_amount will equal the total_charge_amount.
        /// </summary>
        /// <value>The patient paid amount.</value>
        public float PatientPaidAmount { get; set; }

        /// <summary>
        /// Boolean indicator for whether or not a patient’s signature is on file to authorize the release of medical records. Defaults to true if not specified.
        /// </summary>
        /// <value><c>true</c> if [patient signature on file]; otherwise, <c>false</c>.</value>
        public bool PatientSignatureOnFile { get; set; } = true;

        /// <summary>
        /// (Institutional claim specific) The patient’s status as of the dates covered through the statement. 
        /// </summary>
        /// <value>The patient status.</value>
        [JsonConverter(typeof (UnderscoreCaseEnumConverter))]
        public PatientStatus PatientStatus { get; set; }

        /// <summary>
        /// The (start) date of this statement.
        /// </summary>
        /// <value>The statement date.</value>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime StatementDate { get; set; }

        /// <summary>
        /// The end date of this statement.
        /// </summary>
        /// <value>The statement end date.</value>
        [JsonConverter(typeof (YyyyMmDdTimeConverter))]
        public DateTime StatementEndDate { get; set; }

        /// <summary>
        /// (Institutional claim specific) The value code that applies to this claim.
        /// </summary>
        /// <value>The value information.</value>        
        public ValueInformation ValueInformation { get; set; } = null;

        /// <summary>
        /// List of services that were performed as part of this claim.
        /// </summary>
        /// <value>The service lines.</value>
        public List<ServiceLine> ServiceLines { get; set; } = new List<ServiceLine>();

        /// <summary>
        /// The total amount charged/billed for the claim. (e.g. 100.00)
        /// </summary>
        /// <value>The total charge amount.</value>
        public double TotalChargeAmount { get; set; }
    }
}