using ChasmDeserializer.Extensions;
using ChasmDeserializer.Interfaces;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class TreasureChest : GenericProp, ISaveGame
    {
        public string DroppedItem { get; set; }
        public bool DisableSpawner { get; set; }
        public int TreasureClass { get; set; }
        public bool Elixir { get; set; }
        public string BlockedKey { get; set; }
        public bool IsMagicallyHidden { get; set; }

        public void SaveGame(BinaryWriter writer)
        {
            this.Save(writer);
        }

        public void LoadGame(BinaryReader reader)
        {
            this.Load(reader);
        }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(this.DroppedItem.NullCheck());
            writer.Write(this.DisableSpawner);
            writer.Write(this.TreasureClass);
            writer.Write(this.Elixir);
            writer.Write(this.BlockedKey.NullCheck());
            writer.Write(this.IsMagicallyHidden);
            base.Save(writer);
        }

        public override void Load(BinaryReader reader)
        {
            this.DroppedItem = reader.ReadString();
            this.DisableSpawner = reader.ReadBoolean();
            this.TreasureClass = reader.ReadInt32();
            this.Elixir = reader.ReadBoolean();
            this.BlockedKey = reader.ReadString();
            this.IsMagicallyHidden = reader.ReadBoolean();
            base.Load(reader);
        }
    }
}
