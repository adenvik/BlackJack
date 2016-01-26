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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BlackJackV2
{
    public partial class Form1 : Form
    {
        Bitmap b;
        LinearGradientBrush linGrBrush;
        Graphics g;
        Random r = new Random();
        Deck[] decks = new Deck[4];
        Player _player;
        Dealer _dealer;
        public int numberCards = 0;
        public double _bet = 0;
        Form2 f;
        public bool gamego = false;

        public Form1()
        {
            InitializeComponent();
            linGrBrush = new LinearGradientBrush(new Point(0, 300), new Point(1000, 300), Color.Green, Color.Yellow);
            b = new Bitmap(1000, 600);
            f = new Form2(this);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        }


        public void Game()
        {
            //cards = new List<Card>();
            _player = new Player();
            _dealer = new Dealer();

            int posX = 600;
            for (int i = 0; i < 4; i++)
            {
                decks[i] = new Deck(numberCards, posX);
                posX += 100;
            }
 
            int numberDeck = r.Next(0, 3);
            _player.AddCard(ref decks[numberDeck]);
            numberDeck = r.Next(0, 3);
            _player.AddCard(ref decks[numberDeck]);
            numberDeck = r.Next(0, 3);
            _dealer.AddCard(ref decks[numberDeck]);

            draw();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        public int Situations()
        {
            int PCount = _player.CountPoints();
            int DCount = _dealer.CountPoints();
            int number = 0;
            //Проверка дабла
            if (PCount == 10 && PCount == 11)
            {
                return 1;
            }
            //Проверка блекджека
            if (PCount == 21)
            {
                return 2;
            }
            //Проверка двух тузов
            if (_player.NumberCards == 2 &&
                _player.Cards[0].Number == 14 &&
                _player.Cards[1].Number == 14)
            {
                return 3;
            }
            //Выиграл игрок
            if (PCount > DCount && PCount <= 21)
            {
                return 4;
            }
            //Выиграл диллер
            if (PCount < DCount && DCount <= 21)
            {
                return 5;
            }
            //Красивый расход
            if (PCount == DCount)
            {
                return 6;
            }
            //Перебор как исключение
            if (PCount > 21)
            {
                throw new Exception("Перебор! Вы проиграли все что поставили.");
            }
            return number;
        }

        public int StartGame()
        {
            int nSituation = 0;
            int n = 0;
            Random rnd = new Random();
            int numberDeck;

            bool flag = true;

            DialogResult dialogResult;
            while (n != 1) // end of game
            {
                draw();
                this.TopMost = false;
                try
                {
                    nSituation = Situations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Перебор!\nВы проиграли :(");
                    break;
                }
                if (nSituation == 3)
                {
                    //Pair Ace. Win - bets * 1.5
                    MessageBox.Show("Поздравляем! Пара тузов! Ваш выигрыш составил \"полтора от ставки\": " + _bet * 1.5);
                    _bet *= 1.5;
                    break;
                }
                if (nSituation == 2)
                {
                    //BlackJack. Win - bets * 2
                    MessageBox.Show("Поздравляем! У вас BlackJack! Ваш выигрыш составил \"двойная ставка\": " + _bet * 2);
                    _bet *= 2;
                    break;
                }
                if (nSituation == 1)
                {
                    dialogResult = MessageBox.Show("Воспользоваться даблом(удавивается ваша ставка) и взять еще одну карту? - (Ок)\nПродолжение игры - (No)\nХватит - (Cancel)"
                                            , "Игра в разгаре - ДАБЛ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        _bet = _bet * 2;
                        n = 3;
                    }
                    else
                    {
                        if (dialogResult == DialogResult.No)
                        {
                            n = 2;
                        }
                        else
                        {
                            if (dialogResult == DialogResult.Cancel)
                            {
                                n = 1;
                            }
                            else
                            {
                                n = 0;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    dialogResult = MessageBox.Show("Взять еще одну карту?"
                                            , "Игра в разгаре", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        n = 2;
                    }
                    else
                    {
                        if (dialogResult == DialogResult.No)
                        {
                            n = 1;
                        }
                        else
                        {
                            n = 0;
                            break;
                        }
                    }
                }
                if (nSituation == 1 && n == 3)
                {
                    numberDeck = rnd.Next(0, 3);
                    _player.AddCard(ref decks[numberDeck]);
                    numberDeck = rnd.Next(0, 3);
                    _dealer.AddCard(ref decks[numberDeck]);
                    draw();
                    try
                    {
                        nSituation = Situations();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Перебор!\nВы проиграли :(");
                        n = 0;
                        break;
                    }
                }
                if (n == 2)
                {
                    numberDeck = rnd.Next(0, 3);
                    _player.AddCard(ref decks[numberDeck]);
                    draw();
                    try
                    {
                        nSituation = Situations();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Перебор!\nВы проиграли :(");
                        n = 0;
                        break;
                    }
                }
            }
            if (n == 0)
            {
                f = new Form2(this);
                _bet = 0;
                f.ShowDialog();
                gamego = false;
                flag = false;
                return 0;
            }
            numberDeck = rnd.Next(0, 3);
            _dealer.AddCard(ref decks[numberDeck]);
            draw();
            try
            {
                nSituation = Situations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Перебор!\nВы проиграли :(");
                _bet = 0;
                f = new Form2(this);
                f.ShowDialog();
                gamego = false;
                flag = false;

                return 0;
            }
            if (nSituation == 4)
            {
                MessageBox.Show("Вы выиграли! Ваш выигрыш составляет: " + _bet);
                _bet *= 2;
            }
            if (nSituation == 5)
            {
                MessageBox.Show("Диллер победил. Вы проиграли и остались ни с чем!");
            }
            if (nSituation == 6)
            {
                MessageBox.Show("Количество очков равное. Ваша ставка не изменилась и вернулась к вам :" + _bet);
            }
            if (flag)
            {
                f = new Form2(this);
                gamego = false;
                f.ShowDialog();
            }
            return 0;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            f.ShowDialog();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //StartGame();
        }

        public void draw()
        {
            g = Graphics.FromImage(b);
            g.FillRectangle(linGrBrush, 0, 0, 1000, 600);

            foreach (Deck d in decks)
            {
                d.ShowDeck(g);
                //g.DrawString(d.cards.Count.ToString(), new Font("Arial", 30), new SolidBrush(Color.Blue), d.X + 30, d.Y + 45);
            }
            for (int i = 0; i < _player.Cards.Count; i++)
            {
                _player.Cards[i].X = pictureBox1.Size.Width - (i + 1) * 120;
                _player.Cards[i].Y = pictureBox1.Size.Height - 220;
                _player.Cards[i].Show(g);
            }

            for (int i = 0; i < _dealer.Cards.Count; i++)
            {
                _dealer.Cards[i].X = 10 + 120 * i;
                _dealer.Cards[i].Y = 100;
                _dealer.Cards[i].Show(g);
            }
            
            g.DrawString("ИГРОК", new Font("Arial", 45,FontStyle.Italic), new SolidBrush(Color.Red), pictureBox1.Size.Width - 220, pictureBox1.Size.Height - 100);
            g.DrawString("ОЧКИ:"+_player.CountPoints(), new Font("Arial", 35), new SolidBrush(Color.Blue), pictureBox1.Size.Width - 220, pictureBox1.Size.Height - 50);
            g.DrawString("ДИЛЕР", new Font("Arial", 45, FontStyle.Italic), new SolidBrush(Color.Red), -10, -10);
            g.DrawString("ОЧКИ:" + _dealer.CountPoints(), new Font("Arial", 35), new SolidBrush(Color.Blue), -10, 50);
            g = pictureBox1.CreateGraphics();
            g.DrawImage(b, 0, 0);
        }
    }
}
