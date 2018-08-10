using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChasmDeserializer.Model.SaveGameData.WorldState.Saveable
{
    public class NPCdata
    {
        public Dictionary<string, List<Item>> Inventory;
        public Dictionary<string, Dictionary<Tier, List<string>>> ShopTiers;

        public void SaveInventory(BinaryWriter writer)
        {
            writer.Write(Inventory.Count);
            foreach (var KV in Inventory)
            {
                writer.Write(KV.Key);
                writer.Write(KV.Value.Count);
                foreach (Item item in KV.Value)
                    item.Save(writer);
            }
            writer.Write(ShopTiers.Count);
            foreach (var KV in ShopTiers)
            {
                writer.Write(KV.Key);
                writer.Write(KV.Value.Count);
                foreach (var TierString in KV.Value)
                {
                    writer.Write((int)TierString.Key);
                    writer.Write(TierString.Value.Count);
                    foreach (string s in TierString.Value)
                        writer.Write(s);
                }
            }
        }

        public void LoadInventory(BinaryReader reader, float saveVersion)
        {
            int count = reader.ReadInt32();
            Inventory = new Dictionary<string, List<Item>>(count);
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                int innerCount = reader.ReadInt32();
                List<Item> list = new List<Item>(innerCount);
                for (int j = 0; j < innerCount; j++)
                    list.Add(Item.Load(reader, saveVersion));
                Inventory.Add(key, list);
            }
            count = reader.ReadInt32();
            ShopTiers = new Dictionary<string, Dictionary<Tier, List<string>>>(count);
            for (int k = 0; k < count; k++)
            {
                string key = reader.ReadString();
                int innerCount = reader.ReadInt32();
                Dictionary<Tier, List<string>> tiers = new Dictionary<Tier, List<string>>(innerCount);
                ShopTiers.Add(key, tiers);
                for (int l = 0; l < innerCount; l++)
                {
                    Tier tier = (Tier)reader.ReadInt32();
                    int listCount = reader.ReadInt32();
                    List<string> strings = new List<string>(listCount);
                    tiers.Add(tier, strings);
                    for (int m = 0; m < listCount; m++)
                        strings.Add(reader.ReadString());
                }
            }
        }
    }
}
