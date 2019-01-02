using System;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ChasmDeserializer.Extensions;

namespace ChasmDeserializer.Model
{
    public class Item
    {
        public string ID;
        public int Count;
        public int Experience;
        public ItemRarity Rarity;
        public int Mod_CON;
        public int Mod_HP;
        public int Mod_INT;
        public int Mod_LCK;
        public int Mod_MP;
        public int Mod_STR;

        [JsonIgnore]
        private bool UncommonRarity => Rarity > ItemRarity.Common;
        [JsonIgnore]
        private static readonly string[] familiars = { "head_birdhat", "head_birdhat_blue", "head_swordhat" };

        public static Item Load(BinaryReader reader, float saveVersion)
        {
            var id = reader.ReadString();
            if (id == "null")
            {
                return new Item() { ID = "empty" };
            }
            var item = new Item();
            item = String.Equals(id.Trim(), "sword_assassins", StringComparison.OrdinalIgnoreCase)
                                           ? new AssassinsSword()
                                           : familiars.Contains(id, StringComparer.OrdinalIgnoreCase)
                                           ? new FamiliarItem()
                                           : item;
            item.ID = id;
            item.Count = reader.ReadInt32();
            item.Rarity = (ItemRarity)reader.ReadInt32();
            if (item.Rarity > ItemRarity.Common)
	        {
		        item.Mod_CON = reader.ReadInt32();
		        item.Mod_HP = reader.ReadInt32();
		        item.Mod_INT = reader.ReadInt32();
		        item.Mod_LCK = reader.ReadInt32();
		        item.Mod_MP = reader.ReadInt32();
		        item.Mod_STR = reader.ReadInt32();
	        }
            if (saveVersion >= 1.76f)
            {
                item.Experience = reader.ReadInt32();
            }
            item.LoadCustom(reader);
            return item;
        }

        public virtual void Save(BinaryWriter writer)
        {
            if (ID == null || ID == "empty" || ID == "")
            {
                writer.Write("null");
                return;
            }
            writer.Write(ID);
            writer.Write(Count);
            writer.Write((int)Rarity);
            if (this.Rarity > ItemRarity.Common)
	        {
		        writer.Write(this.Mod_CON);
		        writer.Write(this.Mod_HP);
		        writer.Write(this.Mod_INT);
		        writer.Write(this.Mod_LCK);
		        writer.Write(this.Mod_MP);
		        writer.Write(this.Mod_STR);
	        }
            writer.Write(Experience);
        }

        public virtual void LoadCustom(BinaryReader reader) {}

        public bool ShouldSerializeMod_CON() => UncommonRarity;
        public bool ShouldSerializeMod_HP() => UncommonRarity;
        public bool ShouldSerializeMod_INT() => UncommonRarity;
        public bool ShouldSerializeMod_LCK() => UncommonRarity;
        public bool ShouldSerializeMod_MP() => UncommonRarity;
        public bool ShouldSerializeMod_STR() => UncommonRarity;

    }

    public class AssassinsSword : Item
    {
        public int CurrentKills;
        public int Level;

       public override void LoadCustom(BinaryReader reader)
        {
            base.LoadCustom(reader);
            this.Level = reader.ReadInt32();
            this.CurrentKills = reader.ReadInt32();
        }
        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(this.Level);
            writer.Write(this.CurrentKills);
        }
    }

    public class FamiliarItem : Item
    {
        public Vector2 Position;
        public int LVL;
        public int STR;
        public int FamiliarExperience;

        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(this.Position);
            writer.Write(this.LVL);
            writer.Write(this.STR);
            writer.Write(this.FamiliarExperience);
        }
        public override void LoadCustom(BinaryReader read)
        {
            this.Position = read.ReadVector2();
            this.LVL = read.ReadInt32();
            this.STR = read.ReadInt32();
            this.FamiliarExperience = read.ReadInt32();
        }
    }
}
