using Engine.Model;
using Engine.Model.Roles;
using Engine.Utils;
using Engine.ViewModel.GameModes;
using Microsoft.Win32;
using PandemicLegacy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    [Serializable]
    public class GameSession : BaseNotificationClass
    {

        public ObservableCollection<City> Cities;
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        public ObservableCollection<Path> Paths { get; set; }
        public ObservableCollection<Disease> Diseases { get; set; } = new ObservableCollection<Disease>();
        public ObservableCollection<EventCard> AvailableEvents { get; set; }
        public Deck PlayerDeck;
        public Deck InfectionDeck;
        public LegacyDeck Legacy = new LegacyDeck();

        public object Taxi;
        //public Supply Supply = new Supply();
        public int EpidemicCount = 4;
        public int GovFunding = 2;
        public int CurrentPhaseID = 0;
        public int InfectShield = 0;

        public int CurrentPlayerID => Players.IndexOf(CurrentPlayer);

        private int _outbreakCount;

        public int OutbreakCount
        {
            get { return _outbreakCount; }
            set
            {
                _outbreakCount = value;
                OnPropertyChanged(nameof(OutbreakCount));
            }
        }

        private int _infectionTrack;

        public int InfectionTrack
        {
            get { return _infectionTrack; }
            set
            {
                _infectionTrack = value;
                OnPropertyChanged(nameof(InfectionRate));
                OnPropertyChanged(nameof(InfectionTrack));
            }
        }

        public int InfectionRate => (int)((double)InfectionTrack * 0.4 + 2);

        public UIMode CurrentMode { get; set; }
        public Player CurrentPlayer { get; set; }


        public GameSession()
        {
            CurrentMode = new UIMode(this);
            OutbreakCount = 0;

            Cities = new ObservableCollection<City>(GameData.GetCities());
            Paths = GameData.GetPaths(Cities);
            PlayerDeck = GameData.BuildPlayerDeck(Cities);
            AvailableEvents = GameData.GetAvailableEvents();
            InfectionDeck = GameData.BuildInfectionDeck(Cities);

            City Start = Cities.First(c => c.Name == "Atlanta");
            Start.ResearchStation = true;
            Players.Add(new Generalist(this, "Cyrus", "Cyan", Start));
            Players.Add(new Player(this, "Will", "White", Start));
            Players.Add(new Player(this, "Piper", "Pink", Start));
            Players.Add(new Player(this, "Bryan", "Brown", Start));

            InitalSetup();

            CurrentPlayer = Players[0];
            CurrentPlayer.StartMainPhase();
            CurrentMode = new MainPhase(this, CurrentPlayer);

            Players[0].GridLoc = new int[2] { 1, 1 };
            Players[1].GridLoc = new int[2] { 1, 2 };
            Players[2].GridLoc = new int[2] { 2, 1 };
            Players[3].GridLoc = new int[2] { 2, 2 };

            Players[0].Role = ROLE.Colonel;
            Players[1].Role = ROLE.Dispatcher;
            Players[2].Role = ROLE.Generalist;
            Players[3].Role = ROLE.Medic;

            Legacy.CurrentTime = LegacyDeck.TIME.FebStart;
        }

        private void InitalSetup()
        {
            // Place initial cubes, deal player cards and shuffle decks
            MakeDiseases();
            PlaceInitialCubes();
            //AvailableEvents = GameData.GetAvailableEvents().Shuffle(new Random()).Take(4).ToObservableCollection<EventCard>();
            PlayerDeck.Adds(AvailableEvents.ToObservableCollection<Card>());
            DealPlayerCards();
            for (int i = 0; i < EpidemicCount; i++)
            {
                PlayerDeck.Add(GameData.GetEpidemicCard());
            }
            PlayerDeck.Shuffle();
        }

        public void MakeDiseases()
        {
            foreach (ELEMENT c in new ELEMENT[4] { ELEMENT.YELLOW, ELEMENT.RED, ELEMENT.BLUE, ELEMENT.BLACK })
            {
                Diseases.Add(new Disease(c));
            }
        }

        private void PlaceInitialCubes()
        {
            InfectionDeck.Shuffle();
            for (int i = 0; i < 9; i++)
            {
                InfectionCard icard = InfectionDeck.Flip() as InfectionCard;
                icard.City.AddCubes((i / 3) + 1);
                Diseases.First(d => icard.Color == d.Color).CubesInSupply -= (i / 3) + 1;
            }
        }

        private void DealPlayerCards()
        {
            PlayerDeck.Shuffle();
            foreach (Player p in Players)
            {
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
                p.Draw(PlayerDeck);
            }
        }

        private void ShufflePlayerDeck()
        {
            PlayerDeck.Shuffle(Deck.SHUFFLEMODE.EPIDEMIC);
        }


        public void Save(string fileName)
        {
            const int VERSION = 1;
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, VERSION);
                formatter.Serialize(stream, this);
            }
            catch
            {
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }

        public void DeselectAll()
        {
            foreach (City city in Cities)
            {
                city.Selectable = false;
            }
        }

        internal void Treat(City location, ELEMENT color, bool all = false)
        {
            Disease target = Diseases.FirstOrDefault(d => d.Color == color);
            if (target.Cured)
            {
                target.CubesInSupply += location.Items.FirstOrDefault(i => i.Element == color).Count;
                location.RemoveAll(target);
            }
            else
            {
                target.CubesInSupply += 1;
                location.RemoveOne(target);
            }
        }

        internal void Cure(ELEMENT color)
        {
            Diseases.First(d => color == d.Color).Cure();
        }

        internal void Infect(bool bottom = false)
        {
            InfectionCard card;
            if (bottom)
            {
                card = (InfectionCard)InfectionDeck.DrawBottom();
            }
            else
            {
                card = (InfectionCard)InfectionDeck.Draw();
            }
            City location = Cities.FirstOrDefault(c => c.Name == card.Name);
            if (location.AddCubes(1)) // Add cube and check for outbreak
            {
                TriggerOutbreak(location, location.Color, new List<City>());
            }
            else
            {
                Diseases[(int)location.Color].CubesInSupply -= 1;
            }
            InfectionDeck.Discard(card);
        }

        internal void TriggerOutbreak(City location, ELEMENT color, List<City> prev)
        {
            OutbreakCount += 1;
            location.IncreaseTreatLevel();
            prev.Add(location);
            foreach (City adj in location.Adjacent)
            {
                if (adj.AddCubes(1, color) && !prev.Contains(adj))
                {
                    TriggerOutbreak(adj, color, prev);
                }
                else
                {
                    Diseases[(int)color].CubesInSupply -= 1;
                }
            }
        }

        internal void TriggerEpidemic(Card epidemic)
        {
            // Epidemic steps: Increase, Infect, Intensify
            InfectionTrack += 1;
            Infect(true);
            InfectionDeck.Shuffle(Deck.SHUFFLEMODE.DISCARDONLY);

            //InfectionDeck.DiscardPile.Clear();
        }

        internal void EndTurn()
        {
            // Draw phase
            CurrentPlayer.Draw(PlayerDeck);
            CurrentPlayer.Draw(PlayerDeck);

            // Discard
            // TODO

            // Infection
            if (InfectShield > 0)
            {
                InfectShield -= 1;
            }
            else
            {
                for (int i = 0; i < InfectionRate; i++)
                {
                    Infect();
                }
            }
            int index = (Players.IndexOf(CurrentPlayer) + 1) % Players.Count;
            CurrentPlayer.Active = false;
            CurrentPlayer = Players[index];
            CurrentPlayer.StartMainPhase();
            CurrentMode = new MainPhase(this, CurrentPlayer);
            OnPropertyChanged(nameof(CurrentMode));
            OnPropertyChanged(nameof(CurrentPlayerID));
            //CurrentMode.SwitchPlayer(CurrentPlayer);
        }

        public void SaveGame()
        {
            // Save current Game to in an ascii format
            string text = "";
            text += DateTime.Now.ToString("F");




            // Open Save file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, text);

        }
    }
}
