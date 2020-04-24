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
        public ObservableCollection<Region> Regions { get; set; } = new ObservableCollection<Region>();
        public ObservableCollection<Clan> Clans { get; set; } = new ObservableCollection<Clan>();
        //public Supply Supply = new Supply();
        public int CurrentPhaseID = 0;
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

        public int CurrentPlayer_id => Players.IndexOf(CurrentPlayer);

        private int _chips=12;

        public int Chips
        {
            get { return _chips; }
            set
            {
                _chips = value;
                OnPropertyChanged(nameof(Chips));
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

        private Region _origin;

        public Region Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }


        public UIMode CurrentMode { get; set; }

        public ObservableCollection<Card> Lot = new ObservableCollection<Card>();

        public GameSession()
        {
            CurrentMode = new UIMode(this);

            Regions = GameData.GetRegions().ToObservableCollection<Region>();
            Clans = GameData.GetClans().ToObservableColletion<Clan>();


            Players.Add(new Player(this, "Cyrus"));
            Players.Add(new Player(this, "Will"));
            Players.Add(new Player(this, "Piper"));
            Players.Add(new Player(this, "Bryan"));

            StartGame(new Random());
        }

        public void StartGame(Random rng)
        {
            // Assign 1 clan to each player
            List<Clan> avail = Clans.ToList<Clan>();
            foreach(Player p in Players)
            {
                int chosen = rng.Next(avail.Count());
                p.HiddenClan = avail[chosen];
                avail.RemoveAt(chosen);
            }
            // Distribute Initial Huts
            for(int gid = 0; gid < 12; gid++)
            {
                avail = Clans.ToList<Clan>();
                for(int i = 0; i < 5; i++)
                {
                    int rid = gid * 5 + i;
                    int chosen = rng.Next(avail.Count());
                    Regions[rid].AddHut(avail[chosen]);

                }
            }
            // Shuffle Player Order
            Players = Shuffler.Shuffle<Player>(Players,rng).ToObservableCollection<Player>();
            CurrentPlayer = Players.Last();
            NewTurn();
        }
        
        public void ResetFlags()
        {
            ShowContinue = false;
        }

        public void NewTurn()
        {
            CurrentPlayer = Players[(CurrentPlayer_id+1) % Players.Count()];
            // Player to select Origin region
        }

       
        public void OnDestination(Region destination)
        {
            Origin.MoveTo(destination);
            ObservableCollection<Region> new_villages = FindNewVillages();

        }
        
        
        public void EndTurn()
        {

            
            
        }
        


        public void Continue()
        {
            switch (Phase)
            {
                case PHASE.OVERALLVALUE:
                    ScoreVillage();
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
                    NewTurn();
                    break;
                case PHASE.POSTGAME:
                    break;
                default:
                    break;
            }
        }


        public ObservableCollection<Region> FindNewVillages()
        {
            return Regions.Where(r => !r.Empty && !r.Village && r.VillageCheck()).ToObservableCollection<Region>();             
        }

        public void ScoreVillage(Region region)
        {

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
                            r.Points += points;
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
                r.Points += points;
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
                        player.Points += bonus;
                        Message = player.Name + " awarded " + bonus.ToString() + " Florins for " + Enum.GetNames(typeof(RESOURCE))[i]+"!\n";
                    }
                }
            }
            if (anyAwards == false)
            {
                Continue();
            }
        }
        

        private void EndGame()
        {
            int mostMoney = Players.Max(p => p.Points);
            List<Player> winners = Players.Where(p => p.Points == mostMoney).ToList() ;
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
                    player.Points += v;
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
