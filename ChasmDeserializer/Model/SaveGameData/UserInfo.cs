using System;
using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Interfaces;

namespace ChasmDeserializer.Model
{
    public class UserInfo : IBinarySaveLoad
    {
        public string Version;
        public string Email;
        public Dictionary<string, bool> Achievements = new Dictionary<string, bool>();
        public Dictionary<string, int> UserStats = new Dictionary<string, int>();
        public Dictionary<Guid, float> FullTimePlayed = new Dictionary<Guid, float>();
        public List<Guid> DeadGames = new List<Guid>();

        public UserInfo()
        {
            this.UserStats.Add("GamesPlayed", 0);
            this.UserStats.Add("TreasureCollected", 0);
            this.UserStats.Add("NormalGamesWon", 0);
            this.UserStats.Add("HardGamesWon", 0);
            this.UserStats.Add("MortalGamesWon", 0);
            this.UserStats.Add("GoldSpent", 0);
            this.UserStats.Add("EnemiesSlain", 0);
            this.UserStats.Add("TimesSaved", 0);
            this.UserStats.Add("Deaths", 0);
            this.UserStats.Add("wendigo_drop", 0);
            this.UserStats.Add("catakiller_drop", 0);
            this.UserStats.Add("golem_drop", 0);
            this.UserStats.Add("trell_drop", 0);
            this.UserStats.Add("shaman_drop", 0);
        }

        public void Load(BinaryReader reader)
        {
            this.Version = reader.ReadString();
            if (this.Version == "1.6")
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    int value = reader.ReadInt32();
                    this.UserStats[key] = value;
 
                }
                count = reader.ReadInt32();
                for (int j = 0; j < count; j++)
                {
                    reader.ReadString();
                }
                this.Email = reader.ReadString();
                this.FullTimePlayed.Clear();
                count = reader.ReadInt32();
                for (int k = 0; k < count; k++)
                {
                    Guid key2 = Guid.Parse(reader.ReadString());
                    this.FullTimePlayed.Add(key2, reader.ReadSingle());
                }
                // Achivements
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    string id = reader.ReadString();
                    bool isUnlocked = reader.ReadBoolean();
                    this.Achievements[id] = isUnlocked;
                }

                count = reader.ReadInt32();
                for (int l = 0; l < count; l++)
                {
                    Guid item = Guid.Parse(reader.ReadString());
                    this.DeadGames.Add(item);
                }
                return;
            }
            Console.WriteLine("UserInfo: Failed Version Check");
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Version);
            writer.Write(this.UserStats.Count);
            foreach (string text in this.UserStats.Keys)
            {
                writer.Write(text);
                int value = this.UserStats[text];
                writer.Write(value);
            }
            writer.Write(0);
            writer.Write(this.Email.NullCheck());
            writer.Write(this.FullTimePlayed.Count);
            foreach (var game in this.FullTimePlayed)
            {
                writer.Write(game.Key.ToString());
                writer.Write(game.Value);
            }
            writer.Write(this.Achievements.Count);
            foreach (var achievement in this.Achievements)
            {
                writer.Write(achievement.Key);
                writer.Write(achievement.Value);
            }
            writer.Write(this.DeadGames.Count);
            foreach (Guid guid in this.DeadGames)
            {
                writer.Write(guid.ToString());
            }
            writer.Flush();
        }

    }
}

