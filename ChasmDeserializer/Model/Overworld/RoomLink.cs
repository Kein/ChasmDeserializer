using System;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    public class RoomLink
    {
        public int Id;
        public RoomLinkType LinkType { get; set; }
        public LinkDirection LinkDirection { get; set; }
        public int DestinationRoomId;
        public int DestinationLinkId ;

        public void Load(BinaryReader reader)
        {
            this.Id = reader.ReadInt32();
            this.LinkType = (RoomLinkType)Enum.Parse(typeof(RoomLinkType), reader.ReadString());
            this.LinkDirection = (LinkDirection)Enum.Parse(typeof(LinkDirection), reader.ReadString());
            this.DestinationRoomId = reader.ReadInt32();
            this.DestinationLinkId = reader.ReadInt32();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Id);
            writer.Write(this.LinkType.ToString());
            writer.Write(this.LinkDirection.ToString());
            writer.Write(this.DestinationRoomId);
            writer.Write(this.DestinationLinkId);
        }


    }
}
