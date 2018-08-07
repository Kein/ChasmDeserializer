using System;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public class DungeonDefinition
    {
        public string DungeonName { get; set; }
        public float Difficulty { get; set; }
        public bool NoSpecialDrops { get; set; }
        public List<TagDefinition> Tags { get; set; }
        public List<TagDefinition> RandomTags { get; set; }
        public List<TagDefinition> EndCapTags { get; set; }
        public List<PathCheckDefinition> PathChecks { get; set; }
        public List<DungeonFlags> Flags { get; set; }

        public void Load(BinaryReader reader)
        {
            this.DungeonName = reader.ReadString();
            this.Difficulty = reader.ReadSingle();
            this.NoSpecialDrops = reader.ReadBoolean();
            this.Tags = new List<TagDefinition>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                TagDefinition tagDefinition = new TagDefinition();
                tagDefinition.Load(reader);
                this.Tags.Add(tagDefinition);
            }
            count = reader.ReadInt32();
            this.RandomTags = new List<TagDefinition>();
            for (int j = 0; j < count; j++)
            {
                TagDefinition tagDefinition2 = new TagDefinition();
                tagDefinition2.Load(reader);
                this.RandomTags.Add(tagDefinition2);
            }
            count = reader.ReadInt32();
            this.EndCapTags = new List<TagDefinition>();
            for (int k = 0; k < count; k++)
            {
                TagDefinition tagDefinition3 = new TagDefinition();
                tagDefinition3.Load(reader);
                this.EndCapTags.Add(tagDefinition3);
            }
            count = reader.ReadInt32();
            this.PathChecks = new List<PathCheckDefinition>();
            for (int l = 0; l < count; l++)
            {
                PathCheckDefinition item = default(PathCheckDefinition);
                item.Load(reader);
                this.PathChecks.Add(item);
            }
            count = reader.ReadInt32();
            this.Flags = new List<DungeonFlags>();
            for (int m = 0; m < count; m++)
            {
                this.Flags.Add((DungeonFlags)reader.ReadInt32());
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.DungeonName);
            writer.Write(this.Difficulty);
            writer.Write(this.NoSpecialDrops);
            if (this.Tags != null)
            {
                writer.Write(this.Tags.Count);
                foreach (var tagDef in Tags)
                {
                    tagDef.Save(writer);
                }
            }
            else
            {
                writer.Write(0);
            }
            if (this.RandomTags != null)
            {
                writer.Write(this.RandomTags.Count);
                foreach (var tagDef in RandomTags)
                {
                    tagDef.Save(writer);
                }
            }
            else
            {
                writer.Write(0);
            }
            if (this.EndCapTags != null)
            {
                writer.Write(this.EndCapTags.Count);
                foreach (var tagDef in EndCapTags)
                {
                    tagDef.Save(writer);
                }
            }
            else
            {
                writer.Write(0);
            }
            if (this.PathChecks != null)
            {
                writer.Write(this.PathChecks.Count);
                foreach (var pathCheck in PathChecks)
                {
                    pathCheck.Save(writer);
                }
            }
            else
            {
                writer.Write(0);
            }
            if (this.Flags != null)
            {
                writer.Write(this.Flags.Count);
                foreach (var dungeonFlag in Flags)
                {
                    writer.Write((int)dungeonFlag);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

    }
}
