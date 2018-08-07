using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Music
{
    public class MusicManager : IBinarySaveLoad
    {
        public Dictionary<string, MusicDefinition> AreaMusic;
        public Dictionary<string, MusicDefinition> RoomMusic;

        public void Load(BinaryReader reader)
        {
            int count = reader.ReadInt32();
            AreaMusic = new Dictionary<string, MusicDefinition>(count);
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                MusicDefinition musicDefinition = new MusicDefinition();
                musicDefinition.Load(reader);
                this.AreaMusic.Add(key, musicDefinition);
            }
            count = reader.ReadInt32();
            RoomMusic = new Dictionary<string, MusicDefinition>(count);
            for (int j = 0; j < count; j++)
            {
                string key = reader.ReadString();
                MusicDefinition musicDefinition = new MusicDefinition();
                musicDefinition.Load(reader);
                this.RoomMusic.Add(key, musicDefinition);
            }
        }

        public void Save(BinaryWriter writer)
        {
            int count = this.AreaMusic.Keys.Count;
            writer.Write(count);
            foreach (string text in this.AreaMusic.Keys)
            {
                writer.Write(text);
                this.AreaMusic[text].Save(writer);
            }
            count = this.RoomMusic.Keys.Count;
            writer.Write(count);
            foreach (string text2 in this.RoomMusic.Keys)
            {
                writer.Write(text2);
                this.RoomMusic[text2].Save(writer);
            }
        }
    }
}
