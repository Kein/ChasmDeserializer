using ChasmDeserializer.Extensions;
using ChasmDeserializer.Interfaces;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class ComboLockHint : GenericProp, ISaveGame
    {
        public void LoadGame(BinaryReader reader)
        {
            this.Position = reader.ReadVector2();
            base.LayerName = reader.ReadString();
        }

        public void SaveGame(BinaryWriter writer)
        {
            writer.Write(this.Position);
            writer.Write(base.LayerName);
        }


    }
}
