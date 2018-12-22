using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ChasmDeserializer.Extensions;
using ChasmDeserializer.Interfaces;
using ChasmDeserializer.Model.SaveGameData.WorldState;
using Newtonsoft.Json;

namespace ChasmDeserializer.Model
{
    public class WorldSaveState : IBinarySaveLoad
    {
        public string CurrentSaveVersion;
        public int SaveSlot;
        public string ProfileIconKey;
        public bool IsNewGame;
        public Difficulty Difficulty;
        public bool IsMortal;
        public bool Died;
        public Dictionary<string, int> Variables;
        public PlayerSaveData PlayerSaveData;
        public int Seed;
        public GameMode Mode;
        public CurrentRoomData CurrentRoom;
        public List<string> AreasExplored;
        public Dictionary<string, List<int>> DungeonsExplored;
        public HashSet<RoomCordinate> ScreensExplored;
        public CurrentRoomData PerchRoom;
        public Dictionary<string, EnemyCounter> EnemiesKilled;
        public string OverworldFileName;
        public Guid UniqueId;
        public int CratesKilled;
        public int CrateCount;
        public Dictionary<int, RoomMark> RoomMarks;
        public float OverworldFileVersion;
        public OverWorldSaveState OverWorldSaveState;
        public string GameBuildVersion;
        public ClassMode ClassMode;
        [JsonIgnore]
        private byte[] OverWorldSaveStateBytes;

        public void Load(BinaryReader read)
        {
            CurrentSaveVersion = read.ReadString();
            float version = string.IsNullOrEmpty(CurrentSaveVersion) ? -1f : float.Parse(CurrentSaveVersion, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
            if (version <= 0f)
            {
                Console.WriteLine($"Invalid or very outdated save data version detected: {version}");
                return;
            }
            SaveSlot = read.ReadInt32();
            ProfileIconKey = read.ReadString();
            ProfileIconKey = string.IsNullOrEmpty(ProfileIconKey) ? "" : ProfileIconKey;
            IsNewGame = read.ReadBoolean();
            if (version < 1.77f)
                Difficulty = read.ReadBoolean() ? Difficulty.Hard : Difficulty.Normal;
            else
                Difficulty = (Difficulty)read.ReadByte();
            IsMortal = read.ReadBoolean();
            Died = read.ReadBoolean();
            int count = read.ReadInt32();
            Variables = new Dictionary<string, int>(count);
            for (int i = 0; i < count; i++)
            {
                string key = read.ReadString();
                int value = read.ReadInt32();
                Variables.Add(key, value);
            }
            PlayerSaveData = new PlayerSaveData();
            PlayerSaveData.Load(read, version);
            Seed = read.ReadInt32();
            CurrentRoom = new CurrentRoomData();
            CurrentRoom.Load(read);
            Mode = (GameMode)read.ReadInt32();
            count = read.ReadInt32();
            AreasExplored = new List<string>(count);
            for (int j = 0; j < count; j++)
                AreasExplored.Add(read.ReadString());
            count = read.ReadInt32();
            DungeonsExplored = new Dictionary<string, List<int>>(count);
            for (int k = 0; k < count; k++)
            {
                string key = read.ReadString();
                int innerCount = read.ReadInt32();
                List<int> list = new List<int>(innerCount);
                for (int l = 0; l < innerCount; l++)
                    list.Add(read.ReadInt32());
                DungeonsExplored.Add(key, list);
            }
            ScreensExplored = new HashSet<RoomCordinate>();
            count = read.ReadInt32();
            for (int m = 0; m < count; m++)
                ScreensExplored.Add(RoomCordinate.Load(read));
            if (read.BaseStream.Position < read.BaseStream.Length)
            {
                PerchRoom = new CurrentRoomData();
                PerchRoom.Load(read);
            }
            if (read.BaseStream.Position < read.BaseStream.Length)
            {
                count = read.ReadInt32();
                EnemiesKilled = new Dictionary<string, EnemyCounter>(count);
                for (int n = 0; n < count; n++)
                {
                    string typeName = read.ReadString();
                    int current = read.ReadInt32();
                    int needed = read.ReadInt32();
                    bool unlocked = read.ReadBoolean();
                    bool droppedUncommon = read.ReadBoolean();
                    bool droppedRare = read.ReadBoolean();
                    EnemiesKilled.Add(typeName, new EnemyCounter
                    {
                        Current = current,
                        Needed = needed,
                        Unlocked = unlocked,
                        DroppedUncommon = droppedUncommon,
                        DroppedRare = droppedRare
                    });
                }
            }
            OverworldFileName = read.ReadString();
            UniqueId = Guid.Parse(read.ReadString());
            CratesKilled = read.ReadInt32();
            CrateCount = read.ReadInt32();
            if (version >= 1.77f)
            {
                int rmCount = read.ReadInt32();
                RoomMarks = new Dictionary<int, RoomMark>(rmCount);
                for (int num5 = 0; num5 < rmCount; num5++)
                {
                    int roomkey = read.ReadInt32();
                    RoomMark roomMark = RoomMark.Load(read);
                    RoomMarks.Add(roomkey, roomMark);
                }
                GameBuildVersion = version >= 1.79f ? read.ReadString() : null;
                ClassMode = version >= 1.8f ? (ClassMode)read.ReadInt32() : 0;
            }
            bool hasOverworldData = read.ReadBoolean();
            if (hasOverworldData)
            {
                if (version >= 1.76f)
                {
                    OverworldFileVersion = read.ReadSingle();
                    var size = read.ReadInt32();
                    OverWorldSaveStateBytes = read.ReadBytes(size);
                }
                else
                {
                    OverworldFileVersion = version;
                    OverWorldSaveStateBytes = read.ReadAllBytes();
                }
                OverWorldSaveState = new OverWorldSaveState();
                using (MemoryStream input = new MemoryStream(OverWorldSaveStateBytes))
                {
                    using (BinaryReader reader = new BinaryReader(input))
                    {
                        OverWorldSaveState.Load(reader, OverworldFileVersion);
                    }
                }
            }
        }

         public void Save(BinaryWriter writer)
        {
            float version = string.IsNullOrEmpty(CurrentSaveVersion) ? -1f : float.Parse(CurrentSaveVersion, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
            writer.Write(CurrentSaveVersion);
            writer.Write(this.SaveSlot);
            writer.Write(this.ProfileIconKey.NullCheck());
            writer.Write(this.IsNewGame);
            writer.Write((byte)this.Difficulty);
            writer.Write(this.IsMortal);
            writer.Write(this.Died);
            writer.Write(this.Variables.Keys.Count);
            foreach (var item in this.Variables)
            {
                writer.Write(item.Key);
                writer.Write(item.Value);
            }
            this.PlayerSaveData.Save(writer);
            writer.Write(this.Seed);
            this.CurrentRoom.Save(writer);
            writer.Write((int)this.Mode);
            writer.Write(this.AreasExplored.Count);
            foreach (string value in this.AreasExplored)
            {
                writer.Write(value);
            }
            writer.Write(this.DungeonsExplored.Count);
            foreach (var entry in this.DungeonsExplored)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value.Count);
                foreach (int value2 in entry.Value)
                {
                    writer.Write(value2);
                }
            }
            writer.Write(this.ScreensExplored.Count);
            foreach (RoomCordinate roomCordinate in this.ScreensExplored)
            {
                roomCordinate.Save(writer);
            }
            if (this.PerchRoom == null)
            {
                this.PerchRoom = new CurrentRoomData();
            }
            this.PerchRoom.Save(writer);
            writer.Write(this.EnemiesKilled.Count);
            foreach (string enemyType in this.EnemiesKilled.Keys)
            {
                writer.Write(enemyType);
                writer.Write(this.EnemiesKilled[enemyType].Current);
                writer.Write(this.EnemiesKilled[enemyType].Needed);
                writer.Write(this.EnemiesKilled[enemyType].Unlocked);
                writer.Write(this.EnemiesKilled[enemyType].DroppedUncommon);
                writer.Write(this.EnemiesKilled[enemyType].DroppedRare);
            }
            writer.Write(this.OverworldFileName.NullCheck());
            writer.Write(this.UniqueId.ToString());
            writer.Write(this.CratesKilled);
            writer.Write(this.CrateCount);
            writer.Write(this.RoomMarks.Count);
            foreach (var mark in this.RoomMarks)
            {
                writer.Write(mark.Key);
                mark.Value.Save(writer);
            }
            // Shouldnt just write game build version and class
            // - need backward compatibility?
            if (this.GameBuildVersion != null)
                writer.Write(this.GameBuildVersion.NullCheck());
            // Same here, older versions have to classes
            if (version >= 1.8f)
                writer.Write((int)ClassMode);
            if (this.OverWorldSaveState != null)
            {
                writer.Write(true);
                writer.Write(version);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                    {
                        OverWorldSaveState.Save(binaryWriter);
                    }
                    byte[] array = memoryStream.ToArray();
                    writer.Write(array.Length);
                    writer.Write(array);
                }
                return;
            }
            if (OverWorldSaveStateBytes != null && this.OverWorldSaveStateBytes.Length > 0)
            {
                writer.Write(true);
                writer.Write(this.OverworldFileVersion);
                writer.Write(this.OverWorldSaveStateBytes.Length);
                writer.Write(this.OverWorldSaveStateBytes);
                return;
            }
            writer.Write(false);
        }

    }

    public struct RoomCordinate : IEquatable<RoomCordinate>
    {
        public int Id;
        public int X;
        public int Y;
        public RoomCordinate(int id, int x, int y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }
        public static RoomCordinate Load(BinaryReader read)
        {
            return new RoomCordinate(read.ReadInt32(), read.ReadInt32(), read.ReadInt32());
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Id);
            writer.Write(this.X);
            writer.Write(this.Y);
        }

        public bool Equals(RoomCordinate other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            int num = 17;
            num = num * 23 + this.Id.GetHashCode();
            num = num * 23 + this.X.GetHashCode();
            return num * 23 + this.Y.GetHashCode();
        }

    }
    public struct EnemyCounter
    {
        public int Current;
        public int Needed;
        public bool Unlocked;
        public bool DroppedUncommon;
        public bool DroppedRare;
    }
    public struct RoomMark
    {
        public byte X;
        public byte Y;
        public RoomMark(byte x, byte y)
        {
            this.X = x;
            this.Y = y;
        }
        public static RoomMark Load(BinaryReader read)
        {
            var x = read.ReadByte();
            var y = read.ReadByte();
            return new RoomMark(x, y);
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.X);
            writer.Write(this.Y);
        }

    }

}
