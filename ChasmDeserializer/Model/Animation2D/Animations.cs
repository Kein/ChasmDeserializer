using ChasmDeserializer.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Animation2D
{
    class Animations : IBinarySaveLoad
    {
        public List<Animation2DInfo> list;
        public void Load(BinaryReader read)
        {
            int c = read.ReadInt32();
            list = new List<Animation2DInfo>(c);
            for (int i = 0; i < c; i++)
            {
                Animation2DInfo animation2DInfo = new Animation2DInfo();
                animation2DInfo.Load(read);
                list.Add(animation2DInfo);
            }
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(list.Count);
            foreach (var entry in list)
                entry.Save(writer);
        }
    }
}
