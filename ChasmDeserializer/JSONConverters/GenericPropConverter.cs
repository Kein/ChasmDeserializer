using ChasmDeserializer.Model;
using ChasmDeserializer.Model.SaveGameData.WorldState.Saveable;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ChasmDeserializer.JSONConverters
{
    class GenericPropConverter : JsonConverter
    {
        public Formatting Identation { get; set; }
        public override bool CanWrite => false;

        public GenericPropConverter(Formatting Formatting = Formatting.Indented)
        {
            Identation = Formatting;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(KeyValuePair<string, GenericProp>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            JToken x;
            var fullName = o.TryGetValue("Key", StringComparison.OrdinalIgnoreCase, out x) ? (string)x : string.Empty;
            string value = o.TryGetValue("Value", StringComparison.OrdinalIgnoreCase, out x) ? x.ToString() : string.Empty;
            string localName = fullName.IndexOf("ComboLock", StringComparison.OrdinalIgnoreCase) > -1
                ? "ComboLockHint"
                : fullName.Substring(fullName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase) + 1);
            Type type = Type.GetType("ChasmDeserializer.Model.SaveGameData.WorldState.Saveable." + localName);
            var item = JsonConvert.DeserializeObject(value, type, new XNARectangleConverter(), new XNAVector2Converter());
            GenericProp finalItem = null;
            if (type == typeof(Crate))
                finalItem = item as Crate;
            else if (type == typeof(ComboLockHint))
                finalItem = item as ComboLockHint;
            else if (type == typeof(DestructibleWall))
                finalItem = item as DestructibleWall;
            else if (type == typeof(SaveableProp))
                finalItem = item as SaveableProp;
            else if (type == typeof(TreasureChest))
                finalItem = item as TreasureChest;
            else if (type == typeof(Shrine))
                finalItem = item as Shrine;
            return new KeyValuePair<string, GenericProp>(fullName, finalItem);
        }
    }
}
