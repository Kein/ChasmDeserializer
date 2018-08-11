using ChasmDeserializer.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace ChasmDeserializer.Model
{
    public struct TextureInfo
    {
        public string Name;
        public Rectangle SrcRect;
        public int SheetNum;
        public string Key;

        public void Load(BinaryReader reader)
        {
            this.Name = reader.ReadString();
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            this.SrcRect = new Rectangle(x, y, width, height);
            this.SheetNum = reader.ReadInt32();
            this.Key = reader.ReadString();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.SrcRect.X);
            writer.Write(this.SrcRect.Y);
            writer.Write(this.SrcRect.Width);
            writer.Write(this.SrcRect.Height);
            writer.Write(this.SheetNum);
            writer.Write(this.Key.NullCheck());
        }
    }
}
