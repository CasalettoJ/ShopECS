using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Entities
{
    public class Player
    {
        public Guid Id;
        public Wealth WealthInfo;
        public Health HealthInfo;
        public Movement MovementInfo;
        public Display DisplayInfo;
        public Position PositionInfo;
        public Label LabelInfo;
    }

    public class Creature
    {
        public Guid Id;
        public Health HealthInfo;
        public Movement MovementInfo;
        public Display DisplayInfo;
        public Position PositionInfo;
        public Label LabelInfo;
    }

    public class DisplayObject
    {
        public Guid Id;
        public Position PositionInfo;
        public Display DisplayInfo;
    }
}
