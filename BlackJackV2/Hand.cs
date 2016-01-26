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
    class Hand
    {
        List<Card> _cardsInHand = new List<Card>();
        public void AddCard(ref Deck A)
        {
            int number = 0, lear = 0;
            Bitmap bmp = new Bitmap(120,120);
            A.GetCard(ref number, ref lear, ref bmp);
            _cardsInHand.Add(new Card(number,lear,bmp));
        }
        public List<Card> Cards
        {
            get { return _cardsInHand; }
        }
        public int NumberCard
        {
            get { return _cardsInHand.Count; }
        }
    }
}