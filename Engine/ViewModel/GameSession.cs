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

        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        public Deck ResourceDeck;

        public object Taxi;
        //public Supply Supply = new Supply();
        public int CurrentPhaseID = 0;
        public int ShipCapacity => Players.Count == 2 ? 7:5;
        public PHASE Phase;
        public int Step;

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        private Player _currentPlayer;

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        private Player _currentBidder;

        public Player CurrentBidder
        {
            get { return _currentBidder; }
            set
            {
                _currentBidder = value;
                OnPropertyChanged(nameof(CurrentBidder));
            }
        }

        private Player _highBidder;

        public Player HighBidder
        {
            get { return _highBidder; }
            set
            {
                _highBidder = value;
                OnPropertyChanged(nameof(HighBidder));
            }
        }

        private int _currentBid;

        public int CurrentBid
        {
            get { return _currentBid; }
            set
            {
                _currentBid = value;
                OnPropertyChanged(nameof(CurrentBid));
            }
        }


        private int _day;

        public int Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }



        public int CardsRemaining => ResourceDeck.CardsRemaining;
        public int CurrentPlayerID => Players.IndexOf(CurrentPlayer);
        public int CurrentBidderID => Players.IndexOf(CurrentBidder);

        private bool _showAddCardToLot;
        public bool ShowAddCardToLot
        {
            get { return _showAddCardToLot; }
            set
            {
                _showAddCardToLot = value;
                OnPropertyChanged(nameof(ShowAddCardToLot));
            }
        }
        private bool _showStartAuction;
        public bool ShowStartAuction
        {
            get { return _showStartAuction; }
            set
            {
                _showStartAuction = value;
                OnPropertyChanged(nameof(ShowStartAuction));
            }
        }
        private bool _showBidOnLot;
        public bool ShowBidOnLot
        {
            get { return _showBidOnLot; }
            set
            {
                _showBidOnLot = value;
                OnPropertyChanged(nameof(ShowBidOnLot));
            }
        }
        private bool _showPassBid;
        public bool ShowPassBid
        {
            get { return _showPassBid; }
            set
            {
                _showPassBid = value;
                OnPropertyChanged(nameof(ShowPassBid));
            }
        }
        private bool _showContinue;
        public bool ShowContinue
        {
            get { return _showContinue; }
            set
            {
                _showContinue = value;
                OnPropertyChanged(nameof(ShowContinue));
            }
        }

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

        public ObservableCollection<Card> Lot = new ObservableCollection<Card>();

        public GameSession()
        {
            CurrentMode = new UIMode(this);
            OutbreakCount = 0;

            ResourceDeck = GameData.BuildResourceDeck();

            Players.Add(new Player(this, "Cyrus", "Cyan"));
            Players.Add(new Player(this, "Will", "White"));
            Players.Add(new Player(this, "Piper", "Pink"));
            Players.Add(new Player(this, "Bryan", "Brown"));


            ResourceDeck.Shuffle();
            InitalSetup();
            NewDay();
        }

        private void InitalSetup()
        {
            ResourceDeck.Shuffle();
        }

        public void ResetFlags()
        {
            ShowAddCardToLot = false;
            ShowBidOnLot = false;
            ShowContinue = false;
            ShowStartAuction = false;
            ShowPassBid = false;
        }

        public void NewDay()
        {
            Day += 1;
            if (Day == 4)
            {
                EndGame();
                Phase = PHASE.POSTGAME;
                return;
            }
            foreach(Player p in Players)
            {
                p.Empty(ResourceDeck);
            }
            ShufflePlayerDeck();
            int lowest_money = Players.Min(p => p.Money);
            List<Player> Losing = Players.Where(p => p.Money == lowest_money).ToList();
            var random = new Random();
            int id = random.Next(Losing.Count());
            CurrentPlayer = Players[id];
            StartLotPhase();
        }

        public void StartLotPhase()
        {
            ResetFlags();
            ShowAddCardToLot = ResourceDeck.DrawPile.Count>0;
            ShowStartAuction = true;
            CurrentMode = new LotPhase(this, CurrentPlayer);
            if (ResourceDeck.DrawPile.Count == 0)
            {
                EndDay();
            } else {
                AddCardToLot();
            }
        }

        public void AddCardToLot()
        {
            Lot.Add(ResourceDeck.Draw());
            if (Lot.Count() == 3) { ShowAddCardToLot = false; }
            int emptiest_hold = Players.Min(p => p.Hold.Count());
            if (emptiest_hold + Lot.Count() == ShipCapacity)
            {
                ShowAddCardToLot = false;
            }
        }

        public void StartAuction()
        {
            ResetFlags();
            CurrentBid = 0;
            HighBidder = null;
            CurrentBidder = Players[(CurrentPlayerID + 1) % Players.Count()];
            SetupBid();
            ShowPassBid = true;
        }

        public void SetupBid()
        {
            ShowBidOnLot = false;
            if(CurrentBidder.Money > CurrentBid && (Lot.Count()+CurrentBidder.Hold.Count())<=ShipCapacity)
            {
                ShowBidOnLot = true;
            }
        }

        public void Bid()
        {
            CurrentBid += 1;
            HighBidder = CurrentBidder;
            NextBidder();
        }

        public void PassBid()
        {
            NextBidder();
        }

        public void NextBidder()
        {
            if(CurrentBidder==CurrentPlayer)
            {
                EndTurn();
                return;
            }
            CurrentBidder = Players[(CurrentBidderID + 1) % Players.Count()];
            SetupBid();
        }

        public void EndTurn()
        {
            if (HighBidder != null)
            {
                foreach (Card c in Lot)
                {
                    HighBidder.Load(c);
                }
                Lot.Clear();
                HighBidder.Money -= CurrentBid;
            } else
            {
                foreach (Card c in Lot)
                {
                    ResourceDeck.Discard(c);
                }
                Lot.Clear();
            }

            //Check End Day
            if (Players.Count(p => p.Hold.Count() < ShipCapacity) == 1)
            {
                ResetFlags();
                EndDay();
                return;

            }

            CurrentPlayer = Players[(CurrentPlayerID + 1) % Players.Count()];
            while (CurrentPlayer.Hold.Count() == ShipCapacity)
            {
                CurrentPlayer = Players[(CurrentPlayerID + 1) % Players.Count()];

            }
            StartLotPhase();
        }

        public void EndDay()
        {
            // Fill Last Ship
            Player lastPlayer = Players.First(p => p.Hold.Count() < ShipCapacity);
            while (lastPlayer.Hold.Count() < ShipCapacity && ResourceDeck.DrawPile.Count>0)
            {
                lastPlayer.Load(ResourceDeck.Draw());
            }
            Message = lastPlayer.Name + "'s hold filled.";
            Phase = PHASE.OVERALLVALUE;
            ShowContinue = true;
        }


        public void Continue()
        {
            switch (Phase)
            {
                case PHASE.OVERALLVALUE:
                    ScoreHoldValue();
                    Phase = PHASE.LEVELADVANCE;
                    break;
                case PHASE.LEVELADVANCE:
                    AdvanceTracks();
                    Phase = PHASE.LEVELWINNERS;
                    Step = 0;
                    break;
                case PHASE.LEVELWINNERS:
                    ScoreResource(Step);
                    Step += 1;
                    if (Step == 5) { Phase = PHASE.LEVELBONUS; }
                    break;
                case PHASE.LEVELBONUS:
                    Phase = PHASE.NEXTDAY;
                    ScoreBonuses();
                    break;
                case PHASE.NEXTDAY:
                    NewDay();
                    break;
                case PHASE.POSTGAME:
                    break;
                default:
                    break;
            }
        }

        public void ScoreHoldValue()
        {
            List<Player> playersByHoldTotal = Players.OrderByDescending(p => p.HoldTotal).ToList();
            List<Player> recipients = new List<Player>();
            int pool = 0;
            int current_best = -1;
            Message = "Payments for total Hold value:\n";
            for(int i = 0;  i < playersByHoldTotal.Count; i++)
            {
                if (playersByHoldTotal[i].HoldTotal == current_best) // Add this player to current recipients and add points to pool
                {
                    recipients.Add(playersByHoldTotal[i]);
                    pool += GameData.GetHoldReward(i, Players.Count());
                } else
                {
                    if (current_best != -1)
                    {
                        foreach(Player r in recipients)
                        {
                            int points = (int)pool / recipients.Count();
                            Message += r.Name + " gets " + points.ToString() + " Florins!\n";
                            r.Money += points;
                        }
                    }
                    recipients.Clear();
                    pool = GameData.GetHoldReward(i, Players.Count());
                    current_best = playersByHoldTotal[i].HoldTotal;
                    recipients.Add(playersByHoldTotal[i]);
                }
            }
            foreach (Player r in recipients)
            {
                int points = (int)pool / recipients.Count();
                Message += r.Name + " gets " + points.ToString() + " Florins!\n";
                r.Money += points;
            }
        }

        private void AdvanceTracks()
        {
            foreach(Player player in Players)
            {
                foreach (Card card in player.Hold)
                {
                    player.Advance(card.Resource);
                }
            }
            Message = "Commodity Levels Increased!";
        }


        public void ScoreResource(int step)
        {
            List<Player> playersByLevel = Players.OrderBy(p => p.Levels[step]).ToList();
            List<Player> recipients = new List<Player>();
            int pool = 0;
            int current_best = -1;
            Message = "Payments for " + Enum.GetNames(typeof(RESOURCE))[step].ToLower() + ":\n";
            for (int i = 0; i < playersByLevel.Count; i++)
            {
                if (playersByLevel[i].Levels[step] == current_best) // Add this player to current recipients and add points to pool
                {
                    recipients.Add(playersByLevel[i]);
                    pool += GameData.GetTrackReward(i, Players.Count());
                }
                else
                {
                    if (current_best != -1)
                    {
                        foreach (Player r in recipients)
                        {
                            int points = (int)pool / recipients.Count();
                            Message += r.Name + " gets " + points.ToString() + " Florins!\n";
                            r.Money += points;
                        }
                    }
                    recipients.Clear();
                    pool = GameData.GetTrackReward(i, Players.Count());
                    current_best = playersByLevel[i].Levels[step];
                    recipients.Add(playersByLevel[i]);
                }
            }
            foreach (Player r in recipients)
            {
                int points = (int)pool / recipients.Count();
                Message += r.Name + " gets " + points.ToString() + " Florins!\n";
                r.Money += points;
            }
        }
        
        private void ScoreBonuses()
        {
            bool anyAwards = false;
            Message = "Resource Bonuses Awarded:\n";
            foreach(Player player in Players)
            {
                for(int i = 0; i < 5; i++)
                {
                    int bonus = GameData.GetTrackBonus(player.Levels[i]);
                    if (bonus > 0)
                    {
                        player.Money += bonus;
                        Message = player.Name + " awarded " + bonus.ToString() + " Florins for " + Enum.GetNames(typeof(RESOURCE))[i]+"!\n";
                    }
                }
            }
            if (anyAwards == false)
            {
                Continue();
            }
        }

        private void ShufflePlayerDeck()
        {
            ResourceDeck.Shuffle();
            ResourceDeck.Burn(GameData.CardsToBurn(Players.Count()));
        }

        private void EndGame()
        {
            int mostMoney = Players.Max(p => p.Money);
            List<Player> winners = Players.Where(p => p.Money == mostMoney).ToList() ;
            if (winners.Count == 1)
            {
                Message = winners[0].Name + " wins the game!!!";
            } else
            {
                Message = "";
                for(int i = 0; i < winners.Count; i++)
                {
                    if (i != 0 && i != winners.Count - 1)
                    {
                        Message += ", " ;
                    }
                    if (i == winners.Count - 2)
                    {
                        Message += "and ";
                    }
                    Message += winners[i].Name;
                }
                Message += "share victory!!!";

            }

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


        public void Adjust(string name, string field, int v)
        {
            Player player = Players.FirstOrDefault(p => p.Name == name);
            if (player == null) { return; }
            switch (field)
            {
                case "Money":
                    player.Money += v;
                    break;
                case "Chili":
                    player.Levels[(int)RESOURCE.CHILI] -= v;
                    break;
                case "Indigo":
                    player.Levels[(int)RESOURCE.INDIGO] -= v;
                    break;
                case "Pepper":
                    player.Levels[(int)RESOURCE.PEPPER] -= v;
                    break;
                case "Saffron":
                    player.Levels[(int)RESOURCE.SAFFRON] -= v;
                    break;
                case "Tea":
                    player.Levels[(int)RESOURCE.TEA] -= v;
                    break;
                default:
                    break;
            }

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
