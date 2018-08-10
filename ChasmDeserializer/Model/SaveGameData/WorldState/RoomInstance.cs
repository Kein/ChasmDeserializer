using ChasmDeserializer.Interfaces;
using ChasmDeserializer.Model.SaveGameData.WorldState.Saveable;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public class RoomInstance
    {
        public int Id;
        public bool IsDungeon;
        public int DungeonId;
        public string Area;
        public bool IsRoomTransition;
        public RoomFlags RoomFlags;
        public float DifficultyModifier;
        public int X;
        public int Y;
        public int VariationIndex;
        public int SpawnIndex;
        public int TemplateId;
        public bool PlayerEntersLeft;
        public Dictionary<int, LinkDefinition> LeftDoors;
        public Dictionary<int, LinkDefinition> RightDoors;
        public Dictionary<int, LinkDefinition> CustomDoors;
        public List<EnemyInfo> EnemyInfo;
        public List<KeyValuePair<string, GenericProp>> RandomProps;
        public List<AreaConnectionDef> AreaConnections;
        public bool IsDark;
        public bool IsBacktrack;
        public Dictionary<string, string> LocalVaribles;
        public bool IsHub;
        public List<KeyValuePair<int, int>> IdTranslations;
        public List<ContainerData> ChestData;
        public List<ContainerData> CrateData;

        public void Load(BinaryReader reader)
        {
            this.Id = reader.ReadInt32();
            this.IsDungeon = reader.ReadBoolean();
            this.DungeonId = reader.ReadInt32();
            this.Area = reader.ReadString();
            this.IsRoomTransition = reader.ReadBoolean();
            this.RoomFlags = (RoomFlags)reader.ReadInt32();
            this.DifficultyModifier = reader.ReadSingle();
            this.X = reader.ReadInt32();
            this.Y = reader.ReadInt32();
            this.VariationIndex = reader.ReadInt32();
            this.SpawnIndex = reader.ReadInt32();
            this.TemplateId = reader.ReadInt32();
            this.PlayerEntersLeft = reader.ReadBoolean();
            int count = reader.ReadInt32();
            LeftDoors = new Dictionary<int, LinkDefinition>(count);
            for (int i = 0; i < count; i++)
            {
                int key = reader.ReadInt32();
                LinkDefinition value = default(LinkDefinition);
                value.Load(reader);
                this.LeftDoors.Add(key, value);
            }
            count = reader.ReadInt32();
            RightDoors = new Dictionary<int, LinkDefinition>(count);
            for (int j = 0; j < count; j++)
            {
                int key = reader.ReadInt32();
                LinkDefinition value = default(LinkDefinition);
                value.Load(reader);
                this.RightDoors.Add(key, value);
            }
            count = reader.ReadInt32();
            CustomDoors = new Dictionary<int, LinkDefinition>(count);
            for (int k = 0; k < count; k++)
            {
                int key = reader.ReadInt32();
                LinkDefinition value = default(LinkDefinition);
                value.Load(reader);
                this.CustomDoors.Add(key, value);
            }
            count = reader.ReadInt32();
            EnemyInfo = new List<EnemyInfo>(count);
            for (int l = 0; l < count; l++)
            {
                EnemyInfo item = default(EnemyInfo);
                item.Load(reader);
                this.EnemyInfo.Add(item);
            }
            count = reader.ReadInt32();
            RandomProps = new List<KeyValuePair<string, GenericProp>>(count);
            for (int m = 0; m < count; m++)
            {
                string fullName = reader.ReadString();
                string localName = fullName.IndexOf("ComboLock", StringComparison.OrdinalIgnoreCase) > -1
                    ? "ComboLockHint"
                    : fullName.Substring(fullName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase) + 1);
                Type type = Type.GetType("ChasmDeserializer.Model.SaveGameData.WorldState.Saveable." + localName);
                GenericProp genericProp = Activator.CreateInstance(type) as GenericProp;
                this.RandomProps.Add(new KeyValuePair<string, GenericProp>(fullName, genericProp));
                ISaveGame saveGame = genericProp as ISaveGame;
                saveGame.LoadGame(reader);
            }
            count = reader.ReadInt32();
            AreaConnections = new List<AreaConnectionDef>(count);
            for (int n = 0; n < count; n++)
                this.AreaConnections.Add(AreaConnectionDef.Load(reader));
            this.IsDark = reader.ReadBoolean();
            this.IsBacktrack = reader.ReadBoolean();
            count = reader.ReadInt32();
            LocalVaribles = new Dictionary<string, string>(count);
            for (int num4 = 0; num4 < count; num4++)
            {
                string key = reader.ReadString();
                string value = reader.ReadString();
                this.LocalVaribles.Add(key, value);
            }
            this.IsHub = reader.ReadBoolean();
            count = reader.ReadInt32();
            IdTranslations = new List<KeyValuePair<int, int>>(count);
            for (int num6 = 0; num6 < count; num6++)
            {
                int key = reader.ReadInt32();
                int value = reader.ReadInt32();
                this.IdTranslations.Add(new KeyValuePair<int, int>(key, value));
            }
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Id);
            writer.Write(this.IsDungeon);
            writer.Write(this.DungeonId);
            writer.Write(this.Area.NullCheck());
            writer.Write(this.IsRoomTransition);
            writer.Write((int)this.RoomFlags);
            writer.Write(this.DifficultyModifier);
            writer.Write(this.X);
            writer.Write(this.Y);
            writer.Write(this.VariationIndex);
            writer.Write(this.SpawnIndex);
            writer.Write(this.TemplateId);
            writer.Write(this.PlayerEntersLeft);
            writer.Write(this.LeftDoors.Count);
            foreach (int num in this.LeftDoors.Keys)
            {
                writer.Write(num);
                this.LeftDoors[num].Save(writer);
            }
            writer.Write(this.RightDoors.Count);
            foreach (int num2 in this.RightDoors.Keys)
            {
                writer.Write(num2);
                this.RightDoors[num2].Save(writer);
            }
            writer.Write(this.CustomDoors.Count);
            foreach (int num3 in this.CustomDoors.Keys)
            {
                writer.Write(num3);
                this.CustomDoors[num3].Save(writer);
            }
            writer.Write(this.EnemyInfo.Count);
            foreach (EnemyInfo enemyInfo in this.EnemyInfo)
            {
                enemyInfo.Save(writer);
            }
            writer.Write(RandomProps.Count);
            foreach (var item in RandomProps)
            {
                writer.Write(item.Key);
                ISaveGame prop = item.Value as ISaveGame;
                prop.SaveGame(writer);
            }
            writer.Write(this.AreaConnections.Count);
            foreach (AreaConnectionDef areaConnectionDef in this.AreaConnections)
                areaConnectionDef.Save(writer);
            writer.Write(this.IsDark);
            writer.Write(this.IsBacktrack);
            writer.Write(this.LocalVaribles.Count);
            foreach (string text in this.LocalVaribles.Keys)
            {
                writer.Write(text);
                writer.Write(this.LocalVaribles[text]);
            }
            writer.Write(this.IsHub);
            if (this.IdTranslations == null)
            {
                writer.Write(0);
                return;
            }
            writer.Write(this.IdTranslations.Count);
            for (int i = 0; i < this.IdTranslations.Count; i++)
            {
                KeyValuePair<int, int> keyValuePair = this.IdTranslations[i];
                writer.Write(keyValuePair.Key);
                writer.Write(keyValuePair.Value);
            }
        }

        public void LoadChests(BinaryReader reader)
        {
            int objCount = reader.ReadInt32();
            ChestData = new List<ContainerData>(objCount);
            for (int i = 0; i < objCount; i++)
            {
                int id = reader.ReadInt32();
                string droppedItem = reader.ReadString();
                ChestData.Add(new ContainerData() { ID = id, DroppedItem = droppedItem });
            }
            objCount = reader.ReadInt32();
            CrateData = new List<ContainerData>(objCount);
            for (int j = 0; j < objCount; j++)
            {
                int id = reader.ReadInt32();
                string droppedItem = reader.ReadString();
                CrateData.Add(new ContainerData() { ID = id, DroppedItem = droppedItem });
            }
        }

        public void SaveChests(BinaryWriter writer)
        {
            writer.Write(ChestData.Count);
            foreach (var chest in ChestData)
            {
               writer.Write(chest.ID);
               writer.Write(chest.DroppedItem.NullCheck());
            }
            writer.Write(CrateData.Count);
            foreach (var crate in CrateData)
            {
                writer.Write(crate.ID);
                writer.Write(crate.DroppedItem.NullCheck());
            }
        }
    }
}
