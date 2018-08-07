using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model.Overworld
{
    public class RoomDefinition : IBinarySaveLoad
    {
        public int Id { get; set; }
        public RoomDefinitionType RoomDefinitionType { get; set; }
        public int RoomTemplateId { get; set; }
        public string RoomName { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public Point EditorLocation { get; set; }
        public List<RoomLink> LeftLinks { get; set; }
        public List<RoomLink> RightLinks { get; set; }
        public List<RoomLink> CustomLinks { get; set; }
        public DungeonDefinition DungeonDefinition { get; set; }
        public bool IsStartingRoom { get; set; }
        public int? DungeonId { get; set; }
        public string Area { get; set; }
        public bool IsEndCap { get; set; }
        public int Length { get; set; }
        public bool IsHub { get; set; }


        public void Load(BinaryReader reader)
        {
            this.Id = reader.ReadInt32();
            string roomType = reader.ReadString();
            this.RoomDefinitionType = (RoomDefinitionType)Enum.Parse(typeof(RoomDefinitionType), roomType);
            this.RoomTemplateId = reader.ReadInt32();
            this.RoomName = reader.ReadString();
            this.IsLocked = reader.ReadBoolean();
            this.IsDisabled = reader.ReadBoolean();
            this.EditorLocation = reader.ReadPoint();
            int count = reader.ReadInt32();
            this.LeftLinks = new List<RoomLink>();
            for (int i = 0; i < count; i++)
            {
                RoomLink roomLink = new RoomLink();
                roomLink.Load(reader);
                this.LeftLinks.Add(roomLink);
            }
            count = reader.ReadInt32();
            this.RightLinks = new List<RoomLink>();
            for (int j = 0; j < count; j++)
            {
                RoomLink roomLink2 = new RoomLink();
                roomLink2.Load(reader);
                this.RightLinks.Add(roomLink2);
            }
            count = reader.ReadInt32();
            this.CustomLinks = new List<RoomLink>();
            for (int k = 0; k < count; k++)
            {
                RoomLink roomLink3 = new RoomLink();
                roomLink3.Load(reader);
                this.CustomLinks.Add(roomLink3);
            }
            bool flag = reader.ReadBoolean();
            if (flag)
            {
                this.DungeonDefinition = new DungeonDefinition();
                this.DungeonDefinition.Load(reader);
            }
            this.IsStartingRoom = reader.ReadBoolean();
            this.DungeonId = new int?(reader.ReadInt32());
            this.Area = reader.ReadString();
            this.IsEndCap = reader.ReadBoolean();
            this.Length = reader.ReadInt32();
            this.IsHub = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Id);
            writer.Write(RoomDefinitionType.ToString());
            writer.Write(this.RoomTemplateId);
            writer.Write(this.RoomName.NullCheck());
            writer.Write(this.IsLocked);
            writer.Write(this.IsDisabled);
            writer.Write(this.EditorLocation);
            if (this.LeftLinks != null)
            {
                writer.Write(this.LeftLinks.Count);
                foreach (var link in LeftLinks)
                {
                    link.Save(writer);
                }
            }
            else { writer.Write(0); }
            if (this.RightLinks != null)
            {
                writer.Write(this.RightLinks.Count);
                foreach (var link in RightLinks)
                {
                    link.Save(writer);
                }
            }
            else { writer.Write(0); }
            if (this.CustomLinks != null)
            {
                writer.Write(this.CustomLinks.Count);
                foreach (var link in CustomLinks)
                {
                    link.Save(writer);
                }
            }
            else { writer.Write(0); }
            if (this.DungeonDefinition != null)
            {
                writer.Write(true);
                this.DungeonDefinition.Save(writer);
            }
            else
            {
                writer.Write(false);
            }
            writer.Write(this.IsStartingRoom);
            DungeonId = DungeonId ?? new int?(-1);
            writer.Write(this.DungeonId.Value);
            writer.Write(this.Area.NullCheck());
            writer.Write(this.IsEndCap);
            writer.Write(this.Length);
            writer.Write(this.IsHub);
        }

        public override string ToString()
        {
            if (this.RoomDefinitionType == RoomDefinitionType.Dungeon)
            {
                return string.Format("Dungeon:    {0} {1}", this.RoomName, this.Area);
            }
            return string.Format("Room:   {0}", this.RoomName);
        }
    }

}
