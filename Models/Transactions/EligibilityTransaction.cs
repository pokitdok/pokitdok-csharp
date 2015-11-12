namespace pokitdokcsharp.Models.Transactions
{
    /// <summary>
    /// Represents the 270 EDI File
    /// </summary>
    public class EligibilityTransaction : BaseTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EligibilityTransaction"/> class.
        /// </summary>
        public EligibilityTransaction()
        {
            Member = new Member();
            Provider = new Provider();
        }

        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        /// <value>The member.</value>
        public Member Member { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public Provider Provider { get; set; }

        /// <summary>
        /// The CPT code that should be used to request specific eligibility information. Note: requests based on CPT code are not supported by all trading partners.
        /// </summary>
        public string CptCode { get; set; }
    }
}