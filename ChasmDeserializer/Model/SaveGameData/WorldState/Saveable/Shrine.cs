using ChasmDeserializer.Interfaces;
using System;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class Shrine : GenericProp, ISaveGame
    {
        public ShrineType Type;
        public string DroppedItem;
        public bool DisableBacking;

        public void SaveGame(BinaryWriter writer)
        {
            this.Save(writer);
        }

        public void LoadGame(BinaryReader reader)
        {
            this.Load(reader);
        }

        public override void Load(BinaryReader reader)
        {
            this.Type = (ShrineType)Enum.Parse(typeof(ShrineType), reader.ReadString());
            string text = reader.ReadString();
            DroppedItem = text != "null" ? text : DroppedItem;
            this.DisableBacking = reader.ReadBoolean();
            base.Load(reader);
        }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(this.Type.ToString());
            DroppedItem = DroppedItem ?? "null";
            writer.Write(this.DroppedItem);
            writer.Write(this.DisableBacking);
            base.Save(writer);
        }

    }
}
