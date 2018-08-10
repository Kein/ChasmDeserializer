using System.IO;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public struct AreaConnectionDef
    {
        public string Area;
        public Direction LinkSide;

        public void Save(BinaryWriter writer)
        {
            Area = string.IsNullOrEmpty(this.Area) ? "" : Area;
            writer.Write(this.Area);
            writer.Write((int)this.LinkSide);
        }

        public static AreaConnectionDef Load(BinaryReader reader)
        {
            return new AreaConnectionDef
            {
                Area = reader.ReadString(),
                LinkSide = (Direction)reader.ReadInt32()
            };
        }

    }
}