using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Components
{
    public class Entity
    {
        public Entity(int id, params Components[] flags)
        {
            this.Id = id;
            this.ComponentFlags = new BitArray(Enum.GetNames(typeof(Components)).Length);

            foreach(Components flag in flags)
            {
                this.ComponentFlags[(int)flag] = true;
            }
        }

        public int Id { get; }
        public BitArray ComponentFlags { get; } 
    }

    // Extension Methods take place of Bit Masking
    public static class EntityExtensions
    {
        public static bool HasComponents(this Entity e, params Components[] flags)
        {
            foreach (Components flag in flags)
            {
                if (!e.ComponentFlags[(int)flag])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool HasDrawableSprite(this Entity e)
        {
            return (e.ComponentFlags[(int)Components.DISPLAY] && e.ComponentFlags[(int)Components.POSITION]);
        }

        public static bool HasDrawableLabel(this Entity e)
        {
            return (e.ComponentFlags[(int)Components.LABEL] && e.ComponentFlags[(int)Components.POSITION]);
        }
    }
}
