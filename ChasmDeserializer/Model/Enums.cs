namespace ChasmDeserializer.Model
{
    public enum HitboxType : byte
    {
        Body1,
        Body2,
        Body3,
        Attack1 = 32,
        Attack2,
        Attack3,
        Projectile1 = 64,
        Projectile2,
        Projectile3
    }
    public enum ItemType
    {
        None,
        Sword,
        Whip,
        TwoHanded,
        Pole,
        Rod,
        Spell,
        Head,
        Armor,
        Accessory,
        Potion,
        Gem,
        Food,
        Key,
        Knife,
        Scroll,
        PowerUp,
        Note,
        Elixir,
        Rune,
        Ingredient,
        Material,
        Recipe,
        Shield,
        Fist,
        Treasure,
        Moneybag,
        Token,
        Shovel,
        Seal
    }
    public enum SpecialAction
    {
        None,
        SpecialAction1,
        SpecialAction2,
        SpecialAction3,
        SpecialAction4,
        SpecialAction5
    }
    public enum ConversationType
    {
        None,
        Intialization
    }
    public enum Tier
    {
        None,
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Tier6,
        Tier7,
        Tier8
    }
    public enum RoomDefinitionType
    {
        Room,
        Dungeon
    }
    public enum LinkDirection
    {
        Left,
        Right,
        Custom
    }
    public enum RoomLinkType
    {
        RoomToRoom,
        RoomToDungeon
    }
    public enum TagCondition
    {
        MinMax,
        MustHave,
        MustNotHave,
        Chance
    }
    public enum DungeonFlags
    {
        None,
        Dark,
        Backtrack,
        TallHub,
        BuildUp,
        BuildDown,
        Secret
    }
    public enum ColorModes
    {
        AtBirth,
        OverTime
    }
    public enum DirectionTypes
    {
        Uniform,
        Direction
    }
    public enum EmitterTypes
    {
        Point,
        Box
    }
    public enum ParticleTypes
    {
        Pixel,
        Texture,
        Animation
    }
    public enum ShardTypes
    {
        None,
        Bisect,
        Random
    }
    public enum SubSystemBehaviour
    {
        None,
        OnHit,
        OnDeath
    }
    public enum LoopType
    {
        Inherit,
        None
    }
    public enum KeyFrameType
    {
        Refrence,
        Sound
    }
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Nightmare
    }
    public enum GameMode
    {
        Normal,
        Arcade
    }
    public enum UnlockTier
    {
        None,
        Tier1,
        Tier2,
        Tier3
    }

    // StringStore Enums
    public enum NoteStyle
    {
        Letter,
        Note,
        Page
    }
    public enum StringStoreType
    {
        Bubble,
        Note,
        Custom
    }
}
