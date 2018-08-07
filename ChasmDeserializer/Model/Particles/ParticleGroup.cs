using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Particles
{
    public class ParticleGroup
    {
        public List<ParticlePresets> Presets { get; set; }
        public List<KeyFramePreset> KeyFramePresets { get; set; }
        public bool UIElement { get; set; }
        public bool Loop { get; set; }

        public void Load(BinaryReader reader)
        {
            int num = reader.ReadInt32();
            this.Presets = new List<ParticlePresets>();
            for (int i = 0; i < num; i++)
            {
                ParticlePresets particlePresets = new ParticlePresets();
                particlePresets.Load(reader);
                this.Presets.Add(particlePresets);
            }
            int num2 = reader.ReadInt32();
            this.KeyFramePresets = new List<KeyFramePreset>();
            for (int j = 0; j < num2; j++)
            {
                KeyFramePreset keyFramePreset = new KeyFramePreset();
                keyFramePreset.Load(reader);
                this.KeyFramePresets.Add(keyFramePreset);
            }
            this.UIElement = reader.ReadBoolean();
            this.Loop = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Presets.Count);
            foreach (ParticlePresets particlePresets in this.Presets)
            {
                particlePresets.Save(writer);
            }
            writer.Write(this.KeyFramePresets.Count);
            foreach (KeyFramePreset keyFramePreset in this.KeyFramePresets)
            {
                keyFramePreset.Save(writer);
            }
            writer.Write(this.UIElement);
            writer.Write(this.Loop);
        }
    }
}
