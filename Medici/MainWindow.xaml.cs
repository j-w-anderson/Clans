using Engine;
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
            //Lot_IC.ItemsSource = gameSession.Lot;
            //Discards_IC.ItemsSource = gameSession.ResourceDeck.DiscardPile;

            gameSession.StartGame(new Random());
            //FundedEventSelection_IC.ItemsSource = gameSession.AvailableEvents;
        }
        
    }
}
