using System;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public class TagDefinition
    {
        public string Tag { get; set; }
        public TagCondition Condition { get; set; }
        public int Chance { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public int Length { get; set; }

        public override string ToString()
        {
            string text = "";
            if (this.Length > 0)
            {
                text = string.Format(" L:{0}", this.Length);
            }
            if (this.Min != 0 || this.Max != 0)
            {
                return string.Format("[{1}-{2}]{0}{3}", new object[]
                {
                    this.Tag,
                    this.Min,
                    this.Max,
                    text
                });
            }
            switch (this.Condition)
            {
                case TagCondition.MustHave:
                    return string.Format("== {0}{1}", this.Tag, text);
                case TagCondition.MustNotHave:
                    return string.Format("!= {0}{1}", this.Tag, text);
                case TagCondition.Chance:
                    return string.Format("% {0}{1}", this.Tag, text);
                default:
                    return this.Tag;
            }
        }

        public void Load(BinaryReader reader)
        {
            this.Tag = reader.ReadString();
            this.Condition = (TagCondition)Enum.Parse(typeof(TagCondition), reader.ReadString());
            this.Chance = reader.ReadInt32();
            this.Min = reader.ReadInt32();
            this.Max = reader.ReadInt32();
            this.Length = reader.ReadInt32();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Tag);
            writer.Write(this.Condition.ToString());
            writer.Write(this.Chance);
            writer.Write(this.Min);
            writer.Write(this.Max);
            writer.Write(this.Length);
        }
    }
}
