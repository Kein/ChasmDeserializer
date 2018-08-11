using System.IO;
using ChasmDeserializer.Extensions;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model.FormattedText
{
    public class CustomStoreInfo
    {
        public string FontPath;
        public string AtlasKey;
        public Vector2 Position;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.FontPath.NullCheck());
            writer.Write(this.AtlasKey.NullCheck());
            writer.Write(this.Position);
        }
        public static CustomStoreInfo Load(BinaryReader reader)
        {
            return new CustomStoreInfo
            {
                FontPath = reader.ReadString(),
                AtlasKey = reader.ReadString(),
                Position = reader.ReadVector2()
            };
        }
    }
}
