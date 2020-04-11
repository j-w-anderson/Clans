using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model
{
    public class Deck : BaseNotificationClass
    {
        static public Random rnd = new Random();

        public ObservableCollection<Card> DrawPile { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Card> DiscardPile { get; set; } = new ObservableCollection<Card>();

        public Deck(ObservableCollection<Card> drawPile = null)
        {
            if (drawPile != null)
            {
                DrawPile = drawPile;
            }
        }

        public Card Draw()
        {
            if (DrawPile.Count() == 0)
            {
                return null;
            }
            Card card = DrawPile[0];
            DrawPile.RemoveAt(0);
            return card;
        }

        public Card DrawBottom()
        {
            Card card = DrawPile.Last();
            DrawPile.Remove(card);
            return card;
        }

        public void Discard(Card card)
        {
            DiscardPile.Insert(0, card);
        }

        public void Add(Card card)
        {
            DrawPile.Add(card);
        }

        public void Adds(ObservableCollection<Card> cards)
        {
            foreach (Card card in cards)
            {
                DrawPile.Add(card);
            }
        }

        public enum SHUFFLEMODE
        {
            FULL,
            DISCARDONLY,
            EPIDEMIC
        }

        public void Shuffle()
        {

                    DrawPile.Concat(DiscardPile);
                    DiscardPile.Clear();
                    DrawPile = Shuffler.Shuffle(DrawPile, rnd).ToObservableCollection<Card>();
        }
    }
}
