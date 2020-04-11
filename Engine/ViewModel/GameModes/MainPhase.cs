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

        override public bool ShowMovement => Destinations.Any();
        override public bool ShowBuildResearchStation =>
            CurrentPlayer.Hand.Any(pc => pc.Name == CurrentPlayer.Location.Name) && !CurrentPlayer.Location.ResearchStation;

        override public bool ShowCollectVaccines => CurrentPlayer.Location.Items
                    .FirstOrDefault(i => i.Element == ELEMENT.VACCINE)?.Any ?? false;

        override public bool ShowCure => CurrentPlayer.CanCure();

        override public bool ShowTrade => CurrentPlayer.CanTrade();

        override public bool ShowEquip => false;// ((CurrentPlayer.Location.MilitaryBase && Game.Supply.HasMilitaryEquipment) ||
        //                          (CurrentPlayer.Location.ResearchStation && Game.Supply.HasMedicalEquipment));

        override public bool ShowKill => CurrentPlayer.CanKill();

        override public bool ShowQuarantine => CurrentPlayer.Location.Items.Any(it => it.Element == ELEMENT.QUARANTINE);

        private bool _showRoadblock;
        public bool ShowRoadblock
        {
            get { return _showRoadblock; }
            set
            {
                _showRoadblock = value;
                OnPropertyChanged(nameof(ShowRoadblock));
            }
        }
        private bool _showSabotage;
        public bool ShowSabotage
        {
            get { return _showSabotage; }
            set
            {
                _showSabotage = value;
                OnPropertyChanged(nameof(ShowSabotage));
            }
        }
        private bool _showSacrifice;
        public bool ShowSacrifice
        {
            get { return _showSacrifice; }
            set
            {
                _showSacrifice = value;
                OnPropertyChanged(nameof(ShowSacrifice));
            }
        }
        override public bool ShowTreat => CurrentPlayer.CanTreat();


        private bool _showVaccinate;
        public bool ShowVaccinate
        {
            get { return _showVaccinate; }
            set
            {
                _showVaccinate = value;
                OnPropertyChanged(nameof(ShowVaccinate));
            }
        }

        public List<City> Destinations;

        public MainPhase(GameSession game, Player player) : base(game)
        {
            CurrentPlayer = player;
            Destinations = CurrentPlayer.GetDestinations();
            UpdateFlags();
        }

        public override void SwitchPlayer(Player player)
        {
            CurrentPlayer = player;
            Destinations = CurrentPlayer.GetDestinations();
            UpdateFlags();
        }

        public override void UpdateFlags()
        {
            Game.DeselectAll();
            foreach (City city in Destinations)
            {
                city.Selectable = true;
            }

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

        override public void ChooseCity(string name)
        {
            City city = Game.Cities.FirstOrDefault(c => c.Name == name);
            if (city == null)
            {
                throw new ArgumentException();
            }
            CurrentPlayer.Move(city);
            Destinations = CurrentPlayer.GetDestinations();
            UpdateFlags();
        }

        public override void Treat()
        {
            // If only one kind of disease cubes
            List<ItemQuantity> treatable = CurrentPlayer.GetTreatableDiseases();
            if (treatable.Count() == 1)
            {
                CurrentPlayer.Treat(treatable[0].Element);
                CurrentPlayer.Actions -= 1;
            }
            else
            {
                throw new NotImplementedException();
            }

            UpdateFlags();
        }

        public override void Build(string tag)
        {
            CurrentPlayer.Build(STRUCTURE.STATION);
            UpdateFlags();
        }

        public override void Trade()
        {
            List<Trade> trades = CurrentPlayer.GetTrades();
            if (trades.Count() == 1)
            {
                trades[0].Execute();
            }
            else
            {
                // Pause mainphase to resolve trade choice
                trades[0].Execute();
            }
            CurrentPlayer.Actions -= 1;
            UpdateFlags();
        }

        public override void Cure()
        {
            List<ELEMENT> curable = CurrentPlayer.GetCurableDiseases();
            if (curable.Count() == 1)
            {
                CurrentPlayer.Cure(curable[0]);
                CurrentPlayer.Actions -= 1;
            }
            else
            {
                //throw new NotImplementedException();
            }
        }

        public override void Quarantine()
        {
            CurrentPlayer.Location.Items.Add(new ItemQuantity(ELEMENT.QUARANTINE));
            CurrentPlayer.Actions -= 1;
        }

        public override void PassTurn()
        {
            Game.EndTurn();
        }
    }
}
