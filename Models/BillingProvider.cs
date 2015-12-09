namespace pokitdokcsharp.Models
{
    /// <summary>
    /// information for the provider that is billing for services.
    /// </summary>
    public class BillingProvider : BaseProvider
    {
        /// <summary>
        /// A dictionary of information for the billing provider’s address.
        /// </summary>
        /// <value>The address.</value>
        public Address Address { get; set; }

        /// <summary>
        /// The federal tax id for the provider billing for services. 
        /// For individual providers, this may be the tax id of the medical practice or organization where a provider works.
        /// </summary>
        /// <value>The tax identifier.</value>
        public string TaxId { get; set; }
    }
}