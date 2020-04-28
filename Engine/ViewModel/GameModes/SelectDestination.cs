using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SelectDestination : UIMode
    {

        public SelectDestination(GameSession game, Region origin) : base(game)
        {
            Name = "SelectDistination";
            DeselectRegions();
            foreach (Region region in origin.GetDestinations())
            {
                region.Selectable = true;
            }
        }
    }
}
