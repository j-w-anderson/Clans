using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SelectOrigin : UIMode
    {
        public SelectOrigin(GameSession game) : base(game)
        {
            Name = "SelectOrigin";
            Game = game;
            foreach(Region region in Game.Regions)
            {
                region.Selectable = region.IsValidOrigin();
            }
        }
    }
}
