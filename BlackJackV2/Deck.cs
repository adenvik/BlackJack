using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BlackJackV2
{
    class Deck
    {
        int _numberCards;
        List<Card> _cards = new List<Card>();

        public int X { set; get; }
        public int Y { set; get; }

        public Deck(int numberCards, int positionX)
        {
            X = positionX;
            Y = 10;
            Random r = new Random();
            _numberCards = numberCards;
            int count, k = 0;
            if (_numberCards == 36)
            {
                count = 6;
            }
            else
            {
                count = 2;
            }
            string[] lears = {"_clubs","_diamonds","_hearts","_spades" };
            for (int i = 0; i < _numberCards; i++)
            {
                if (k < 4)
                {
                    Bitmap bmp = new Bitmap(120, 120);
                    Graphics g = Graphics.FromImage(bmp);
                    Matrix m = new Matrix();
                    m.RotateAt(r.Next(-90, 90), new Point(60, 60));
                    g.Transform = m;
                    if (count < 10)
                    {
                        g.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("cards_0" + count + lears[k]), 27, 17);
                    }
                    else
                    {
                        g.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("cards_" + count.ToString() + lears[k]), 27, 17);
                    }
                    _cards.Add(new Card(count, k + 3,bmp));
                    k++;
                }
                else
                {
                    k = 0;
                    count++;
                    i--;
                }
            }
        Shuffle();
        }
        public void Shuffle()
        {
            //Random rng = new Random();
            //int n = _numberCards;
            //while (n > 1)
            //{
            //    n--;
            //    int k = rng.Next(n + 1);
            //    Card value = _cards[k];
            //    _cards[k] = _cards[n];
            //    _cards[n] = value;
            //}
            Random r = new Random();
            Card temp = new Card();
            int k;
            for (int i = _cards.Count - 1; i >= 0; i--)
            {
                k = r.Next(0, i);
                temp = _cards[i];
                _cards[i] = _cards[k];
                _cards[k] = temp;

                _cards[i].X = X + i / 2;
                _cards[i].Y = Y + i / 2;
            }
        }
        public int NumberCard
        {
            get { return _numberCards; }
            set { _numberCards = value; }
        }
        public void GetCard(ref int number, ref int lear, ref Bitmap bmp)
        {
            number = _cards[NumberCard - 1].Number;
            lear = _cards[NumberCard - 1].Lear;
            bmp = _cards[NumberCard - 1].CardImage;
            _cards.RemoveAt(NumberCard - 1);
            NumberCard--;
        }
        public Card GetCard1()
        {
            //_cards.RemoveAt(NumberCard - 1);
            NumberCard--;
            return _cards[_cards.Count - 1];
        }
        public List<Card> cards
        {
            get { return _cards; }
        }

        public void ShowDeck(Graphics g)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].ShowBack(g);
            }
            g.DrawString(_cards.Count.ToString(), new Font("Arial", 30), new SolidBrush(Color.DarkRed), X + 25, Y + 60);
        }
    }
}