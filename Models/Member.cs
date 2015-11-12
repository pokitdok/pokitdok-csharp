namespace pokitdokcsharp.Models
{
    /// <summary>
    /// This class represents the member arguments for the API.
    /// </summary>
    public class Member : BaseEntity
    {
        /// <summary>
        /// The named insured’s member identifier. May be omitted if member.birth_date is provided.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
    }
}