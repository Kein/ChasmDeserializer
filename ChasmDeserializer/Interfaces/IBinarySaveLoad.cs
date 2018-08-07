using System.IO;

namespace ChasmDeserializer
{
    public interface IBinarySaveLoad
    {
        void Load(BinaryReader read);
        void Save(BinaryWriter writer);
    }
}
