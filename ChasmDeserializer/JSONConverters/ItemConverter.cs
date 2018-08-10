using ChasmDeserializer.Model;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ChasmDeserializer.JSONConverters
{
    class ItemConverter : JsonConverter
    {
        private static readonly string[] familiars = { "head_birdhat", "head_birdhat_blue", "head_swordhat" };
        public Formatting Identation { get; set; }
        public override bool CanWrite => false;

        public ItemConverter(Formatting Formatting = Formatting.Indented)
        {
            Identation = Formatting;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Model.Item));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            JToken x;
            var ID = o.TryGetValue("ID", StringComparison.OrdinalIgnoreCase, out x) ? (string)x : string.Empty;
            var Count = o.TryGetValue("Count", StringComparison.OrdinalIgnoreCase, out x) ? (int)x : 0;
            var Experience = o.TryGetValue("Experience", StringComparison.OrdinalIgnoreCase, out x) ? (int)x : 0;
            var tier = o.TryGetValue("UnlockTier", StringComparison.OrdinalIgnoreCase, out x) ? (int)x : 0;
            var UnlockTier = (UnlockTier)tier;
            if (o.Count > 4)
            {
                if (String.Equals(ID, "sword_assassins", StringComparison.OrdinalIgnoreCase))
                {
                    var aItem = new AssassinsSword() { ID = ID, Count = Count, Experience = Experience, UnlockTier = UnlockTier };
                    JToken a;
                    var CurrentKills = o.TryGetValue("CurrentKills", StringComparison.OrdinalIgnoreCase, out a) ? (int)x : 0;
                    var Level = o.TryGetValue("Level", StringComparison.OrdinalIgnoreCase, out a) ? (int)x : 0;
                    aItem.CurrentKills = CurrentKills;
                    aItem.Level = Level;
                    return aItem;
                }
                else if (familiars.Contains(ID, StringComparer.OrdinalIgnoreCase))
                {
                    var fItem = new FamiliarItem() { ID = ID, Count = Count, Experience = Experience, UnlockTier = UnlockTier };
                    JToken f;
                    var Position = o.TryGetValue("Position", StringComparison.OrdinalIgnoreCase, out f) ? JsonConvert.DeserializeObject<Vector2>(f.ToString(), new XNAVector2Converter()) : default(Vector2);
                    var LVL = o.TryGetValue("LVL", StringComparison.OrdinalIgnoreCase, out f) ? (int)f : 0;
                    var STR = o.TryGetValue("STR", StringComparison.OrdinalIgnoreCase, out f) ? (int)f : 0;
                    var FamiliarExperience =  o.TryGetValue("FamiliarExperience", StringComparison.OrdinalIgnoreCase, out f) ? (int)f : 0;
                    fItem.Position = Position;
                    fItem.LVL = LVL;
                    fItem.STR = STR;
                    fItem.FamiliarExperience = FamiliarExperience;
                    return fItem;
                }
            }
            return new Item() { ID = ID, Count = Count, Experience = Experience, UnlockTier = UnlockTier };
        }
    }
}
