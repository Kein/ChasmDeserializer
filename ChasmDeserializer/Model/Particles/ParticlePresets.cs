using System.IO;
using ChasmDeserializer.Extensions;
using ChasmDeserializer.Interfaces;
using Microsoft.Xna.Framework;

namespace ChasmDeserializer.Model.Particles
{
    public class ParticlePresets : IBinarySaveLoad
    {
        public string Name { get; set; }
        public float ReleaseRate { get; set; }
        public float Delay { get; set; }
        public float Duration { get; set; }
        public float Velocity { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float VelocityRandom { get; set; }
        public float Direction { get; set; }
        public float DirectionSpread { get; set; }
        public Rectangle Box { get; set; }
        public int ParticleCount { get; set; }
        public float ParticleCountRandom { get; set; }
        public DirectionTypes DirectionType { get; set; }
        public EmitterTypes EmitterType { get; set; }
        public float ReleaseDistance { get; set; }
        public float ReleaseDistanceRandom { get; set; }
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public float ColorRandom { get; set; }
        public ColorModes ColorMode { get; set; }
        public ParticleTypes ParticleType { get; set; }
        public float ParticleMaxLife { get; set; }
        public float ParticleLifeRandom { get; set; }
        public float Size { get; set; }
        public float SizeRandom { get; set; }
        public Curve SizeOverLife { get; set; }
        public Curve OpacityOverlife { get; set; }
        public Curve ColorOverlife { get; set; }
        public float Opacity { get; set; }
        public float OpacityRandom { get; set; }
        public string AtlasKey { get; set; }
        public float Rotation { get; set; }
        public float RotationRandom { get; set; }
        public float RotationSpeed { get; set; }
        public float RotationSpeedRandom { get; set; }
        public float Gravity { get; set; }
        public float AirResistance { get; set; }
        public bool IsCollidable { get; set; }
        public string SubSystemKey { get; set; }
        public SubSystemBehaviour SubBehaviour { get; set; }
        public float Resitution { get; set; }
        public Vector2 Offset { get; set; }
        public ShardTypes ShardType { get; set; }
        public Rectangle SubRect { get; set; }
        public bool UseCustomDirection { get; set; }
        public LoopType LoopType { get; set; }

        public ParticlePresets()
        {
            this.SizeOverLife = new Curve();
            this.OpacityOverlife = new Curve();
            this.ColorOverlife = new Curve();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(this.Name.NullCheck());
            writer.Write(this.ReleaseRate);
            writer.Write(this.Delay);
            writer.Write(this.Duration);
            writer.Write(this.Velocity);
            writer.Write(this.VelocityX);
            writer.Write(this.VelocityY);
            writer.Write(this.VelocityRandom);
            writer.Write(this.Direction);
            writer.Write(this.DirectionSpread);
            writer.Write(this.Box);
            writer.Write(this.ParticleCount);
            writer.Write(this.ParticleCountRandom);
            writer.Write((int)this.DirectionType);
            writer.Write((int)this.EmitterType);
            writer.Write(this.ReleaseDistance);
            writer.Write(this.ReleaseDistanceRandom);
            writer.Write(this.StartColor);
            writer.Write(this.EndColor);
            writer.Write(this.ColorRandom);
            writer.Write((int)this.ColorMode);
            writer.Write((int)this.ParticleType);
            writer.Write(this.ParticleMaxLife);
            writer.Write(this.ParticleLifeRandom);
            writer.Write(this.Size);
            writer.Write(this.SizeRandom);
            writer.Write(this.SizeOverLife);
            writer.Write(this.OpacityOverlife);
            writer.Write(this.ColorOverlife);
            writer.Write(this.Opacity);
            writer.Write(this.OpacityRandom);
            writer.Write(this.AtlasKey.NullCheck());
            writer.Write(this.Rotation);
            writer.Write(this.RotationRandom);
            writer.Write(this.RotationSpeed);
            writer.Write(this.RotationSpeedRandom);
            writer.Write(this.Gravity);
            writer.Write(this.AirResistance);
            writer.Write(this.IsCollidable);
            writer.Write(this.SubSystemKey.NullCheck());
            writer.Write((int)this.SubBehaviour);
            writer.Write(this.Resitution);
            writer.Write(this.Offset);
            writer.Write((int)this.ShardType);
            writer.Write(this.SubRect);
            writer.Write(this.UseCustomDirection);
            writer.Write((int)this.LoopType);
        }

        public void Load(BinaryReader reader)
        {
            this.Name = reader.ReadString();
            this.ReleaseRate = reader.ReadSingle();
            this.Delay = reader.ReadSingle();
            this.Duration = reader.ReadSingle();
            this.Velocity = reader.ReadSingle();
            this.VelocityX = reader.ReadSingle();
            this.VelocityY = reader.ReadSingle();
            this.VelocityRandom = reader.ReadSingle();
            this.Direction = reader.ReadSingle();
            this.DirectionSpread = reader.ReadSingle();
            this.Box = reader.ReadRectangle();
            this.ParticleCount = reader.ReadInt32();
            this.ParticleCountRandom = reader.ReadSingle();
            this.DirectionType = (DirectionTypes)reader.ReadInt32();
            this.EmitterType = (EmitterTypes)reader.ReadInt32();
            this.ReleaseDistance = reader.ReadSingle();
            this.ReleaseDistanceRandom = reader.ReadSingle();
            this.StartColor = reader.ReadColor();
            this.EndColor = reader.ReadColor();
            this.ColorRandom = reader.ReadSingle();
            this.ColorMode = (ColorModes)reader.ReadInt32();
            this.ParticleType = (ParticleTypes)reader.ReadInt32();
            this.ParticleMaxLife = reader.ReadSingle();
            this.ParticleLifeRandom = reader.ReadSingle();
            this.Size = reader.ReadSingle();
            this.SizeRandom = reader.ReadSingle();
            this.SizeOverLife = reader.ReadCurve();
            this.OpacityOverlife = reader.ReadCurve();
            this.ColorOverlife = reader.ReadCurve();
            this.Opacity = reader.ReadSingle();
            this.OpacityRandom = reader.ReadSingle();
            this.AtlasKey = reader.ReadString();
            this.Rotation = reader.ReadSingle();
            this.RotationRandom = reader.ReadSingle();
            this.RotationSpeed = reader.ReadSingle();
            this.RotationSpeedRandom = reader.ReadSingle();
            this.Gravity = reader.ReadSingle();
            this.AirResistance = reader.ReadSingle();
            this.IsCollidable = reader.ReadBoolean();
            this.SubSystemKey = reader.ReadString();
            this.SubBehaviour = (SubSystemBehaviour)reader.ReadInt32();
            this.Resitution = reader.ReadSingle();
            this.Offset = reader.ReadVector2();
            this.ShardType = (ShardTypes)reader.ReadInt32();
            this.SubRect = reader.ReadRectangle();
            this.UseCustomDirection = reader.ReadBoolean();
            this.LoopType = (LoopType)reader.ReadInt32();
        }
     }
}
