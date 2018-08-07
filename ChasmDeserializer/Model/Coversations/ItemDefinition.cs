using System;
using System.IO;

namespace ChasmDeserializer.Model.Coversations
{
    public struct ItemDefinition
    {
        public bool IsType;
        public ItemType ItemType;
        public string Item;
        public int Count;
        public bool Equipped;
        public bool NotHave;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.IsType);
            if (this.IsType)
                writer.Write(this.ItemType.ToString());
            else
                writer.Write(this.Item);
            writer.Write(this.Count);
            writer.Write(this.Equipped);
            writer.Write(this.NotHave);
        }

        public void Load(BinaryReader reader)
        {
            this.IsType = reader.ReadBoolean();
            if (this.IsType)
                this.ItemType = (ItemType)Enum.Parse(typeof(ItemType), reader.ReadString());
            else
                this.Item = reader.ReadString();
            this.Count = reader.ReadInt32();
            this.Equipped = reader.ReadBoolean();
            this.NotHave = reader.ReadBoolean();
        }

        public override string ToString()
        {
            string text;
            text = this.NotHave ? "No" : "";
            text = this.Equipped ? text += " EQ" : text;
            return this.IsType ? string.Format("{0} {1} :{2}", text, this.ItemType, this.Count) : string.Format("{0} {1} :{2}", text, this.Item, this.Count);
        }
    }
}
