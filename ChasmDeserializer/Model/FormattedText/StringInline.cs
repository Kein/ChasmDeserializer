using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model.FormattedText
{
    public class StringInline
    {
        public Color Color;
        public int Offset;
        public string Tag;
        public string Text;

        public static StringInline Load(BinaryReader reader)
        {
            return new StringInline
            {
                Color = reader.ReadColor(),
                Offset = reader.ReadInt32(),
                Tag = reader.ReadString(),
                Text = reader.ReadString()
            };
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Color);
            writer.Write(this.Offset);
            writer.Write(this.Tag.NullCheck());
            writer.Write(this.Text.NullCheck());
        }
    }
}
