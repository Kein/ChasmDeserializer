using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model
{
    public class TextureInfoList : IBinarySaveLoad
    {
        public long DateModified;
        public List<TextureInfo> Textures;

        public void Load(BinaryReader reader)
        {
            this.DateModified = reader.ReadInt64();
            int count = reader.ReadInt32();
            this.Textures = new List<TextureInfo>(count);
            for (int i = 0; i < count; i++)
            {
                TextureInfo item = default(TextureInfo);
                item.Load(reader);
                this.Textures.Add(item);
            }
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.DateModified);
            writer.Write(this.Textures.Count);
            for (int i = 0; i < this.Textures.Count; i++)
            {
                this.Textures[i].Save(writer);
            }
        }
    }
}
