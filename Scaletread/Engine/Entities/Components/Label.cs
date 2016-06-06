using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Entities.Components
{
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
}
