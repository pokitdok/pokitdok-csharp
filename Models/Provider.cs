using System.ComponentModel;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Represents the provider API arguments
    /// </summary>
    public class Provider : BaseProvider
    {
        /// <summary>
        /// N/A for Provider
        /// </summary>
        /// <value>The taxonomy code.</value>
        [Browsable(false)]
        public override string TaxonomyCode => null;
    }
}