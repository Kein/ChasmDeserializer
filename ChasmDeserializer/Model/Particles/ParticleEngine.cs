using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Interfaces;

namespace ChasmDeserializer.Model.Particles
{
    class ParticleEngine : IBinarySaveLoad
    {
        public Dictionary<string, ParticleGroup> ParticleGroups = new Dictionary<string, ParticleGroup>();
        public void Load(BinaryReader reader)
        {
            this.ParticleGroups = new Dictionary<string, ParticleGroup>();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                string key = reader.ReadString();
                ParticleGroup particleGroup = new ParticleGroup();
                particleGroup.Load(reader);
                this.ParticleGroups.Add(key, particleGroup);
            }
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(ParticleGroups.Count);
            foreach (var group in ParticleGroups)
            {
                writer.Write(group.Key);
                group.Value.Save(writer);
            }
        }

    }
}
