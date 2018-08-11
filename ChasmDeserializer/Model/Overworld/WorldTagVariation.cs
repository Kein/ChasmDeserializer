using ChasmDeserializer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public class WorldTagVariation
    {
        public string Name { get; set; }
        public List<int> DungeonIds { get; set; }
        public List<string> Tags { get; set; }
        public int MaxRooms { get; set; }

        public WorldTagVariation() { }

        public void Load(BinaryReader reader)
        {
            this.Name = reader.ReadString();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                this.DungeonIds.Add(reader.ReadInt32());
            }
            int num2 = reader.ReadInt32();
            for (int j = 0; j < num2; j++)
            {
                this.Tags.Add(reader.ReadString());
            }
            this.MaxRooms = reader.ReadInt32();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Name.NullCheck());
            writer.Write(this.DungeonIds.Count);
            foreach (int value in this.DungeonIds)
            {
                writer.Write(value);
            }
            writer.Write(this.Tags.Count);
            foreach (string s in this.Tags)
            {
                writer.Write(s.NullCheck());
            }
            writer.Write(this.MaxRooms);
        }
    }
}
