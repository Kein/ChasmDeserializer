using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model
{
    public class Polygon
    {
        public List<Vector2> Points;
  
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Points.Count);
            foreach (Vector2 vec in this.Points)
                writer.Write(vec);
        }
        public void Load(BinaryReader reader)
        {
            int num = reader.ReadInt32();
            this.Points = new List<Vector2>(num);
            for (int i = 0; i < num; i++)
            {
                Vector2 item = reader.ReadVector2();
                this.Points.Add(item);
            }
        }
    }
}
