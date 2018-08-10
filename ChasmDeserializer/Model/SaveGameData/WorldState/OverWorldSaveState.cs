using ChasmDeserializer.Model.SaveGameData.WorldState.Saveable;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public class OverWorldSaveState
    {
        public List<string> RoomTemplates;
        public List<RoomInstance> RoomInstances;
        public RoomInstance TransitionLeftEnd;
        public RoomInstance TransitionRightEnd;
        public HashSet<int> RoomTransitions;
        public Dictionary<string, List<Dungeon>> Areas;
        public NPCdata NPCinventory;

        public void Load(BinaryReader reader, float saveVersion)
        {
            int count = reader.ReadInt32();
            RoomTemplates = new List<string>(count);
            for (int i = 0; i < count; i++)
                this.RoomTemplates.Add(reader.ReadString());

            count = reader.ReadInt32();
            RoomInstances = new List<RoomInstance>(count);
            for (int j = 0; j < count; j++)
            {
                RoomInstance roomInstance = new RoomInstance();
                roomInstance.Load(reader);
                this.RoomInstances.Add(roomInstance);
            }

            this.TransitionLeftEnd = new RoomInstance();
            this.TransitionRightEnd = new RoomInstance();
            if (reader.ReadBoolean())
            {
                this.TransitionLeftEnd.Load(reader);
                this.TransitionRightEnd.Load(reader);
            }
            count = reader.ReadInt32();
            RoomTransitions = new HashSet<int>();
            for (int k = 0; k < count; k++)
                this.RoomTransitions.Add(reader.ReadInt32());

            count = reader.ReadInt32();
            Areas = new Dictionary<string, List<Dungeon>>(count);
            for (int l = 0; l < count; l++)
            {
                string key = reader.ReadString();
                int areaCount = reader.ReadInt32();
                var area = new List<Dungeon>(areaCount);
                for (int m = 0; m < areaCount; m++)
                {
                    Dungeon dungeon = new Dungeon();
                    dungeon.Load(reader);
                    area.Add(dungeon);
                }
                Areas.Add(key, area);
            }
            foreach (var areas in Areas.Values)
                foreach (var dungeon in areas)
                    dungeon.LoadConnections(reader);

            count = reader.ReadInt32();
            for (int n = 0; n < count; n++)
            {
                int key = reader.ReadInt32();
                var room = this.RoomInstances.Where(x => x.Id == key).First();
                room.LoadChests(reader);
            }

            NPCinventory = new NPCdata();
            NPCinventory.LoadInventory(reader, saveVersion);
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.RoomTemplates.Count);
            foreach (var item in RoomTemplates)
                writer.Write(item);
            writer.Write(this.RoomInstances.Count);
            foreach (RoomInstance roomInstance in this.RoomInstances)
                roomInstance.Save(writer);
            if (this.TransitionLeftEnd != null)
            {
                writer.Write(true);
                this.TransitionLeftEnd.Save(writer);
                this.TransitionRightEnd.Save(writer);
            }
            else
            {
                writer.Write(false);
            }
            writer.Write(this.RoomTransitions.Count);
            foreach (int value in this.RoomTransitions)
                writer.Write(value);
            writer.Write(this.Areas.Keys.Count);
            foreach (var KVpair in this.Areas)
            {
                writer.Write(KVpair.Key);
                writer.Write(KVpair.Value.Count);
                foreach (var dungeon in KVpair.Value)
                    dungeon.Save(writer);
            }
            foreach (var dungeonCollection in this.Areas.Values)
                foreach (var dungeon in dungeonCollection)
                    dungeon.SaveConnections(writer);
            writer.Write(this.RoomInstances.Count);
            foreach (var roomInstance in RoomInstances)
            {
                writer.Write(roomInstance.Id);
                roomInstance.SaveChests(writer);
            }
            NPCinventory.SaveInventory(writer);
        }
    }
}
