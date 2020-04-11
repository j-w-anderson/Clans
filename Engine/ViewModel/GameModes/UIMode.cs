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
        Player HasCard;
        Card Target;

        virtual public bool ShowMovement => false;
        virtual public bool ShowBuildResearchStation => false;
        virtual public bool ShowCollectVaccines => false;
        virtual public bool ShowCure => false;
        virtual public bool ShowTrade => false;
        virtual public bool ShowEquip => false;
        virtual public bool ShowKill => false;
        virtual public bool ShowQuarantine => false;
        virtual public bool ShowTreat => false;
        virtual public bool ShowCard => false;
        virtual public bool ShowPass => true;

        public bool CanSelectPlayer = false;

        private bool _showSelectionWindow;

        public bool ShowSelectionWindow
        {
            get { return _showSelectionWindow; }
            set
            {
                _showSelectionWindow = value;
                OnPropertyChanged(nameof(ShowSelectionWindow));
            }
        }

        private bool _selectFundedEvent;

        public bool SelectFundedEvent
        {
            get { return _selectFundedEvent; }
            set
            {
                _selectFundedEvent = value;
                OnPropertyChanged(nameof(SelectFundedEvent));
            }
        }

        private bool _showActionModes;

        public bool ShowActionModes
        {
            get { return _showActionModes; }
            set
            {
                _showActionModes = value;
                OnPropertyChanged(nameof(_showActionModes));
            }
        }

        public UIMode(GameSession game)
        {
            Game = game;
        }

        public void PlayCard()
        {
            Game.CurrentMode = new SelectEventCard(Game, null);
            return;
        }


        virtual public void SelectCity(string name)
        {
            return;
        }

        public void SelectCard(string name)
        {
            OldMode = Game.CurrentMode;
            HasCard = Game.Players.FirstOrDefault(p => p.Hand.Count(c => c.Name == name) == 1);
            Target = HasCard.Hand.FirstOrDefault(c => c.Name == name);
            if (Target.HasEffect)
            {
                Target.Activate(Game, HasCard);
            }
            return;
        }

        public void FinishCard(Player holder, Card card)
        {
            holder.Discard(Game.PlayerDeck, card);
        }

        virtual public void Treat() { return; }
        virtual public void Build(string tag) { return; }
        virtual public void Trade() { return; }
        virtual public void Cure() { return; }
        virtual public void PassTurn() { return; }
        virtual public void Quarantine() { return; }
        virtual public void SwitchPlayer(Player p) { return; }

        virtual public void ChoosePlayer(string name) { return; }
        virtual public void ChooseCity(string name) { return; }

        virtual public void UpdateFlags() { }
    }
}
