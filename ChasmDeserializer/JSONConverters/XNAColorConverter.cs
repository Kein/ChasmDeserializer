using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ChasmDeserializer.JSONConverters
{
    class XNAColorConverter : JsonConverter
    {
        public Formatting Identation { get; set; }
        public XNAColorConverter(Formatting Formatting = Formatting.None)
        {
            Identation = Formatting;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var color = (Color)value;
            var R = color.R;
            var G = color.G;
            var B = color.B;
            var A = color.A;
            var format = writer.Formatting;
            writer.Formatting = Identation;
            var o = JObject.FromObject(new { R, G, B, A });
            o.WriteTo(writer);
            writer.Formatting = format;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            byte R = GetTokenValue(o, "R") ?? 0;
            byte G = GetTokenValue(o, "G") ?? 0;
            byte B = GetTokenValue(o, "B") ?? 0;
            byte A = GetTokenValue(o, "A") ?? 0;
            return new Color(R,G,B,A);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Color));
        }

        private static byte? GetTokenValue(JObject o, string tokenName)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.InvariantCultureIgnoreCase, out t) ? (byte)t : (byte?)null;
        }
    }
}
