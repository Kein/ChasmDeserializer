using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public class Dungeon
    {
        public int Id;
        public Vector2 Location;
        public int height;
        public int width;
        public Vector2 margin;
        public bool IsVisible;
        public List<DungeonConnection> Connections;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Id);
            writer.Write(this.Location);
            writer.Write(this.height);
            writer.Write(this.width);
            writer.Write(this.margin);
            writer.Write(this.IsVisible);
        }
        public void Load(BinaryReader reader)
        {
            this.Id = reader.ReadInt32();
            this.Location = reader.ReadVector2();
            this.height = reader.ReadInt32();
            this.width = reader.ReadInt32();
            this.margin = reader.ReadVector2();
            this.IsVisible = reader.ReadBoolean();
        }

        public void LoadConnections(BinaryReader reader)
        {
            int count = reader.ReadInt32();
            Connections =  new List<DungeonConnection>(count);
            for (int i = 0; i < count; i++)
            {
                DungeonConnection dungeonConnection = new DungeonConnection();
                dungeonConnection.Load(reader);
                this.Connections.Add(dungeonConnection);
            }
        }

        public void SaveConnections(BinaryWriter writer)
        {
            writer.Write(this.Connections.Count);
            foreach (var connection in this.Connections)
                connection.Save(writer);
        }
    }
}
