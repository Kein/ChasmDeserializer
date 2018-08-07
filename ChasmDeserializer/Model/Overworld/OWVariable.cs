using System;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public class OWVariable
    {
        public string Variable { get; set; }
        public string Value { get; set; }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Variable);
            writer.Write(this.Value);
        }

        public void Load(BinaryReader reader)
        {
            this.Variable = reader.ReadString();
            this.Value = reader.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0}={1}", this.Variable, this.Value);
        }
    }
}
