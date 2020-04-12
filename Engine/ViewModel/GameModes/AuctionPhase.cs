using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModel.GameModes
{

    class AuctionPhase : UIMode
    {
        public Player CurrentPlayer { get; set; }
        public Player CurrentBidder { get; set; }

        public override bool ShowBidOnLot => true;
        public override bool ShowPassBid => true;


        public AuctionPhase(GameSession game, Player player) : base(game)
        {
            CurrentPlayer = player;
            UpdateFlags();
        }

        public override void UpdateFlags()
        {
            base.UpdateFlags();
            OnPropertyChanged(nameof(ShowBidOnLot));
            OnPropertyChanged(nameof(ShowPassBid));
        }


    }
}
