using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Entities
{
    #region Position
    public class Position
    {
        public Vector2 OriginPosition { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
    #endregion

    #region Movement
    public enum MovementType
    {
        INPUT,
        AI
    }

    public class Movement
    {
        public int BaseVelocity { get; set; }
        public int Velocity { get; set; }
        public MovementType MovementType { get; set; }
    }
    #endregion

    #region Label
    public enum WhenToShowLabel
    {
        ALWAYS,
        PLAYER_CLOSE,
        PLAYER_FAR
    }

    public class Label
    {
        public string Text;
        public float Scale;
        public Vector2 Displacement;
        public SpriteEffects SpriteEffect;
        public float Rotation;
        public WhenToShowLabel WhenToShow;
        public Color Color;
    }
    #endregion

    #region Display
    public enum DisplayLayer
    {
        BACKGROUND,
        FLOOR,
        FOREGROUND,
        SUPER
    }

    public class Display
    {
        public Rectangle SpriteSource;
        public Color Color;
        public float Scale;
        public Vector2 Origin;
        public SpriteEffects SpriteEffect;
        public float Rotation;
        public float Opacity;
        public DisplayLayer Layer;
    }
    #endregion

    #region Health
    public class Health
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
    }
    #endregion

    #region Wealth
    public class Wealth
    {
        public int Money { get; set; }
    }
    #endregion
}
