using System;
using Newtonsoft.Json.Serialization;

namespace pokitdokcsharp.JSONFormatters
{
    /// <summary>
    /// This class overrides default JSON serialization to get the naming convention to match Ruby standards.
    /// </summary>
    public class UnderscoreCaseResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Resolved name of the property.
        /// </returns>
        protected override string ResolvePropertyName(string propertyName)
            => propertyName.ToUnderscoreCase();
    }
}