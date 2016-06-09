using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Components
{
    #region Component Boilerplate
    public enum Components : int
    {
        IS_PLAYER = 0,
        POSITION = 1,
        LABEL = 2,
        DISPLAY = 3
    }

    public class ECSContainer
    {
        public ECSContainer()
        {
            this.EntityCount = 0;
            this.Entities = new List<Entity>();
            this.Positions = new List<Position>();
            this.Labels = new List<Label>();
            this.Displays = new List<Display>();
        }

        // Entities
        public int EntityCount { get; private set; }
        public List<Entity> Entities { get; private set; }

        // Component Arrays
        public List<Position> Positions { get; private set; }
        public List<Label> Labels { get; private set; }
        public List<Display> Displays { get; private set; }

        public int CreateEntity(params Components[] flags)
        {
            this.Entities.Insert(this.EntityCount, new Entity(this.EntityCount, flags));
            return this.EntityCount++;
        }

        public void DestroyEntity(int entityId)
        {
            this.Entities.RemoveAt(entityId);
            this.Positions.RemoveAt(entityId);
            this.Labels.RemoveAt(entityId);
            this.Displays.RemoveAt(entityId);
            this.EntityCount -= 1;
        }


    }
    #endregion


    #region Components
    public class Position
    {
        public Vector2 OriginPosition { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
    }

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
        public Vector2 Origin;
        public SpriteEffects SpriteEffect;
        public float Rotation;
        public WhenToShowLabel WhenToShow;
    }

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
}
