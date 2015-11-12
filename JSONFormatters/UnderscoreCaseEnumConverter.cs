using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace pokitdokcsharp.JSONFormatters
{
    /// <summary>
    /// Class EnumConverter.
    /// </summary>
    public class UnderscoreCaseEnumConverter : StringEnumConverter
    {
        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var strValue = value.ToString();

            if (string.IsNullOrWhiteSpace(strValue))
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(strValue.ToUnderscoreCase());
        }
    }
}