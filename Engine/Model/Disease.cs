using System;
using Engine.Utils;

namespace Engine
{
    public class Disease : BaseNotificationClass
    {
        public ELEMENT Color { get; set; }
        private bool _cured;

        public bool Eradicated => Cured && CubesInSupply == TotalCubes;

        public bool Cured
        {
            get { return _cured; }
            set
            {
                _cured = value;
                OnPropertyChanged(nameof(Cured));
                OnPropertyChanged(nameof(Eradicated));
            }
        }

        private int _cubesInSupply;

        public int CubesInSupply
        {
            get { return _cubesInSupply; }
            set
            {
                _cubesInSupply = value;
                OnPropertyChanged(nameof(CubesInSupply));
                OnPropertyChanged(nameof(Eradicated));
            }
        }

        private int _totalCubes;

        public int TotalCubes
        {
            get { return _totalCubes; }
            set { _totalCubes = value; }
        }


        public Disease(ELEMENT color)
        {
            Color = color;
            TotalCubes = 24;
            CubesInSupply = 24;
            Cured = false;
        }

        internal void Cure()
        {
            Cured = true;
        }
    }
}