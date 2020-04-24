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

            Players_IC.ItemsSource = gameSession.Players;
            //Lot_IC.ItemsSource = gameSession.Lot;
            //Discards_IC.ItemsSource = gameSession.ResourceDeck.DiscardPile;

            gameSession.StartGame();
            //FundedEventSelection_IC.ItemsSource = gameSession.AvailableEvents;
        }
        
        private void Money_Up(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            string name = target.Tag.ToString();
            gameSession.Adjust(name, "Money", 1);
        }

        private void Chili_Up(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            string name = target.Tag.ToString();
            gameSession.Adjust(name, "Chili", 1);
        }
        private void Indigo_Up(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            string name = target.Tag.ToString();
            gameSession.Adjust(name, "Indigo", 1);
        }
        private void Pepper_Up(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            string name = target.Tag.ToString();
            gameSession.Adjust(name, "Pepper", 1);
        }
        private void Saffron_Up(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            string name = target.Tag.ToString();
            gameSession.Adjust(name, "Saffron", 1);
        }
        private void Tea_Up(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            string name = target.Tag.ToString();
            gameSession.Adjust(name, "Tea", 1);
        }
        

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Continue();
        }
    }
}
