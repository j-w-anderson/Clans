using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model.Roles
{
    public class Scientist : Player
    {
        public Scientist(GameSession game, string name, string color, City location) : base(game, name, color, location) { }


        override public void Cure(ELEMENT color)
        {
            Game.Cure(color);
            DiscardCityCards(Game.PlayerDeck, color, 4);
        }
    }
}
