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

        public void Shuffle(SHUFFLEMODE mode = SHUFFLEMODE.FULL)
        {
            switch (mode)
            {
                case SHUFFLEMODE.FULL:
                    DrawPile.Concat(DiscardPile);
                    DiscardPile.Clear();
                    DrawPile = Shuffler.Shuffle(DrawPile, rnd).ToObservableCollection<Card>();
                    break;
                case SHUFFLEMODE.DISCARDONLY:

                    while (DiscardPile.Count() > 0)
                    {
                        int a = rnd.Next(DiscardPile.Count());
                        DrawPile.Insert(0, DiscardPile[a]);
                        DiscardPile.RemoveAt(a);
                    }
                    break;
                case SHUFFLEMODE.EPIDEMIC:
                    Deck[] piles = new Deck[5];
                    DrawPile.Concat(DiscardPile);
                    DiscardPile.Clear();
                    while (DrawPile.Count > 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (piles[i] == null)
                            {
                                piles[i] = new Deck();
                                piles[i].Add(new EpidemicCard());
                            }
                            if (DrawPile.Count > 0)
                            {
                                piles[i].Add(DrawPile[0]);
                                DrawPile.RemoveAt(0);
                            }
                        }
                    }
                    foreach (Deck pile in piles)
                    {
                        pile.Shuffle();
                    }
                    DrawPile = piles[4].DrawPile.Concat(piles[3].DrawPile)
                                                .Concat(piles[2].DrawPile)
                                                .Concat(piles[1].DrawPile)
                                                .Concat(piles[0].DrawPile) as ObservableCollection<Card>;
                    break;

            }
        }

        public Card Flip()
        {
            DiscardPile.Insert(0, DrawPile[0]);
            DrawPile.RemoveAt(0);
            return DiscardPile[0];
        }
    }
}
