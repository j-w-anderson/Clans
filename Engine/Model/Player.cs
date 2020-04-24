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
        public Clan HiddenClan { get; set; }
        public string Name { get; set; }
        
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

        private int _points = 0;
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
            }
        }
                
       
        public Player(GameSession game, string name)
        {
            Game = game;
            Name = name;
        }


        
        public void EndMainPhase()
        {
            Game.EndTurn();
        }
        
        
        
        
    }
}
