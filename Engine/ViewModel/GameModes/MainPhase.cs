using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModel.GameModes
{

    class MainPhase : UIMode
    {
        public Player CurrentPlayer { get; set; }
        

        public MainPhase(GameSession game, Player player) : base(game)
        {
            CurrentPlayer = player;
            UpdateFlags();
        }

        public override void SwitchPlayer(Player player)
        {
            CurrentPlayer = player;
            UpdateFlags();
        }

        public override void UpdateFlags()
        {

            OnPropertyChanged(nameof(ShowMovement));
            OnPropertyChanged(nameof(ShowBuildResearchStation));
            OnPropertyChanged(nameof(ShowCollectVaccines));
            OnPropertyChanged(nameof(ShowCure));
            OnPropertyChanged(nameof(ShowTrade));
            OnPropertyChanged(nameof(ShowEquip));
            OnPropertyChanged(nameof(ShowKill));
            OnPropertyChanged(nameof(ShowQuarantine));
            OnPropertyChanged(nameof(ShowTreat));
            OnPropertyChanged(nameof(ShowCard));
            OnPropertyChanged(nameof(ShowPass));
            //OnPropertyChanged(nameof());
            //OnPropertyChanged(nameof());
        }

        
        public override void PassTurn()
        {
            Game.EndTurn();
        }
    }
}
