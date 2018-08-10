namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public enum ConditionType
    {
        None,
        Property,
        Method
    }
    public enum RoomFlags
    {
        None = 0,
        CampFire = 1,
        Portal = 2,
        AreaDoors = 4,
        NormalDoors = 8,
        Altar = 16,
        Treasure = 32,
        Fountain = 64,
        Shrine = 128,
        DoorConnections = 256,
        TrapRoom = 512,
        BossRoom = 1024,
        HubConnector = 4096,
        Secret = 8192
    }
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
    public enum CrateDropType
    {
        Predetermined,
        HealthPickup
    }
    public enum ShrineType : byte
    {
        FortuneBuff,
        ExperienceBuff,
        Protection,
        Healing,
        Mana,
        Gem,
        Spell
    }
}
