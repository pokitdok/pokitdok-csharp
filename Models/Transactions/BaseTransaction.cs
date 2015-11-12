namespace pokitdokcsharp.Models.Transactions
{
    /// <summary>
    /// Class BaseTransaction.
    /// </summary>
    public abstract class BaseTransaction
    {
        /// <summary>
        /// Gets or sets the trading partner identifier.
        /// </summary>
        /// <value>The trading partner identifier.</value>
        public string TradingPartnerId { get; set; }

        /// <summary>
        /// Gets or sets the application data.
        /// Optional, used for callback tracking.
        /// </summary>
        /// <value>The application data.</value>
        public ApplicationData ApplicationData { get; set; }
    }
}