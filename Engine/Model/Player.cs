using Engine.Model;
using Engine.Utils;
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

        private int _pid;

        public int PID
        {
            get { return _pid; }
            set
            {
                _pid = value;
                OnPropertyChanged(nameof(PID));
            }
        }


        public Player(GameSession game, int pid, string name)
        {
            PID = pid;
            Game = game;
            Name = name;
        }
    }
}
