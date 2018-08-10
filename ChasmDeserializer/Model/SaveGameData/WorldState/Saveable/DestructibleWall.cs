using ChasmDeserializer.Interfaces;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class DestructibleWall : GenericProp, ISaveGame
    {
        public int MaxHealth { get; set; }

        public void SaveGame(BinaryWriter writer)
        {
            writer.Write(this.SortIndex);
            this.Save(writer);
        }

        public void LoadGame(BinaryReader reader)
        {
            this.SortIndex = reader.ReadInt32();
            this.Load(reader);
        }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(this.MaxHealth);
            base.Save(writer);
        }
        public override void Load(BinaryReader reader)
        {
            this.MaxHealth = reader.ReadInt32();
            base.Load(reader);
        }
    }
}
