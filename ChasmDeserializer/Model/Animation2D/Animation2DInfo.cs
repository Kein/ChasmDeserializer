using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model
{
    public class Animation2DInfo : IBinarySaveLoad
    {
        public Dictionary<int, List<Hitbox>> Hitboxes;
        public Dictionary<int, PolygonList> Polygones;
        public Dictionary<string, Vector2> ReferencePoints;
        public Dictionary<int, SoundEvent> Sounds;
        public int ActionFrame;
        public string AtlasKey;
        public float DelayedStart;
        public Point FlipOffset;
        public int FPS;
        public Point FrameSize;
        public bool IsLoop;
        public string Name;
        public Vector2 Origin;
        public string Path;
        public int TotalFrames;
        public int TotalRows;
        public float LoopMaxElapsed;
        public bool RandomFrameStart;

        public void Load(BinaryReader reader)
        {
            this.Name = reader.ReadString();
            this.Path = reader.ReadString();
            this.AtlasKey = reader.ReadString();
            this.TotalFrames = reader.ReadInt32();
            this.TotalRows = reader.ReadInt32();
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            this.FrameSize = new Point(x, y);
            this.FPS = reader.ReadInt32();
            this.IsLoop = reader.ReadBoolean();
            float x2 = reader.ReadSingle();
            float y2 = reader.ReadSingle();
            this.Origin = new Vector2(x2, y2);
            x = reader.ReadInt32();
            y = reader.ReadInt32();
            this.FlipOffset = new Point(x, y);
            this.DelayedStart = reader.ReadSingle();
            int count = reader.ReadInt32();
            this.Hitboxes = new Dictionary<int, List<Hitbox>>(count);
            for (int i = 0; i < count; i++)
            {
                int key = reader.ReadInt32();
                int count2 = reader.ReadInt32();
                List<Hitbox> list = new List<Hitbox>(count2);
                this.Hitboxes.Add(key, list);
                for (int j = 0; j < count2; j++)
                {
                    Hitbox item = default(Hitbox);
                    item.Load(reader);
                    list.Add(item);
                }
            }
            count = reader.ReadInt32();
            this.Sounds = new Dictionary<int, SoundEvent>(count);
            for (int k = 0; k < count; k++)
            {
                int key = reader.ReadInt32();
                SoundEvent value = default(SoundEvent);
                value.Load(reader);
                this.Sounds.Add(key, value);
            }
            count = reader.ReadInt32();
            this.Polygones = new Dictionary<int, PolygonList>(count);
            for (int l = 0; l < count; l++)
            {
                int key = reader.ReadInt32();
                PolygonList polygonList = new PolygonList();
                polygonList.Load(reader);
                this.Polygones.Add(key, polygonList);
            }
            this.ActionFrame = reader.ReadInt32();
            this.LoopMaxElapsed = reader.ReadSingle();
            count = reader.ReadInt32();
            this.ReferencePoints = new Dictionary<string, Vector2>(count);
            for (int m = 0; m < count; m++)
            {
                string key = reader.ReadString();
                Vector2 value = reader.ReadVector2();
                this.ReferencePoints.Add(key, value);
            }
            this.RandomFrameStart = reader.ReadBoolean();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.Path);
            writer.Write(this.AtlasKey);
            writer.Write(this.TotalFrames);
            writer.Write(this.TotalRows);
            writer.Write(this.FrameSize.X);
            writer.Write(this.FrameSize.Y);
            writer.Write(this.FPS);
            writer.Write(this.IsLoop);
            writer.Write(this.Origin.X);
            writer.Write(this.Origin.Y);
            writer.Write(this.FlipOffset.X);
            writer.Write(this.FlipOffset.Y);
            writer.Write(this.DelayedStart);
            if (this.Hitboxes == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(this.Hitboxes.Keys.Count);
                foreach (int num in this.Hitboxes.Keys)
                {
                    writer.Write(num);
                    writer.Write(this.Hitboxes[num].Count);
                    foreach (Hitbox hitbox in this.Hitboxes[num])
                    {
                        hitbox.Save(writer);
                    }
                }
            }
            if (this.Sounds == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(this.Sounds.Keys.Count);
                foreach (int num2 in this.Sounds.Keys)
                {
                    writer.Write(num2);
                    this.Sounds[num2].Save(writer);
                }
            }
            if (this.Polygones == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(this.Polygones.Keys.Count);
                foreach (int num3 in this.Polygones.Keys)
                {
                    writer.Write(num3);
                    this.Polygones[num3].Save(writer);
                }
            }
            writer.Write(this.ActionFrame);
            writer.Write(this.LoopMaxElapsed);
            if (this.ReferencePoints == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(this.ReferencePoints.Keys.Count);
                foreach (string text in this.ReferencePoints.Keys)
                {
                    writer.Write(text);
                    writer.Write(this.ReferencePoints[text]);
                }
            }
            writer.Write(this.RandomFrameStart);
        }
    }
}