using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Button[,] buttons;


        // buradaki sabitlere eş ve çift sayı vermezseniz oyun düzgün çalışmayacaktır
        // oyun geliştirilmeye açıktır

        private const int _x = 4;
        private const int _y = 4;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_x != _y || _x % 2 != 0 || _y % 2 != 0)
            {
                MessageBox.Show("Oyun kuralları geçersiz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            buttons = new Button[_x, _y];

            int x;
            int y = 0;

            for (int i = 0; i < _x; i++)
            {
                x = 0;
                for (int j = 0; j < _y; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Width = 50,
                        Height = 50,
                        BackColor = Color.Black,
                        ForeColor = Color.Black,
                        Cursor = Cursors.Hand
                    };
                    buttons[i, j].Text = "";
                    this.Controls.Add(buttons[i, j]);
                    buttons[i, j].Location = new Point(x, y);
                    buttons[i, j].Click += Button_Click;
                    x += 50;
                }
                y += 50;
            }

            Group();
        }

        readonly Random random = new Random();
    
        void Group()
        {
            int x;
            int y;
            int randomNumber;
            int randomIndex;

            List<string> numbers = new List<string>();

            for (int i = 0; i < _x / 2; i++)
            {
                for (int j = 0; j < _y;)
                {
                    x = random.Next(0, _x);
                    y = random.Next(0, _y);
                    randomNumber = random.Next(0, 100);
                    
                    if (buttons[x, y].Text == "" && !numbers.Contains(randomNumber.ToString()))
                    {                        
                        buttons[x, y].Text = randomNumber.ToString();
                        numbers.Add(randomNumber.ToString());
                        j++;
                    }
                }
            }

            for (int i = 0; i < _x / 2; i++)
            {
                for (int j = 0; j < _y;)
                {
                    x = random.Next(0, _x);
                    y = random.Next(0, _y);
                    randomIndex = random.Next(0, numbers.Count);

                    if (buttons[x, y].Text == "")
                    {
                        buttons[x, y].Text = numbers[randomIndex];
                        numbers.RemoveAt(randomIndex);
                        j++;
                    }
                }
            }
        }

        string match = "";
        Button matchButton;

        int counter = 0;

        private void Button_Click(object sender, EventArgs e)
        {
            var text = (sender as Button).Text;
            var button = sender as Button;

            if (match == "")
            {
                match = text;
                matchButton = button;
                matchButton.ForeColor = Color.White;
            }
            else if (match == text)
            {
                button.ForeColor = Color.White;
                match = "";
                Task.Delay(1500).Wait();
                matchButton.BackColor = Color.Green;
                button.BackColor = Color.Green;

                matchButton.Enabled = false;
                button.Enabled = false;

                counter++;

                if (counter == _x + _y)
                {
                    MessageBox.Show("Oyun bitti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                button.ForeColor = Color.White;
                match = "";
                Task.Delay(1500).Wait();
                matchButton.ForeColor = Color.Black;
                button.ForeColor = Color.Black;
            }
        }
    }
}
