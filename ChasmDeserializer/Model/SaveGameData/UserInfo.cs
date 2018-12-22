using System;
using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Extensions;
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
                    // Avoid having UserStats populated like in the original
                    // source code - this way we keep backward compatibility
    		        UserStats[key] = value;
                }
                //some leftover stuff from old format?
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
                // Achievements
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
            foreach (var stat in UserStats)
            {
                writer.Write(stat.Key);
                writer.Write(stat.Value);
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

