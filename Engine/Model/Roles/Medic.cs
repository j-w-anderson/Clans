using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model.Roles
{
    public class Medic : Player
    {
        public Medic(GameSession game, string name, string color, City location) : base(game, name, color, location)
        {
            CardsToCure -= 1;
        }
    }
}
