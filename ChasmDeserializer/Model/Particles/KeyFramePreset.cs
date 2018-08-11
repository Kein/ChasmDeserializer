using ChasmDeserializer.Extensions;
using System;
using System.IO;

namespace ChasmDeserializer.Model.Particles
{
    public class KeyFramePreset
    {
        public KeyFrameType KeyFrameType { get; set; }
        public string Path { get; set; }
        public float Time { get; set; }
        public float Volume { get; set; }
        public float Pitch { get; set; }

        public override string ToString()
        {
            if (this.KeyFrameType == KeyFrameType.Sound)
            {
                return string.Format("Time:{0} {1}", this.Time, this.Path);
            }
            return base.ToString();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write((int)this.KeyFrameType);
            writer.Write(this.Path.NullCheck());
            writer.Write(this.Time);
            writer.Write(this.Volume);
            writer.Write(this.Pitch);
        }

        public void Load(BinaryReader reader)
        {
            this.KeyFrameType = (KeyFrameType)reader.ReadInt32();
            this.Path = reader.ReadString();
            this.Time = reader.ReadSingle();
            this.Volume = reader.ReadSingle();
            this.Pitch = reader.ReadSingle();
        }
    }
}
