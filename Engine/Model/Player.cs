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
        public string Color { get; set; }


        public string Name { get; set; }
        


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

        public int HoldTotal => Hold.Count==0?0:Hold.Sum(c => c.Value);

        public Player(GameSession game, string name, string color)
        {
            Game = game;
            Name = name;
            Color = color;
        }

        virtual public void StartLotPhase()
        {
            Active = true;
        }


        public void Load(Card card)
        {
            Hold.Add(card);
            OnPropertyChanged(nameof(HoldTotal));
        }

        public void Empty(Deck deck)
        {
            foreach(Card c in Hold)
            {
                deck.Discard(c);   
            }
            Hold.Clear();
            OnPropertyChanged(nameof(HoldTotal));
        }

        public void Advance(RESOURCE resource)
        {
            if (resource != RESOURCE.GOLD)
            {
                Levels[(int)resource] = Math.Max(0, Levels[(int)resource] - 1);
            }
        }

        public void EndMainPhase()
        {
            Game.EndTurn();
        }
        
        
        
        
    }
}
