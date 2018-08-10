using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Interfaces;

namespace ChasmDeserializer.Model.RoomManager
{
    public class RoomManager : IBinarySaveLoad
    {
        public Dictionary<string, HashSet<string>> RoomTags;

        public void Load(BinaryReader reader)
        {
            int count = reader.ReadInt32();
            this.RoomTags = new Dictionary<string, HashSet<string>>(count);
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                int count2 = reader.ReadInt32();
                var hashSet = new HashSet<string>();
                this.RoomTags.Add(key, hashSet);
                for (int j = 0; j < count2; j++)
                    hashSet.Add(reader.ReadString());
            }
        }

        public void Save(BinaryWriter writer)
        {
            if (this.RoomTags == null)
            {
                writer.Write(0);
                return;
            }
            writer.Write(this.RoomTags.Count);
            foreach (var keyValue in RoomTags)
            {
                writer.Write(keyValue.Key);
                writer.Write(keyValue.Value.Count);
                foreach (var item in keyValue.Value)
                    writer.Write(item);
            }
        }
    }
}
