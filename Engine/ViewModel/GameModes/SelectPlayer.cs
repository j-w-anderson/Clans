﻿using Engine.Utils;
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
            Game.DeselectAll();
        }


        override public void ChoosePlayer(string name)
        {
            Player chosen = Game.Players.FirstOrDefault(p => p.Name == name);
            SendPlayer(this, chosen);
        }
    }


    public class SelectCity : UIMode
    {
        public event CityHandler SendCity;
        public delegate void CityHandler(SelectCity sp, City c);

        public SelectCity(GameSession game) : base(game)
        {
            Game.DeselectAll();
            foreach (City city in Game.Cities)
            {
                city.Selectable = true;
            }
        }

        override public void ChooseCity(string name)
        {
            City chosen = Game.Cities.FirstOrDefault(p => p.Name == name);
            SendCity(this, chosen);
        }
    }
}