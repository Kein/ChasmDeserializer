using System;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public struct PathCheckDefinition
    {
        public int RoomInstanceA;
        public int RoomInstanceB;
        public int MinLength;

        public override string ToString()
        {
            return string.Format("RA:{0} RB:{1} Len:{2}", this.RoomInstanceA, this.RoomInstanceB, this.MinLength);
        }

        public void Load(BinaryReader reader)
        {
            this.RoomInstanceA = reader.ReadInt32();
            this.RoomInstanceB = reader.ReadInt32();
            this.MinLength = reader.ReadInt32();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.RoomInstanceA);
            writer.Write(this.RoomInstanceB);
            writer.Write(this.MinLength);
        }
    }
}
