namespace pokitdokcsharp.Models
{
    /// <summary>
    /// API client applications may include custom application data in requests to help support scenarios where an application is unable to store the activity id and wishes to include application specific data in their API requests so that the information will be stored on the request’s activity and returned to the application in asynchronous callbacks. This can be useful for scenarios where you want to directly associate a PokitDok Platform API request with some identifier(s) in your system so that you can do direct lookups to associate responses with the appropriate information. For example, suppose you wish to fire off a number of eligibility or claims requests and want to include some identifiers specific to your application. By including the identifier(s) you need in the request’s application_data section, you can easily do direct lookups using those identifiers when you receive the API response.
    ///
    /// </summary>
    public class ApplicationData
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// Used only for internal tracking.
        /// </summary>
        /// <value>The patient identifier.</value>
        public string PatientId { get; set; }
        /// <summary>
        /// Gets or sets the location identifier.
        /// Used only for internal tracking.
        /// </summary>
        /// <value>The location identifier.</value>
        public string LocationId { get; set; }
        /// <summary>
        /// Gets or sets the transaction UUID.
        /// </summary>
        /// <value>The transaction UUID.</value>
        public string TransactionUuid { get; set; } 
    }
}