using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Overworld
{
    class OverWorldManager : IBinarySaveLoad
    {
        public Dictionary<int, RoomDefinition> RoomDefinitions;
        public Dictionary<Point, List<string>> RoomTransitionTagsRev2;
        public WorldSettings WorldSettings;

        public void Load(BinaryReader binaryReader)
        {
            int count = binaryReader.ReadInt32();
            RoomDefinitions = new Dictionary<int, RoomDefinition>(count);
            for (int i = 0; i < count; i++)
            {
                int key = binaryReader.ReadInt32();
                RoomDefinition roomDefinition = new RoomDefinition();
                roomDefinition.Load(binaryReader);
                RoomDefinitions.Add(key, roomDefinition);
            }
            count = binaryReader.ReadInt32();
            RoomTransitionTagsRev2 = new Dictionary<Point, List<string>>(count);
            for (int j = 0; j < count; j++)
            {
                Point key2 = binaryReader.ReadPoint();
                List<string> list = new List<string>();
                int innerCount = binaryReader.ReadInt32();
                for (int k = 0; k < innerCount; k++)
                {
                    list.Add(binaryReader.ReadString());
                }
                RoomTransitionTagsRev2.Add(key2, list);
            }
            WorldSettings = new WorldSettings();
            WorldSettings.Load(binaryReader);
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(RoomDefinitions.Count);
            foreach (var roomDef in RoomDefinitions)
            {
                writer.Write(roomDef.Key);
                roomDef.Value.Save(writer);
            }
            writer.Write(RoomTransitionTagsRev2.Count);
            foreach (var roomTag in RoomTransitionTagsRev2)
            {
                writer.Write(roomTag.Key);
                writer.Write(roomTag.Value);
            }
            WorldSettings.Save(writer);
        }
    }
}
