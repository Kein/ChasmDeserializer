using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChasmDeserializer.JSONConverters
{
    class StringCollectionConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(JsonConvert.SerializeObject(value, Formatting.None));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(string[])) || objectType.Equals(typeof(List<string>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
