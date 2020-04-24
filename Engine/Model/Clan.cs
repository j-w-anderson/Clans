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
        public int Clan_id;

        private int _points=0;

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
        }
    }
}
