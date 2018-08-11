using System.IO;
using ChasmDeserializer.Extensions;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model
{
    public class CurrentRoomData
    {
        public int Id;
        public LinkDefinition LeftDestId;
        public LinkDefinition RightDestId;

        public void Load(BinaryReader reader)
        {
            this.Id = reader.ReadInt32();
            this.LeftDestId = default(LinkDefinition);
            this.LeftDestId.Load(reader);
            this.RightDestId = default(LinkDefinition);
            this.RightDestId.Load(reader);
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Id);
            this.LeftDestId.Save(writer);
            this.RightDestId.Save(writer);
        }

    }
    public struct LinkDefinition
    {
        public int DestinationRoomId;
        public int DestinationDoorId;
        public Vector2 SpawnPoint;
        public Vector2 Vector;

        public void Load(BinaryReader reader)
        {
            this.DestinationRoomId = reader.ReadInt32();
            this.DestinationDoorId = reader.ReadInt32();
            this.SpawnPoint = reader.ReadVector2();
            this.Vector = reader.ReadVector2();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.DestinationRoomId);
            writer.Write(this.DestinationDoorId);
            writer.Write(this.SpawnPoint);
            writer.Write(this.Vector);
        }
    }
}
