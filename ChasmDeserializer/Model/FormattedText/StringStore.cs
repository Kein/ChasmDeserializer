using ChasmDeserializer.Extensions;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.FormattedText
{
    public class StringStore
    {
        public CustomStoreInfo CustomInfo;
        public int Height;
        public int Width;
        public StringStoreType StoreType;
        public NoteStyle Style;
        public string Category;
        public List<StringLine> StringLines;

        public static StringStore Load(BinaryReader reader)
        {
            StringStore stringStore = new StringStore();
            stringStore.CustomInfo = CustomStoreInfo.Load(reader);
            stringStore.Height = reader.ReadInt32();
            stringStore.Width = reader.ReadInt32();
            stringStore.StoreType = (StringStoreType)reader.ReadInt32();
            stringStore.Style = (NoteStyle)reader.ReadInt32();
            stringStore.Category = reader.ReadString();
            int count = reader.ReadInt32();
            stringStore.StringLines = new List<StringLine>(count);
            for (int i = 0; i < count; i++)
                stringStore.StringLines.Add(StringLine.Load(reader));
            return stringStore;
        }

        public void Save(BinaryWriter writer)
        {
            this.CustomInfo.Save(writer);
            writer.Write(this.Height);
            writer.Write(this.Width);
            writer.Write((int)this.StoreType);
            writer.Write((int)this.Style);
            writer.Write(this.Category.NullCheck());
            writer.Write(this.StringLines.Count);
            foreach (StringLine stringLine in this.StringLines)
                stringLine.Save(writer);
        }

    }
}
