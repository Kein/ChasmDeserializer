using System.IO;

namespace ChasmDeserializer.Interfaces
{
    public interface ISaveGame
    {
        void SaveGame(BinaryWriter writer);
        void LoadGame(BinaryReader reader);
    }
}
