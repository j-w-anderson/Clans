using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Engine.Model
{
    public class Region : BaseNotificationClass
    {

        public Point Coords { get; set; }
        public int RID { get; set; }
        public TERRAIN Terrain { get; set; }
        public int GID { get; set;  }
        public List<Region> Adj { get; set; } = new List<Region>();
        public ObservableCollection<int> Huts = new ObservableCollection<int> { 0, 0, 0, 0, 0 };

        private bool _village=false;

        public bool Village
        {
            get { return _village; }
            set { _village = value; }
        }

        public bool Empty => Huts.Max() == 0;

        public Region(int rid, int gid, Point coords, TERRAIN terrain)
        {
            Coords = coords;
            RID = rid;
            GID = gid;
            Terrain = terrain;
        }

        public void AddAdj(Region region)
        {
            Adj.Add(region);
        }

        public bool VillageCheck()
        {
            if (Empty) { return false; }
            return Adj.All(r => r.Empty);
        }

        public void AddHut(Clan clan)
        {
            Huts[clan.Clan_id] += 1;
            OnPropertyChanged(nameof(Empty));
        }

        public void AddHuts(List<int> huts)
        {
            Huts = (from i in Enumerable.Range(0,5)
                     select Huts.ElementAt(i) + huts.ElementAt(i)).ToObservableCollection<int>();
            OnPropertyChanged(nameof(Empty));
        }

        public void MoveTo(Region region)
        {
            region.AddHuts(Huts.ToList<int>());
            Huts = new ObservableCollection<int>{ 0,0,0,0,0};
            OnPropertyChanged(nameof(Empty));
        }

        public ObservableCollection<Region> GetDestinations()
        {
            // Get all possible destinations from this region
            // 1) Can't move to empty regions
            // 2) Can't move if 7 or more huts unless destination has same or more
            ObservableCollection<Region> destinations = new ObservableCollection<Region>();
            foreach(Region region in Adj)
            {
                if (region.Empty) { continue; }
                if (Huts.Sum() < 7 || Huts.Sum() <= region.Huts.Sum())
                {
                    destinations.Add(region);
                }
            }
            return destinations;
        }
    }
}
