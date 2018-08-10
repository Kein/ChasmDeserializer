using Microsoft.Xna.Framework;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class GenericProp : Trigger
    {
        public virtual bool IsUsable { get; set; }
        public virtual bool IsAlive { get; set; }
        public virtual bool IsScripted { get; set; }
        public virtual bool IsCollidable { get; set; }
        public virtual bool IsOneWay { get; set; }
        public virtual bool FlipTexture { get; set; }
        public virtual bool FlipTextureVertically { get; set; }
        public virtual bool IgnoreBody { get; set; }
        public virtual bool IsDestructible { get; set; }
        public virtual string AtlasKey { get; set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle CollisionBox { get; set; }
        public virtual  float Rotation { get; set; }
        public Vector2 Origin;
        public virtual  Vector2 Position { get; set; }
        public string LayerName { get; set; }
        public virtual int SortIndex { get; set; }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(this.IsUsable);
            writer.Write(this.IsAlive);
            writer.Write(this.IsScripted);
            writer.Write(this.IsCollidable);
            writer.Write(this.IsOneWay);
            writer.Write(this.FlipTexture);
            writer.Write(this.FlipTextureVertically);
            writer.Write(this.IgnoreBody);
            writer.Write(this.IsDestructible);
            if (this.AtlasKey == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.Write(this.AtlasKey);
            }
            writer.Write(this.SourceRect);
            writer.Write(this.CollisionBox);
            writer.Write(this.Rotation);
            writer.Write(this.Origin);
            writer.Write(this.Position);
            writer.Write(this.LayerName.NullCheck());
            base.Save(writer);
        }

        public override void Load(BinaryReader reader)
        {
            this.IsUsable = reader.ReadBoolean();
            this.IsAlive = reader.ReadBoolean();
            this.IsScripted = reader.ReadBoolean();
            this.IsCollidable = reader.ReadBoolean();
            this.IsOneWay = reader.ReadBoolean();
            this.FlipTexture = reader.ReadBoolean();
            this.FlipTextureVertically = reader.ReadBoolean();
            this.IgnoreBody = reader.ReadBoolean();
            this.IsDestructible = reader.ReadBoolean();
            string text = reader.ReadString();
            if (text != "null")
            {
                this.AtlasKey = text;
            }
            this.SourceRect = reader.ReadRectangle();
            this.CollisionBox = reader.ReadRectangle();
            this.Rotation = reader.ReadSingle();
            this.Origin = reader.ReadVector2();
            this.Position = reader.ReadVector2();
            this.LayerName = reader.ReadString();
            base.Load(reader);
        }
    }
}
