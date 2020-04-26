using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SelectVillage : UIMode
    {

        public SelectVillage(GameSession game, ObservableCollection<Region> villages) : base(game)
        {
            DeselectRegions();
            foreach(Region village in villages)
            {
                village.Selectable = true;
            }
        }
    }
}
