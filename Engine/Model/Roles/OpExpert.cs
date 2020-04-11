using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model.Roles
{
    class OpExpert : Player
    {

        public OpExpert(GameSession game, string name, string color, City location) : base(game, name, color, location) { }

        override public void Build(STRUCTURE kind, City target)
        {
            target.Build(kind);
        }
    }
}
