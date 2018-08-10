using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer
{
    public static class Extensions
    {
        // Whatever man
        public static string NullCheck(this string s) => s = s ?? string.Empty;
        public static Vector2 ToVector2(this Point p) => new Vector2((float)p.X, (float)p.Y);


        // XNA Point struct
        public static void Write(this BinaryWriter writer, Point rect)
        {
            writer.Write(rect.X);
            writer.Write(rect.Y);
        }
        public static Point ReadPoint(this BinaryReader reader)
        {
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            return new Point(x, y);
        }

        // XNA Rectangle struct
        public static Rectangle ReadRectangle(this BinaryReader reader)
        {
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            return new Rectangle(x, y, width, height);
        }
        public static void Write(this BinaryWriter writer, Rectangle rect)
        {
            writer.Write(rect.X);
            writer.Write(rect.Y);
            writer.Write(rect.Width);
            writer.Write(rect.Height);
        }

        // XNA Vector 2 struct
        public static void Write(this BinaryWriter writer, Vector2 vec)
        {
            writer.Write(vec.X);
            writer.Write(vec.Y);
        }
        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            return new Vector2(x, y);
        }

        // XNA Color struct
        public static void Write(this BinaryWriter writer, Color c)
        {
            writer.Write(c.R);
            writer.Write(c.G);
            writer.Write(c.B);
            writer.Write(c.A);
        }

        public static Color ReadColor(this BinaryReader reader)
        {
            int r = (int)reader.ReadByte();
            int g = (int)reader.ReadByte();
            int b = (int)reader.ReadByte();
            int alpha = (int)reader.ReadByte();
            return new Color(r, g, b, alpha);
        }

        // List<string> reader/writer helpers
        public static void Write(this BinaryWriter writer, List<string> c)
        {
            writer.Write(c.Count);
            foreach (string value in c)
                writer.Write(value);
        }
        public static List<string> ReadStrings(this BinaryReader reader)
        {
            List<string> list = new List<string>();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
                list.Add(reader.ReadString());
            return list;
        }

        // Curve writer/Reader from XNA

        public static void Write(this BinaryWriter writer, Curve c)
        {
            writer.Write(c.Keys.Count);
            foreach (CurveKey curveKey in c.Keys)
            {
                writer.Write(curveKey.Value);
                writer.Write(curveKey.Position);
                writer.Write(curveKey.TangentIn);
                writer.Write(curveKey.TangentOut);
            }
        }

        public static Curve ReadCurve(this BinaryReader reader)
        {
            Curve curve = new Curve();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                float value = reader.ReadSingle();
                float position = reader.ReadSingle();
                float tangentIn = reader.ReadSingle();
                float tangentOut = reader.ReadSingle();
                CurveKey item = new CurveKey(position, value, tangentIn, tangentOut);
                curve.Keys.Add(item);
            }
            return curve;
        }

        // Used for WorldSaveState
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] array = new byte[4096];
                int count;
                while ((count = reader.Read(array, 0, array.Length)) != 0)
                {
                    memoryStream.Write(array, 0, count);
                }
                result = memoryStream.ToArray();
            }
            return result;
        }
    }
}
