using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Entities.Components
{
    public class Position
    {
        public Vector2 OriginPosition { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
    }
}
