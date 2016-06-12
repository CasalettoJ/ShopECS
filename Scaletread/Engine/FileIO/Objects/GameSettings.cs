using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.FileIO.Objects
{
    public class GameSettings
    {
        public Vector2 Resolution { get; set; }
        public bool Vsync { get; set; }
        public bool Borderless { get; set; }
        public bool HasChanges { get; set; }
    }
}
