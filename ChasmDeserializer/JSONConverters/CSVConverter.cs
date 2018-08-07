using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ChasmDeserializer.JSONConverters
{
    public class CSVConverter : JsonConverter
    {
        public Formatting Identation { get; set; }
        public override bool CanRead => false;

        public CSVConverter(Formatting Formatting = Formatting.None)
        {
            Identation = Formatting;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(CSVData));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
