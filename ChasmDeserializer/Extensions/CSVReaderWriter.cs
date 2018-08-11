using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace ChasmDeserializer.Extensions
{
    public static class CSVReaderWriter
    {

        public static string WriteCSV(CSVData data)
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Join(",", data.headers));
            foreach (var list in data.values)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var s = list[i];
                    if (s.IndexOf('"') > -1)
                    {
                        list[i] = String.Concat("\"", s.Replace("\"", "\"\""), "\"");
                        continue;
                    }
                    if (s.IndexOf(',') > -1)
                    {
                        list[i] = String.Concat("\"", s, "\"");
                    }
                }
                builder.AppendLine(string.Join(",", list));
            }
            return builder.ToString();
        }

        public static CSVData ReadCSV(string filePath)
        {
            var data = new CSVData();
            data.values = new List<List<string>>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                ushort c = 0;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (c == 0)
                        data.headers = new List<string>(fields);
                    else
                        data.values.Add(new List<string>(fields));
                    c++;
                }
            }
            return data;
        }
    }
}
