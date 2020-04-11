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

namespace Medici
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
            //FundedEventSelection_IC.ItemsSource = gameSession.AvailableEvents;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.SelectCard((sender as Label).Content.ToString());
        }

        private void Player_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.ChoosePlayer((sender as Grid).Tag.ToString());
        }

        private void CityDot_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.ChooseCity((sender as Button).Tag.ToString());
        }

        private void Treat_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.Treat();
        }

        private void Trade_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.Trade();
        }
        private void Build_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.Build((sender as Button).Tag.ToString());
        }
        private void Cure_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.Cure();
        }
        private void Quarantine_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.Quarantine();
        }
        private void PassTurn_Click(object sender, RoutedEventArgs e)
        {
            gameSession.CurrentMode.PassTurn();
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
