using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.FormattedText
{
    public class StringLine
    {
        public List<StringInline> Inlines;

        public static StringLine Load(BinaryReader reader)
        {
            StringLine stringLine = new StringLine();
            int count = reader.ReadInt32();
            stringLine.Inlines = new List<StringInline>(count);
            for (int i = 0; i < count; i++)
                stringLine.Inlines.Add(StringInline.Load(reader));
            return stringLine;
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Inlines.Count);
            foreach (StringInline stringInline in this.Inlines)
                stringInline.Save(writer);
        }

    }
}
