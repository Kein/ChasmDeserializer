using System.Collections.Generic;
using System.IO;
using ChasmDeserializer.Interfaces;

namespace ChasmDeserializer.Model.FormattedText
{
    class FormattedText : IBinarySaveLoad
    {
        public Dictionary<string, StringStore> strStores;

        public void Load(BinaryReader read)
        {
            int lenght = read.ReadInt32();
            strStores = new Dictionary<string, StringStore>(lenght);
            for (int i = 0; i < lenght; i++)
            {
                string key = read.ReadString();
                StringStore stringStore = StringStore.Load(read);
                strStores.Add(key, stringStore);
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(strStores.Count);
            foreach (var store in strStores)
            {
                writer.Write(store.Key);
                store.Value.Save(writer);
            }
        }
    }
}
