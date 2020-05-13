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


        public double X => Coords.X;
        public double Y => Coords.Y;


        // For some reason, I couldn't get xaml to bind correctly to Huts[0]
        // This next code is my quick and dirty workaround; perhaps someday 
        // I'll be back to clean it up.
        public int Huts0 => Huts[0];
        public int Huts1 => Huts[1];
        public int Huts2 => Huts[2];
        public int Huts3 => Huts[3];
        public int Huts4 => Huts[4];

        public bool Huts0p => Huts[0] > 0;
        public bool Huts1p => Huts[1] > 0;
        public bool Huts2p => Huts[2] > 0;
        public bool Huts3p => Huts[3] > 0;
        public bool Huts4p => Huts[4] > 0;

        private void OnHutChange()
        {
            OnPropertyChanged(nameof(Huts0));
            OnPropertyChanged(nameof(Huts1));
            OnPropertyChanged(nameof(Huts2));
            OnPropertyChanged(nameof(Huts3));
            OnPropertyChanged(nameof(Huts4));
            OnPropertyChanged(nameof(Huts0p));
            OnPropertyChanged(nameof(Huts1p));
            OnPropertyChanged(nameof(Huts2p));
            OnPropertyChanged(nameof(Huts3p));
            OnPropertyChanged(nameof(Huts4p));
        }
        // End Q&D


        private bool _village=false;
        public bool Village
        {
            get { return _village; }
            set { _village = value; }
        }

        public bool Empty => Huts.Max() == 0;

        private bool _selectable;
        public bool Selectable
        {
            get { return _selectable; }
            set
            {
                _selectable = value;
                OnPropertyChanged(nameof(Selectable));
            }
        }


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
            OnHutChange();
        }

        public void AddHuts(List<int> huts)
        {
            Huts = (from i in Enumerable.Range(0,5)
                     select Huts.ElementAt(i) + huts.ElementAt(i)).ToObservableCollection<int>();
            OnPropertyChanged(nameof(Empty));
            OnHutChange();
        }

        public void MoveTo(Region region)
        {
            region.AddHuts(Huts.ToList<int>());
            Huts = new ObservableCollection<int>{ 0,0,0,0,0};
            OnPropertyChanged(nameof(Empty));
            OnHutChange();
        }

        public bool IsValidOrigin()
        {
            // Valid origin regions are
            // 1) Not empty
            // 2) Not already villages
            // 3a) Have fewer than 7 huts, or
            // 3b) Have the same number or fewer huts than a neighbor
            if(Empty || Village) { return false; }
            if (Huts.Sum() < 7 || Huts.Sum() <= Adj.Max(r => r.Huts.Sum())) { return true; }
            return false;
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

        public List<int> GetScores(TERRAIN boon, TERRAIN barren, int bonus)
        {
            List<int> points = new List<int> { 0, 0, 0, 0, 0 };
            Village = true;
            if (barren == Terrain) { return points; }

            // Clans battle if all are present
            if (Huts.Min() > 0)
            {
                Huts = Huts.Select(h => h == 1 ? 0 : h).ToObservableCollection<int>();
                OnHutChange();
            }
            // Remaining clans score
            for(int cid = 0; cid < 5; cid++)
            {
                if (Huts[cid] > 0) { points[cid] = Huts.Sum() + bonus; }
            }

            return points;

        }

    }
}
