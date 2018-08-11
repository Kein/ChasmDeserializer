using ChasmDeserializer.Extensions;
using ChasmDeserializer.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer
{
    public class CSVData : IBinarySaveLoad
    {
        public List<string> headers;
        public List<List<string>> values;

         public void Load(BinaryReader reader)
        {
            int count = reader.ReadInt32();
            this.headers = new List<string>(count);
            for (int i = 0; i < count; i++)
                this.headers.Add(reader.ReadString());
            count = reader.ReadInt32();
            this.values = new List<List<string>>(count);
            for (int j = 0; j < count; j++)
            {
                int count2 = reader.ReadInt32();
                List<string> list = new List<string>(count2);
                for (int k = 0; k < count2; k++)
                    list.Add(reader.ReadString());
                this.values.Add(list);
            }
        }
        public void Save(BinaryWriter writer)
        {
            headers = headers ?? new List<string>(0);
            writer.Write(headers.Count);
                foreach (string s in headers)
                    writer.Write(s.NullCheck());
            values = values ?? new List<List<string>>(0);
            writer.Write(values.Count);
            foreach (List<string> list in values)
            {
                writer.Write(list.Count);
                foreach (string s2 in list)
                    writer.Write(s2.NullCheck());
            }
        }
    }
}
