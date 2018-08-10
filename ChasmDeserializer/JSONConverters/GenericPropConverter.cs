using ChasmDeserializer.Model;
using ChasmDeserializer.Model.SaveGameData.WorldState;
using ChasmDeserializer.Model.SaveGameData.WorldState.Saveable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ChasmDeserializer.JSONConverters
{
    class GenericPropConverter : JsonConverter
    {
        private const string assemblyPath = "ChasmDeserializer.Model.SaveGameData.WorldState.Saveable.";
        public Formatting Identation { get; set; }
        public override bool CanWrite => false;

        public GenericPropConverter(Formatting Formatting = Formatting.Indented) => Identation = Formatting;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) => objectType.Equals(typeof(PropData));

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            JToken x;
            var fullName = o.TryGetValue("PropFullName", StringComparison.OrdinalIgnoreCase, out x) ? (string)x : string.Empty;
            string value = o.TryGetValue("PopData", StringComparison.OrdinalIgnoreCase, out x) ? x.ToString() : string.Empty;
            string localName = fullName.IndexOf("ComboLock", StringComparison.OrdinalIgnoreCase) > -1
                ? "ComboLockHint"
                : fullName.Substring(fullName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase) + 1);
            Type type = Type.GetType(assemblyPath + localName);
            var item = JsonConvert.DeserializeObject(value, type, new XNARectangleConverter(), new XNAVector2Converter());
            return new PropData(fullName, item as GenericProp);
        }
    }
}
