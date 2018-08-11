using System.IO;
using ChasmDeserializer.Extensions;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model
{
    public struct Hitbox
    {
        public string Type;
        public Rectangle Rectangle;

        public void Load(BinaryReader reader)
        {
            this.Type = reader.ReadString();
            this.Rectangle = reader.ReadRectangle();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Type);
            writer.Write(this.Rectangle);
        }

    }
}
