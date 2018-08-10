using System;
using System.IO;
using ChasmDeserializer.Interfaces;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class Crate : GenericProp, ISaveGame
    { 
        public CrateDropType DropType;
        public string PreDeterminedDropID;

        public void SaveGame(BinaryWriter writer)
        {
            writer.Write(base.Id);
            writer.Write(this.Position);
            writer.Write((int)this.DropType);
            writer.Write(this.SortIndex);
            PreDeterminedDropID = string.IsNullOrEmpty(PreDeterminedDropID) ? "null" : PreDeterminedDropID;
            writer.Write(PreDeterminedDropID);
        }

        public void LoadGame(BinaryReader reader)
        {
            base.Id = reader.ReadInt32();
            this.Position = reader.ReadVector2();
            this.DropType = (CrateDropType)reader.ReadInt32();
            this.SortIndex = reader.ReadInt32();
            string DropID = reader.ReadString();
            PreDeterminedDropID = String.Equals(DropID, "null", StringComparison.OrdinalIgnoreCase) ? String.Empty : DropID;
        }
    }
}
