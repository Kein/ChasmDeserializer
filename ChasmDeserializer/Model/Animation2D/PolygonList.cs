using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model
{
    public class PolygonList : List<Polygon>
    {
        public void Load(BinaryReader reader)
        {
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                Polygon polygon = new Polygon();
                polygon.Load(reader);
                base.Add(polygon);
            }
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(base.Count);
            foreach (Polygon polygon in this)
                polygon.Save(writer);
        }
    }
}
