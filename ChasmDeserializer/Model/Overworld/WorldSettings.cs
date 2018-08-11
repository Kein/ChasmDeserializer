using ChasmDeserializer.Extensions;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public class WorldSettings
    {
        public List<OWVariable> OWVariableSettings;
        public int? PlayerLevel { get; set; }
        public string Head { get; set; }
        public string LeftHand { get; set; }
        public string RightHand { get; set; }
        public string Armor { get; set; }
        public string Accessory1 { get; set; }
        public string Accessory2 { get; set; }
        public List<string> InventoryItems { get; set; }
        public int? Gold { get; set; }
        public List<WorldTagVariation> WorldTagVariations;

        public void Load(BinaryReader reader)
        {
            int num = reader.ReadInt32();
            OWVariableSettings = new List<OWVariable>();
            for (int i = 0; i < num; i++)
            {
                OWVariable variable = new OWVariable();
                variable.Load(reader);
                this.OWVariableSettings.Add(variable);
            }
            int num2 = reader.ReadInt32();
            this.PlayerLevel = num2 == -1 ? null : new int?(num2);
            this.Head = reader.ReadString();
            this.LeftHand = reader.ReadString();
            this.RightHand = reader.ReadString();
            this.Armor = reader.ReadString();
            this.Accessory1 = reader.ReadString();
            this.Accessory2 = reader.ReadString();
            this.InventoryItems = reader.ReadStrings();
            this.Gold = new int?(reader.ReadInt32());
            int num3 = reader.ReadInt32();
            WorldTagVariations = new List<WorldTagVariation>();
            for (int j = 0; j < num3; j++)
            {
                WorldTagVariation worldTagVariation = new WorldTagVariation();
                worldTagVariation.Load(reader);
                this.WorldTagVariations.Add(worldTagVariation);
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.OWVariableSettings.Count);
            foreach (OWVariable owvariable in this.OWVariableSettings)
            {
                owvariable.Save(writer);
            }
            if (this.PlayerLevel != null)
            {
                writer.Write(this.PlayerLevel.Value);
            }
            else
            {
                writer.Write(-1);
            }
            writer.Write(this.Head.NullCheck());
            writer.Write(this.LeftHand.NullCheck());
            writer.Write(this.RightHand.NullCheck());
            writer.Write(this.Armor.NullCheck());
            writer.Write(this.Accessory1.NullCheck());
            writer.Write(this.Accessory2.NullCheck());
            writer.Write(this.InventoryItems);
            if (this.Gold != null)
            {
                writer.Write(this.Gold.Value);
            }
            else
            {
                writer.Write(-1);
            }
            writer.Write(this.WorldTagVariations.Count);
            foreach (var variation in this.WorldTagVariations)
            {
                variation.Save(writer);
            }
        }
    }
}
