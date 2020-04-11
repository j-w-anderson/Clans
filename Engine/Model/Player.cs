using Engine.Model;
using Engine.Utils;
using PandemicLegacy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Engine
{
    public class Player : BaseNotificationClass
    {
        public GameSession Game { get; set; }
        public string Name { get; set; }
        public ROLE Role { get; set; }
        public bool Military { get; set; } = true;
        public string Color { get; set; }
        public int CardsToCure { get; set; }


        private City _location;
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }

        public City Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }


        private int _actions;

        public int Actions
        {
            get { return _actions; }
            set
            {
                _actions = value;
                OnPropertyChanged(nameof(Actions));
                if (Actions <= 0)
                {
                    EndMainPhase();
                }
            }
        }

        public int[] GridLoc { get; set; } = new int[2] { 0, 0 };

        public int GridRow => GridLoc[0];
        public int GridCol => GridLoc[1];


        public ObservableCollection<PlayerCard> Hand { get; set; } = new ObservableCollection<PlayerCard>();
        public int VaccineDoses { get; internal set; }

        public Player(GameSession game, string name, string color, City location)
        {
            Game = game;
            Name = name;
            Color = color;
            Location = location;
            CardsToCure = 5;
        }

        virtual public void StartMainPhase()
        {
            Active = true;
            Actions = 4;
        }

        public void EndMainPhase()
        {
            if (Actions != 0)
            {
                _actions = 0;
            }
            Game.EndTurn();
        }

        public List<City> GetDestinations()
        {
            // Compile a list of cities to which this player can travel directly

            if (Hand.Any(pc => pc.Name == Location.Name))
            {
                return Game.Cities.Where(pc => pc.Name != Location.Name).ToList<City>();
            }
            else
            {
                List<City> destinations = Location.Adjacent.ToList();
                destinations.AddRange(Hand.Where(pc => pc.GetType() == typeof(CityCard)).Select(cc => (cc as CityCard).City));
                return destinations;
            }
        }

        internal void Draw(Deck playerDeck)
        {
            PlayerCard draw = playerDeck.Draw() as PlayerCard;
            if (draw is EpidemicCard)
            {
                Discard(Game.PlayerDeck, draw);
                Game.TriggerEpidemic(draw);
            }
            else
            {
                Hand.Add(draw);
            }
        }

        internal bool HasCard(string name)
        {
            return Hand.Count(c => c.Name == name) > 0;
        }

        internal void Discard(Deck deck, Card card)
        {
            deck.Discard(card);
            Hand.Remove(card as PlayerCard);
        }

        internal void DiscardByName(Deck deck, string name)
        {
            PlayerCard card = Hand.FirstOrDefault(pc => pc.Name == name);
            if (card == null)
            {
                throw new ArgumentException();
            }
            Discard(deck, card);
        }

        internal void DiscardCityCards(Deck playerDeck, ELEMENT color, int n)
        {
            List<PlayerCard> discards = Hand.Where(cc => cc.Color == color).Take(n).ToList<PlayerCard>();
            foreach (PlayerCard card in discards)
            {
                Discard(playerDeck, card);
            }
        }

        internal bool CanCure()
        {
            if (!Location.ResearchStation)
            {
                return false;
            }
            foreach (ELEMENT c in new ELEMENT[4] { ELEMENT.RED, ELEMENT.BLACK, ELEMENT.BLUE, ELEMENT.YELLOW })
            {
                if (Hand.Count(pc => pc.Color == c) >= CardsToCure)
                {
                    return true;
                }
            }
            return false;
        }

        internal bool CanTrade()
        {
            // Find if self or any player at this location has this location's card
            List<Player> playersHere = Game.Players.Where(p => p.Location == Location).ToList<Player>();
            return playersHere.Count() > 1 && playersHere.Any(p => p.HasCard(Location.Name));
        }

        internal List<Trade> GetTrades()
        {
            // Return all legal Trade objects

            // Find card matching current loc
            List<Player> playersHere = Game.Players.Where(p => p.Location == Location).ToList<Player>();
            Player fromPlayer = playersHere.FirstOrDefault(p => p.HasCard(Location.Name));
            PlayerCard tradeCard = fromPlayer.Hand.FirstOrDefault(c => c.Name == Location.Name);
            List<Trade> trades = new List<Trade>();
            foreach (Player p in playersHere)
            {
                if (p != fromPlayer)
                {
                    trades.Add(new Trade(fromPlayer, p, tradeCard));
                }
            }
            return trades;

        }

        internal bool CanTreat()
        {
            return GetTreatableDiseases().Count > 0;
        }

        internal bool CanKill()
        {
            return false;
        }

        virtual public void Move(City city)
        {
            bool canDrive = false;
            bool canFly = false;
            bool canCharter = false;
            bool canShuttle = false;
            // Find cost to move
            if (Location.Adjacent.Any(c => c.Name == city.Name))
            {
                canDrive = true;
            }
            if (Hand.Any(card => card.Name == Location.Name))
            {
                canCharter = true;
            }
            if (Hand.Any(card => card.Name == city.Name))
            {
                canFly = true;
            }
            if (Location.ResearchStation && city.ResearchStation)
            {
                canShuttle = true;
            }

            if (canDrive || canShuttle)
            {
            }
            else if (canFly)
            {
                DiscardByName(Game.PlayerDeck, city.Name);
            }
            else if (canCharter)
            {
                DiscardByName(Game.PlayerDeck, Location.Name);
            }
            else
            {
                return;
            }
            Location = city;
            Actions -= 1;
            return;

        }

        internal List<ItemQuantity> GetTreatableDiseases()
        {
            List<ItemQuantity> treatable = Location.Items.Where(it => it.Element == ELEMENT.BLACK && it.Any
                                                                    || it.Element == ELEMENT.BLUE && it.Any
                                                                    || it.Element == ELEMENT.RED && it.Any
                                                                    || it.Element == ELEMENT.YELLOW && it.Any).ToList<ItemQuantity>();
            // TODO: Check if all cubes are treatable by char
            return treatable;
        }

        internal List<ELEMENT> GetCurableDiseases()
        {
            List<ELEMENT> curable = new List<ELEMENT>();

            foreach (ELEMENT c in new ELEMENT[4] { ELEMENT.RED, ELEMENT.BLACK, ELEMENT.BLUE, ELEMENT.YELLOW })
            {
                if (Hand.Count(pc => pc.Color == c) >= CardsToCure)
                {
                    curable.Add(c);
                }
            }
            return curable;
        }

        virtual public void Treat(ELEMENT target)
        {
            Game.Treat(Location, target);
        }

        virtual public void Cure(ELEMENT color)
        {
            Game.Cure(color);
            DiscardCityCards(Game.PlayerDeck, color, CardsToCure);
        }

        virtual public void Build(STRUCTURE kind)
        {
            Build(kind, Location);
        }


        virtual public void Build(STRUCTURE kind, City target)
        {
            target.Build(kind);
            DiscardByName(Game.PlayerDeck, Location.Name);
            Actions -= 1;
        }

        public void CardClick()
        {

        }
    }
}
