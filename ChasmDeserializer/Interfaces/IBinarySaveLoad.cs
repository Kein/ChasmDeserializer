using System.IO;

namespace ChasmDeserializer.Interfaces
{
    public interface IBinarySaveLoad
    {
        void Load(BinaryReader read);
        void Save(BinaryWriter writer);
    }
}
