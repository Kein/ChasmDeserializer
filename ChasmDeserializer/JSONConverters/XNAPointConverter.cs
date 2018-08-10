using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChasmDeserializer.JSONConverters
{
    class XNAPointConverter : JsonConverter
    {
        public Formatting Identation { get; set; }
        public XNAPointConverter(Formatting Formatting = Formatting.None)
        {
            Identation = Formatting;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var point = (Point)value;
            var X = point.X;
            var Y = point.Y;
            var format = writer.Formatting;
            writer.Formatting = Identation;
            var o = JObject.FromObject(new { X, Y });
            o.WriteTo(writer);
            writer.Formatting = format;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            int x = GetTokenValue(o, "X") ?? 0;
            int y = GetTokenValue(o, "Y") ?? 0;
            return new Point(x, y);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Point));
        }

        private static int? GetTokenValue(JObject o, string tokenName)
        {
            JToken t;
            return o.TryGetValue(tokenName, StringComparison.OrdinalIgnoreCase, out t) ? (int)t : (int?)null;
        }
    }
}
