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
    class Card
    {
        int _number;
        int _lear;
        Bitmap _card;

        public int X { get; set; }
        public int Y { get; set; }

        public Card()
        {
            _number = -1;
            _lear = -1;
            _card = null;
        }
        public Card(int number, int lear, Bitmap card)
        {
            _number = number;
            _lear = lear;
            _card = card;
        }
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        public int Lear
        {
            get { return _lear; }
            set { _lear = value; }
        }
        public Bitmap CardImage
        {
            set { _card = value; }
            get { return _card; }
        }
        public void ShowBack(Graphics g)
        {
            g.DrawImage(Properties.Resources.card_back, X, Y);
        }

        public void Show(Graphics g)
        {
            g.DrawImage(_card, X, Y);
        }
    }
}