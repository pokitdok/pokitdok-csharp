namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Class ValueInformation
    /// </summary>
    public class ValueInformation
    {
        /// <summary>
        /// Value Type
        /// </summary>
        public ValueInformationEnum ValueType { get; set; }
        /// <summary>
        /// Value Amount
        /// </summary>
        public string Value { get; set; } 
    }
}