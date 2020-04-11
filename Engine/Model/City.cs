using Engine;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Engine
{
    public class City : BaseNotificationClass
    {
        public Point Coords { get; set; }
        public string Name { get; set; }
        public ELEMENT Color { get; set; }
        public ELEMENT TrueColor { get; }
        public REGION Region { get; set; }
        public List<Player> Pawns = new List<Player>();
        public ObservableCollection<ItemQuantity> Items { get; set; } = new ObservableCollection<ItemQuantity>();
        public List<City> Adjacent { get; private set; } = new List<City>();
        public double X { get; set; }
        public double Y { get; set; }

        private STATUS _status;

        public STATUS Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


        private bool _researchStation;

        public bool ResearchStation
        {
            get { return _researchStation; }
            set
            {
                _researchStation = value;
                OnPropertyChanged(nameof(ResearchStation));
            }
        }


        public bool MilitaryBase { get; set; } = false;
        public bool VaccineFactory { get; set; } = false;

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


        public City(string name, ELEMENT color, REGION region, Point coords)
        {
            Name = name;
            Color = color;
            TrueColor = color;
            Region = region;
            Coords = coords;
            X = coords.X - 24;
            Y = coords.Y - 24;
            Status = STATUS.NORMAL;
        }

        public void AddAdjacent(City adj)
        {
            Adjacent.Add(adj);
            if (Adjacent.Count > 6)
            {
                int a = 0;
            }
        }

        public bool AddCubes(int count)
        {
            // Shorthand if we're adding cubes of this city's color
            return AddCubes(count, Color);
        }

        public bool AddCubes(int count, ELEMENT color)
        {
            // Add cubes and return true if outbreak occurs
            ItemQuantity cubes = Items.FirstOrDefault(i => i.Element == color);
            if (cubes == null)
            {
                cubes = new ItemQuantity(color, count);
                Items.Add(cubes);
            }
            else
            {
                cubes.Add(count);
            }
            if (cubes.Count > 3)
            {
                cubes.Count = 3;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveOne(Disease target)
        {
            ItemQuantity cubes = Items.FirstOrDefault(i => i.Element == target.Color);
            if (cubes.Count > 0)
            {
                cubes.Count -= 1;
            }
        }
        public void RemoveAll(Disease target)
        {
            ItemQuantity cubes = Items.FirstOrDefault(i => i.Element == target.Color);
            cubes.Count = 0;
        }

        public void AddQuarantine()
        {
            Items.Add(new ItemQuantity(ELEMENT.QUARANTINE));
        }

        public void IncreaseTreatLevel()
        {
            if (Status != STATUS.FALLEN)
            {
                Status = (STATUS)((int)Status + 1);
                if (Status == STATUS.RIOTING2)
                {
                    ResearchStation = false;
                }
            }
        }

        public void Build(STRUCTURE kind)
        {
            switch (kind)
            {
                case STRUCTURE.STATION:
                    ResearchStation = true;
                    break;
                case STRUCTURE.BASE:
                    MilitaryBase = true;
                    break;
                case STRUCTURE.FACTORY:
                    VaccineFactory = true;
                    break;
                default:
                    break;
            }
        }
    }
}
