using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ChasmDeserializer.JSONConverters
{
    public class XNAVector2Converter : JsonConverter
    {
        public Formatting Identation { get; set; }
        public XNAVector2Converter(Formatting Formatting = Formatting.None) => Identation = Formatting;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vector2 = (Vector2)value;
            var X = vector2.X;
            var Y = vector2.Y;
            var format = writer.Formatting;
            writer.Formatting = Identation;
            var o = JObject.FromObject(new { X, Y });
            o.WriteTo(writer);
            writer.Formatting = format;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            float x = GetTokenValue(o, "X") ?? 0f;
            float y = GetTokenValue(o, "Y") ?? 0f;
            return new Vector2(x, y);
        }

        public override bool CanConvert(Type objectType) =>
            objectType.Equals(typeof(Vector2));

        private static float? GetTokenValue(JObject o, string tokenName) =>
            o.TryGetValue(tokenName, StringComparison.OrdinalIgnoreCase, out JToken t) ? (float)t : (float?)null;
    }
}
