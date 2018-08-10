using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ChasmDeserializer.JSONConverters
{
    public class XNARectangleConverter : JsonConverter
    {
        public Formatting Identation { get; set; }
        public XNARectangleConverter(Formatting Formatting = Formatting.None)
        {
            Identation = Formatting;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rectangle = (Rectangle)value;

            var x = rectangle.X;
            var y = rectangle.Y;
            var width = rectangle.Width;
            var height = rectangle.Height;
            var format = writer.Formatting;
            var o = JObject.FromObject(new { x, y, width, height });
            writer.Formatting = Identation;
            o.WriteTo(writer);
            writer.Formatting = format;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);

            var x = GetTokenValue(o, "x") ?? 0;
            var y = GetTokenValue(o, "y") ?? 0;
            var width = GetTokenValue(o, "width") ?? 0;
            var height = GetTokenValue(o, "height") ?? 0;

            return new Rectangle(x, y, width, height);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Rectangle));
        }

        private static int? GetTokenValue(JObject o, string tokenName)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.OrdinalIgnoreCase, out t) ? (int)t : (int?)null;
        }
    }
}
