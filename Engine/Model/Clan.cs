using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model
{
    public class Clan: BaseNotificationClass
    {
        private int _clan_id;

        public int Clan_id
        {
            get { return _clan_id; }
            set {
                _clan_id = value;
                OnPropertyChanged(nameof(Clan_id));
            }
        }



        private int _points;

        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        public Clan(int clan_id)
        {
            Clan_id = clan_id;
            Points = clan_id;
        }
    }
}
