using Newtonsoft.Json.Converters;

namespace pokitdokcsharp.JSONFormatters
{
    internal class YyyyMmDdTimeConverter : IsoDateTimeConverter
    {
        public YyyyMmDdTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}