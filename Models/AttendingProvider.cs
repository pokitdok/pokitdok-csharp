using System.ComponentModel;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// information for the attending provider on this claim.
    /// </summary>
    public class AttendingProvider : BaseProvider
    {
        /// <summary>
        /// N/A for attending provider
        /// </summary>
        /// <value>The name of the organization.</value>
        [Browsable(false)]
        public override string OrganizationName => null;
    }
}