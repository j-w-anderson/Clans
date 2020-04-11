using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model.Roles
{
    public class Generalist : Player
    {
        public Generalist(GameSession game, string name, string color, City location) : base(game, name, color, location) { }

        public override void StartMainPhase()
        {
            base.StartMainPhase();
            Actions = 5;
        }

    }
}
