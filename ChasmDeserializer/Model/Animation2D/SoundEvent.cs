using System.IO;

namespace ChasmDeserializer.Model
{
    public struct SoundEvent
    {
        public string Name;
        public float PitchMax;
        public float PitchMin;
        public float Volume;

        public void Load(BinaryReader reader)
        {
            this.Name = reader.ReadString();
            this.PitchMax = reader.ReadSingle();
            this.PitchMin = reader.ReadSingle();
            this.Volume = reader.ReadSingle();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.PitchMax);
            writer.Write(this.PitchMin);
            writer.Write(this.Volume);
        }

    }
}
