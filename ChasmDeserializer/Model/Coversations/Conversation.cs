using ChasmDeserializer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChasmDeserializer.Model.Coversations
{
    public class Conversation
    {
        public string Name;
        public List<string> SetWorldVariables;
        public List<string> UnsetWorldVariables;
        public List<ItemDefinition> RequiredItems;
        public List<string> RequiredWorldVariables;
        public List<string> RequiredNOWorldVariables;
        public bool RemoveRequiredItems;
        public List<string> GiveItem;
        public string YesCommand;
        public string NoCommand;
        public string ForwardCommand;
        public SpecialAction PostAction;
        public SpecialAction PreAction;
        public ConversationType ConversationType;
        public Tier UnlockTeir;

         public void Load(BinaryReader reader)
        {
            this.Name = reader.ReadString();
            this.SetWorldVariables = reader.ReadStrings();
            this.UnsetWorldVariables = reader.ReadStrings();
            int count = reader.ReadInt32();
            this.RequiredItems = new List<ItemDefinition>(count);
            for (int i = 0; i < count; i++)
            {
                ItemDefinition item = default(ItemDefinition);
                item.Load(reader);
                this.RequiredItems.Add(item);
            }
            this.RequiredWorldVariables = reader.ReadStrings();
            this.RequiredNOWorldVariables = reader.ReadStrings();
            this.RemoveRequiredItems = reader.ReadBoolean();
            this.GiveItem = reader.ReadStrings();
            this.YesCommand = reader.ReadString();
            this.NoCommand = reader.ReadString();
            this.ForwardCommand = reader.ReadString();
            this.PostAction = (SpecialAction)reader.ReadInt16();
            this.PreAction = (SpecialAction)reader.ReadInt16();
            this.ConversationType = (ConversationType)reader.ReadInt16();
            this.UnlockTeir = (Tier)reader.ReadInt16();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.SetWorldVariables);
            writer.Write(this.UnsetWorldVariables);
            writer.Write(this.RequiredItems.Count);
            for (int i = 0; i < this.RequiredItems.Count; i++)
                this.RequiredItems[i].Save(writer);
            writer.Write(this.RequiredWorldVariables);
            writer.Write(this.RequiredNOWorldVariables);
            writer.Write(this.RemoveRequiredItems);
            writer.Write(this.GiveItem);
            writer.Write(this.YesCommand.NullCheck());
            writer.Write(this.NoCommand.NullCheck());
            writer.Write(this.ForwardCommand.NullCheck());
            writer.Write((short)this.PostAction);
            writer.Write((short)this.PreAction);
            writer.Write((short)this.ConversationType);
            writer.Write((short)this.UnlockTeir);
        }
    }
}
