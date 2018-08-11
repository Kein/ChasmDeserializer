using ChasmDeserializer.Extensions;
using ChasmDeserializer.Interfaces;
using Microsoft.Xna.Framework;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class Trigger : IBinarySaveLoad
    {
        public virtual bool TriggerEnabled { get; set; }
        public virtual bool EnemyActivated { get; set; }
        public int Id { get; set; }
        public virtual Rectangle Bounds { get; set; }

        public virtual void Load(BinaryReader reader)
        {
            this.TriggerEnabled = reader.ReadBoolean();
            this.EnemyActivated = reader.ReadBoolean();
            this.Id = reader.ReadInt32();
            this.Bounds = reader.ReadRectangle();
        }
        public virtual void Save(BinaryWriter writer)
        {
            writer.Write(this.TriggerEnabled);
            writer.Write(this.EnemyActivated);
            writer.Write(this.Id);
            writer.Write(this.Bounds);
        }
    }
}
