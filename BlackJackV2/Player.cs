using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BlackJackV2
{
    class Player
    {
        Hand _hand = new Hand();
        public void AddCard(ref Deck A)
        {
            _hand.AddCard(ref A);
        }
        public int CountPoints()
        {
            int sum = 0;
            int numberCardInHand = _hand.NumberCard;
            for (int i = 0; i < numberCardInHand; i++)
            {
                if (_hand.Cards[i].Number > 10 && _hand.Cards[i].Number < 14)
                {
                    sum += 10;
                }
                else
                {
                    if (_hand.Cards[i].Number == 14)
                    {
                        if (sum + 11 <= 21)
                        {
                            sum += 11;
                        }
                        else
                        {
                            sum++;
                        }
                    }
                    else
                    {
                        sum += _hand.Cards[i].Number;
                    }
                }
            }
            return sum;
        }
        public List<Card> Cards
        {
            get { return _hand.Cards; }
        }
        public int NumberCards
        {
            get { return _hand.NumberCard; }
        }
    }
}