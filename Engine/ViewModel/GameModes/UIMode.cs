using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class UIMode : BaseNotificationClass
    {

        public MODE Mode { get; set; }
        public GameSession Game { get; set; }
        public UIMode OldMode;

        virtual public bool ShowAddCardToLot => false;
        virtual public bool ShowStartAuction => false;
        virtual public bool ShowBidOnLot => false;
        virtual public bool ShowPassBid => false;
        virtual public bool ShowContinue => false;

        public UIMode(GameSession game)
        {
            Game = game;
        }
        

        virtual public void UpdateFlags()
        {
            OnPropertyChanged(nameof(ShowAddCardToLot));
            OnPropertyChanged(nameof(ShowStartAuction));
            OnPropertyChanged(nameof(ShowBidOnLot));
            OnPropertyChanged(nameof(ShowPassBid));
            OnPropertyChanged(nameof(ShowContinue));
        }
    }
}
