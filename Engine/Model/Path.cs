using Engine.Utils;
using PandemicLegacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Engine
{
    public class Path : BaseNotificationClass
    {
        public City[] Cities { get; set; } = new City[2];
        private Point _midPoint;
        private bool _roadblock = false;

        public bool Selectable { get; set; } = false;

        public bool Roadblock
        {
            get { return _roadblock; }
            set
            {
                _roadblock = value;
                OnPropertyChanged(nameof(Roadblock));
                OnPropertyChanged(nameof(NoRoadblock));

            }
        }

        public bool NoRoadblock => !Roadblock;
        public Point MidPoint
        {
            get { return _midPoint; }
            set
            {
                _midPoint = value;
                OnPropertyChanged(nameof(X));
                OnPropertyChanged(nameof(Y));
            }
        }

        private double _rotation;

        public double Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                OnPropertyChanged(nameof(Rotation));
            }
        }


        public double X => MidPoint.X;
        public double Y => MidPoint.Y;



        public Path(City city1, City city2)
        {
            Cities[0] = city1;
            Cities[1] = city2;
            double X1 = city1.Coords.X;
            double X2 = city2.Coords.X;
            double Y1 = city1.Coords.Y;
            double Y2 = city2.Coords.Y;
            MidPoint = new Point((X1 + X2) / 2.0,
                                 (Y1 + Y2) / 2.0);
            Rotation = Math.Atan2(Y2 - Y1, X2 - X1) * 180 / Math.PI + 90;
        }
    }
}
