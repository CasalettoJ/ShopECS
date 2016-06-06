using Scaletread.Engine.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Entities
{
    public class Creature
    {
        public Guid Id;
        public int Money;
        public double MaxHealth;
        public double Health;
        public int BaseVelocity;
        public int Velocity;
        public Display DisplayInfo;
        public Position PositionInfo;
        public MovementType MovementType;
    }
}
