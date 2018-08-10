using ChasmDeserializer.Interfaces;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class SaveableProp : GenericProp, ISaveGame
    {
        public void SaveGame(BinaryWriter writer)
        {
            Save(writer);
        }

        public void LoadGame(BinaryReader reader)
        {
            Load(reader);
        }
    }
}
