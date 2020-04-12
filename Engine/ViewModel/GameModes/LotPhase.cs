using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModel.GameModes
{

    class LotPhase : UIMode
    {
        public Player CurrentPlayer { get; set; }

        public override bool ShowAddCardToLot => Game.Lot.Count() < 3;
        public override bool ShowStartAuction => true;


        public LotPhase(GameSession game, Player player) : base(game)
        {
            CurrentPlayer = player;
            UpdateFlags();
        }

        public override void UpdateFlags()
        {
            base.UpdateFlags();
            OnPropertyChanged(nameof(ShowAddCardToLot));
        }

        
    }
}
