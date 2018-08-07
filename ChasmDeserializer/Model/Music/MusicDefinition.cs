using System;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Music
{
    public class MusicDefinition
    {
        public string DefaultSong;
        public bool UseFade;
        public float FadeInTime;
        public float FadeOutTime;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.DefaultSong);
            writer.Write(this.UseFade);
            writer.Write(this.FadeInTime);
            writer.Write(this.FadeOutTime);
        }

        public void Load(BinaryReader reader)
        {
            this.DefaultSong = reader.ReadString();
            this.UseFade = reader.ReadBoolean();
            this.FadeInTime = reader.ReadSingle();
            this.FadeOutTime = reader.ReadSingle();
        }


    }
}
