using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Card
    {
        public RESOURCE Resource { get; set; }
        public int Value { get; set; }
        public string Name => Resource.ToString() + " " + Value.ToString();


        public Card(RESOURCE resource,int value)
        {
            Resource = resource;
            Value = value;
        }

        virtual public void Activate(GameSession Game, Player Holder) { }
    }
}
