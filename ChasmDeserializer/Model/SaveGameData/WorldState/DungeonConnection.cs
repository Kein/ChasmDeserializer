using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public class DungeonConnection
    {
        public int ConnectionAid;
        public int ConnectionBid;
        public int DungeonAid;
        public int DungeonBid;
        public RoomFlags IconType;
        public List<Vector2> PathList;
        public bool ShowIcon;

        public void Load(BinaryReader reader)
        {
            this.ConnectionAid = reader.ReadInt32(); 
            this.ConnectionBid = reader.ReadInt32();
            this.DungeonAid = reader.ReadInt32();
            this.DungeonBid = reader.ReadInt32();
            this.IconType = (RoomFlags)reader.ReadInt32();
            int count = reader.ReadInt32();
            PathList = new List<Vector2>(count);
            for (int i = 0; i < count; i++)
                this.PathList.Add(reader.ReadVector2());
            this.ShowIcon = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.ConnectionAid);
            writer.Write(this.ConnectionBid);
            writer.Write(this.DungeonAid);
            writer.Write(this.DungeonBid);
            writer.Write((int)this.IconType);
            writer.Write(this.PathList.Count);
            foreach (Vector2 vec in this.PathList)
                writer.Write(vec);
            writer.Write(this.ShowIcon);
        }

    }
}
