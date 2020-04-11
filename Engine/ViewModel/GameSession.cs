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
            
            ResourceDeck = GameData.BuildResourceDeck();
            
            Players.Add(new Player(this, "Cyrus", "Cyan"));
            Players.Add(new Player(this, "Will", "White"));
            Players.Add(new Player(this, "Piper", "Pink"));
            Players.Add(new Player(this, "Bryan", "Brown"));

            InitalSetup();

            CurrentPlayer = Players[0];
            CurrentPlayer.StartMainPhase();
            CurrentMode = new MainPhase(this, CurrentPlayer);
            
        }

        private void InitalSetup()
        { 
            ResourceDeck.Shuffle();
        }

        
        private void ShufflePlayerDeck()
        {
            ResourceDeck.Shuffle();
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

        internal void EndTurn()
        {
            // Draw phase

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
