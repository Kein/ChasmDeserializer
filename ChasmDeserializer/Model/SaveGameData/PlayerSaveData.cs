using System;
using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Extensions;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace ChasmDeserializer.Model
{
    public class PlayerSaveData
    {
        public long SaveDateTime;
        public Vector2 PositionF;
        public PlayerStats Stats;
        public int HPCurrent;
        public int MPCurrent;
        public Item ItemHand;
        public Item WeaponHand;
        public Item EQHead;
        public Item EQBody;
        public Item EQAcc1;
        public Item EQAcc2;
        public List<Item> ItemsWeaponHand;
        public List<Item> ItemsActiveHand;
        public List<Item> ItemsHead;
        public List<Item> ItemsBody;
        public List<Item> ItemsAccessories;
        public List<Item> ItemsInventory;
        public long TimePlayed;
        public long LastBenchPayload;
        public string Name;
        public Vector2 DeathPosition;
        public int DeathRoom;
        public int DeathEssence;
        public string Explored;

        public void Load(BinaryReader read, float saveVersion)
        {
            this.SaveDateTime = read.ReadInt64();
            this.PositionF = read.ReadVector2();
            this.Stats = new PlayerStats();
            this.Stats.Load(read);
            if (saveVersion < 1.78f)
            {
                Console.WriteLine("Legacy xp version , checking {0}...", saveVersion);
                uint xpForLevel = PlayerStats.GetXpForLevel((uint)(this.Stats.Level + 1));
                uint xpForLevel2 = PlayerStats.GetXpForLevel((uint)this.Stats.Level);
                int num = (int)((xpForLevel - xpForLevel2) * 0.1f);
                int num2 = (int)((ulong)xpForLevel2 + (ulong)((long)num));
                if (this.Stats.Experience > num2)
                {
                    Console.WriteLine("Legacy Player XP:{0} should be {1}", this.Stats.Experience, num2);
                    this.Stats.Experience = num2;
                }
            }
            this.HPCurrent = read.ReadInt32();
            this.MPCurrent = read.ReadInt32();
            this.ItemHand = Item.Load(read, saveVersion);
            this.WeaponHand = Item.Load(read, saveVersion);
            this.EQHead = Item.Load(read, saveVersion);
            this.EQBody = Item.Load(read, saveVersion);
            this.EQAcc1 = Item.Load(read, saveVersion);
            this.EQAcc2 = Item.Load(read, saveVersion);
            int count = read.ReadInt32();
            ItemsWeaponHand = new List<Item>(count);
            for (int i = 0; i < count; i++)
            {
                this.ItemsWeaponHand.Add(Item.Load(read, saveVersion));
            }
            count = read.ReadInt32();
            ItemsActiveHand = new List<Item>(count);
            for (int j = 0; j < count; j++)
            {
                this.ItemsActiveHand.Add(Item.Load(read, saveVersion));
            }
            count = read.ReadInt32();
            ItemsHead = new List<Item>(count);
            for (int k = 0; k < count; k++)
            {
                this.ItemsHead.Add(Item.Load(read, saveVersion));
            }
            count = read.ReadInt32();
            ItemsBody = new List<Item>(count);
            for (int l = 0; l < count; l++)
            {
                this.ItemsBody.Add(Item.Load(read, saveVersion));
            }
            count = read.ReadInt32();
            ItemsAccessories = new List<Item>(count);
            for (int m = 0; m < count; m++)
            {
                this.ItemsAccessories.Add(Item.Load(read, saveVersion));
            }
            count = read.ReadInt32();
            ItemsInventory = new List<Item>(count);
            for (int n = 0; n < count; n++)
            {
                this.ItemsInventory.Add(Item.Load(read, saveVersion));
            }
            this.TimePlayed = read.ReadInt64();
            this.LastBenchPayload = read.ReadInt64();
            this.Name = read.ReadString();
            this.DeathPosition = read.ReadVector2();
            this.DeathRoom = read.ReadInt32();
            this.DeathEssence = read.ReadInt32();
            this.Explored = read.ReadString();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.SaveDateTime);
            writer.Write(this.PositionF);
            this.Stats.Save(writer);
            writer.Write(this.HPCurrent);
            writer.Write(this.MPCurrent);
            this.ItemHand.Save(writer);
            this.WeaponHand.Save(writer);
            this.EQHead.Save(writer);
            this.EQBody.Save(writer);
            this.EQAcc1.Save(writer);
            this.EQAcc2.Save(writer);
            writer.Write(this.ItemsWeaponHand.Count);
            foreach (Item item in this.ItemsWeaponHand)
            {
                item.Save(writer);
            }
            writer.Write(this.ItemsActiveHand.Count);
            foreach (Item item2 in this.ItemsActiveHand)
            {
                item2.Save(writer);
            }
            writer.Write(this.ItemsHead.Count);
            foreach (Item item3 in this.ItemsHead)
            {
                item3.Save(writer);
            }
            writer.Write(this.ItemsBody.Count);
            foreach (Item item4 in this.ItemsBody)
            {
                item4.Save(writer);
            }
            writer.Write(this.ItemsAccessories.Count);
            foreach (Item item5 in this.ItemsAccessories)
            {
                item5.Save(writer);
            }
            writer.Write(this.ItemsInventory.Count);
            foreach (Item item6 in this.ItemsInventory)
            {
                item6.Save(writer);
            }
            writer.Write(this.TimePlayed);
            writer.Write(this.LastBenchPayload);
            writer.Write(this.Name);
            writer.Write(this.DeathPosition);
            writer.Write(this.DeathRoom);
            writer.Write(this.DeathEssence);
            writer.Write(this.Explored);
        }
    }

    public class PlayerStats
    {
        public byte Level;
        public int HP;
        public int MP;
        public byte STR;
        public byte INT;
        public byte CON;
        public byte LCK;
        public uint Kills;
        public uint DamageTaken;
        public int Gold;
        public int Experience;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Level);
            writer.Write(this.HP);
            writer.Write(this.MP);
            writer.Write(this.STR);
            writer.Write(this.INT);
            writer.Write(this.CON);
            writer.Write(this.LCK);
            writer.Write(this.Kills);
            writer.Write(this.DamageTaken);
            writer.Write(this.Gold);
            writer.Write(this.Experience);
        }
        public void Load(BinaryReader reader)
        {
            this.Level = reader.ReadByte();
            this.HP = reader.ReadInt32();
            this.MP = reader.ReadInt32();
            this.STR = reader.ReadByte();
            this.INT = reader.ReadByte();
            this.CON = reader.ReadByte();
            this.LCK = reader.ReadByte();
            this.Kills = reader.ReadUInt32();
            this.DamageTaken = reader.ReadUInt32();
            this.Gold = reader.ReadInt32();
            this.Experience = reader.ReadInt32();
        }

        public static uint GetXpForLevel(uint level)
        {
            if (level >= (uint)(PlayerStats.ExpNeededPerLevel.Count - 1))
            {
                return 99999u;
            }
            return (uint)PlayerStats.ExpNeededPerLevel[(int)level];
        }

        [JsonIgnore]
        public static List<int> ExpNeededPerLevel = new List<int>
        {
            0,
            0,
            100,
            250,
            450,
            700,
            1000,
            1350,
            1750,
            2200,
            2700,
            3250,
            3850,
            4500,
            5200,
            5950,
            6750,
            7600,
            8500,
            9450,
            10450,
            11700,
            13200,
            14700,
            16200,
            17700,
            19200,
            21200,
            23000,
            25000,
            27000,
            29500,
            32000,
            35000,
            37500,
            40000,
            43000,
            46000,
            50000,
            54000,
            58500,
            63000,
            68000,
            73000,
            78000,
            82000,
            86000,
            90000,
            96000,
            100000,
            110000
        };
    }
}
