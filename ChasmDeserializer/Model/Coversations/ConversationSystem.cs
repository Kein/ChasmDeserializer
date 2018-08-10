using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Interfaces;

namespace ChasmDeserializer.Model.Coversations
{
    class ConversationSystem : IBinarySaveLoad
    {
        public Dictionary<string, List<Conversation>> ConversationsByNPC;

        public void Load(BinaryReader reader)
        {
            int count = reader.ReadInt32();
            this.ConversationsByNPC = new Dictionary<string, List<Conversation>>(count);
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                int count2 = reader.ReadInt32();
                List<Conversation> list = new List<Conversation>(count2);
                for (int j = 0; j < count2; j++)
                {
                    Conversation conversation = new Conversation();
                    conversation.Load(reader);
                    list.Add(conversation);
                }
                this.ConversationsByNPC.Add(key, list);
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.ConversationsByNPC.Count);
            foreach (var keyValue in this.ConversationsByNPC)
            {
                writer.Write(keyValue.Key);
                writer.Write(keyValue.Value.Count);
                foreach (var convo in keyValue.Value)
                    convo.Save(writer);
            }
        }
    }
}
