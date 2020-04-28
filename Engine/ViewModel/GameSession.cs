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

        public ObservableCollection<Region> NewVillages { get; set; } = new ObservableCollection<Region>();

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

        private UIMode _uimode;

        public UIMode CurrentMode
        {
            get
            {
                return _uimode;
            }
            set
            {
                _uimode = value;
                OnPropertyChanged(nameof(UIMode));
            }
        }

        public ObservableCollection<Card> Lot = new ObservableCollection<Card>();

        public GameSession()
        {
            CurrentMode = new UIMode(this);

            Regions = GameData.GetRegions().ToObservableCollection<Region>();
            Clans = GameData.GetClans().ToObservableCollection<Clan>();


            Players.Add(new Player(this, "Cyrus"));
            Players.Add(new Player(this, "Will"));
            Players.Add(new Player(this, "Piper"));
            Players.Add(new Player(this, "Bryan"));

        }

        public void StartGame(Random rng)
        {
            DistributeHuts(rng);
            // Assign 1 clan to each player
            List<Clan> avail = Clans.ToList<Clan>();
            foreach(Player p in Players)
            {
                int chosen = rng.Next(avail.Count());
                p.HiddenClan = avail[chosen];
                avail.RemoveAt(chosen);
            }
            // Shuffle Player Order
            Players = Shuffler.Shuffle<Player>(Players,rng).ToObservableCollection<Player>();
            CurrentPlayer = Players.Last();
            NewTurn();
        }
        
        public void DistributeHuts(Random rng)
        {

            // Distribute Initial Huts
            List<Clan> avail = Clans.ToList<Clan>();
            for (int gid = 0; gid < 12; gid++)
            {
                avail = Clans.ToList<Clan>();
                for (int i = 0; i < 5; i++)
                {
                    int rid = gid * 5 + i;
                    int chosen = rng.Next(avail.Count());
                    Regions[rid].AddHut(avail[chosen]);
                    avail.RemoveAt(chosen);
                }
            }
        }
        
        
        public void ResetFlags()
        {
            ShowContinue = false;
        }

        public void NewTurn()
        {
            CurrentPlayer = Players[(CurrentPlayer_id+1) % Players.Count()];
            // Player to select Origin region
            CurrentMode = new SelectOrigin(this);
        }

        public void OnOrigin(Region origin)
        {
            Origin = origin;
            CurrentMode = new SelectDestination(this,Origin);
        }

        public void OnDestination(Region destination)
        {
            Origin.MoveTo(destination);
            NewVillages = FindNewVillages();
            ScoreNewVillages();
        }

        public void ScoreNewVillages()
        { 
            if (NewVillages.Count() == 0)
            {
                NewTurn();
                return;
            } else if(NewVillages.Count()==1)
            {
                // Maybe should be a mode so UI can pause during scoring...
                // probably the UI should listen for a scoring event and pause on its own?
                ScoreVillage(NewVillages[0]);
            } else
            {
                CurrentMode = new SelectVillage(this, NewVillages);
            }
        }

        public void ScoreVillage(Region village)
        {
            TERRAIN boon = GameData.GetBonusTerrain(Chips);
            TERRAIN barren = GameData.GetBarrenTerrain(Chips);
            int bonus = GameData.GetBonus(Chips);
            Chips -= 1;

            List<int> points = village.GetScores(boon,barren,bonus);
            for(int cid = 0; cid < 5; cid++)
            {
                Clans[cid].Points += points[cid];
            }
        }

        public void OnVillage(Region village)
        {
            NewVillages.Remove(village);
            ScoreVillage(village);
            if (Chips == 0)
            {
                EndGame();
            }
            else
            {
                ScoreNewVillages();
            }
        }

        public void OnScoreContinue()
        {
            if (NewVillages.Count() == 0)
            {
                NewTurn();
            } else
            {
                CurrentMode = new SelectVillage(this, NewVillages);
            }

        }
        


        public void EndTurn()
        {

            
            
        }
        



        public ObservableCollection<Region> FindNewVillages()
        {
            return Regions.Where(r => !r.Empty && !r.Village && r.VillageCheck()).ToObservableCollection<Region>();             
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
