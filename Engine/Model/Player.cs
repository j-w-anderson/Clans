using Engine.Model;
using Engine.Utils;
using PandemicLegacy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Engine
{
    public class Player : BaseNotificationClass
    {
        public GameSession Game { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }


        private int _money=40;
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }

        public int Money
        {
            get { return _money; }
            set
            {
                _money = value;
                OnPropertyChanged(nameof(Money));
            }
        }

        

        public ObservableCollection<Card> Hold { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<int> Levels { get; set; } = new ObservableCollection<int> { 7, 7, 7, 7,7 };

        public Player(GameSession game, string name, string color)
        {
            Game = game;
            Name = name;
            Color = color;
        }

        virtual public void StartMainPhase()
        {
            Active = true;
        }

        public void EndMainPhase()
        {
            Game.EndTurn();
        }
        
        
        
        
    }
}
