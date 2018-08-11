using System;
using System.IO;
using ChasmDeserializer.Extensions;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public struct EnemyInfo
    {
        public string EnemyType;
        public Direction Facing;
        public bool FlipX;
        public int Id;
        public bool RandomPlaced;
        public bool SaveDeath;
        public string SaveKey;
        public Vector2 SpawnPosition;
        public int SpawnRadius;
        public float Scaling;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.EnemyType);
            writer.Write((int)this.Facing);
            writer.Write(this.FlipX);
            writer.Write(this.Id);
            writer.Write(this.RandomPlaced);
            writer.Write(this.SaveDeath);
            writer.Write(this.SaveKey.NullCheck());
            writer.Write(this.SpawnPosition);
            writer.Write(this.SpawnRadius);
            writer.Write(this.Scaling);
        }
        public void Load(BinaryReader reader)
        {
            this.EnemyType = reader.ReadString();
            this.Facing = (Direction)reader.ReadInt32();
            this.FlipX = reader.ReadBoolean();
            this.Id = reader.ReadInt32();
            this.RandomPlaced = reader.ReadBoolean();
            this.SaveDeath = reader.ReadBoolean();
            this.SaveKey = reader.ReadString();
            this.SpawnPosition = reader.ReadVector2();
            this.SpawnRadius = reader.ReadInt32();
            this.Scaling = reader.ReadSingle();
        }
    }
}
