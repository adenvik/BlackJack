using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJackV2
{
    public partial class Form2 : Form
    {
        Form1 f;

        public bool gamego = true;

        public Form2(Form1 form)
        {
            InitializeComponent();
            f = form;
            if (f._bet != 0)
            {
                textBox1.Text = f._bet.ToString();
            }
            else
            {
                textBox1.Text = "200";
            }
            if (f.numberCards == 52)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int number = 0;
            if (radioButton1.Checked)
            {
                number = 36;
            }
            else
            {
                number = 52;
            }
            f.numberCards = number;
            f._bet = double.Parse(textBox1.Text);
            f.Game();
            f.gamego = true;
            //f.Hide();
            f.TopMost = true;

            this.Close();
            this.Dispose();

            //f.Show();
            //f.draw();

            f.StartGame();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (f.gamego == false)
            {
                Application.Exit();
            }
        }

    }
}
