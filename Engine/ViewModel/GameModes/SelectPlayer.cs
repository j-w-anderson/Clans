using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SelectPlayer : UIMode
    {
        public event PlayerHandler SendPlayer;
        public delegate void PlayerHandler(SelectPlayer sp, Player p);

        public SelectPlayer(GameSession game) : base(game)
        {
        }


        override public void ChoosePlayer(string name)
        {
            Player chosen = Game.Players.FirstOrDefault(p => p.Name == name);
            SendPlayer(this, chosen);
        }
    }


}
