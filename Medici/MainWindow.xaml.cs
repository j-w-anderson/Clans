using Engine;
using Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameSession gameSession;

        public MainWindow()
        {
            InitializeComponent();

            gameSession = new GameSession();

            DataContext = gameSession;

            Regions_IC.ItemsSource = gameSession.Regions;
            Clans_IC.ItemsSource = gameSession.Clans;
            Players_IC.ItemsSource = gameSession.Players;
            //Lot_IC.ItemsSource = gameSession.Lot;
            //Discards_IC.ItemsSource = gameSession.ResourceDeck.DiscardPile;

            gameSession.StartGame(new Random());
            //FundedEventSelection_IC.ItemsSource = gameSession.AvailableEvents;
        }

        private void Select_Region_Click(object sender, RoutedEventArgs e)
        {
            Button S = sender as Button;
            int RID = (int)S.Tag;
            gameSession.OnSelectRegion(RID);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int cid = 0; cid < 5; cid++)
            {
                gameSession.Clans[cid].Points +=  10;
            }
        }
    }
}
