using System;

namespace CollectBag
{
    public partial class Form1 : Form
    {
        private Rectangle player;
        private List<Rectangle> moneyBags;
        private int score = 0;
        private HashSet<Keys> keysPressed = new HashSet<Keys>();
        private System.Windows.Forms.Timer gameLoop;
        private Image briefcaseImage;
        private Image moneyBagImage;
        private System.Windows.Forms.Timer spawnTimer;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            briefcaseImage = Image.FromFile("C:\\Users\\pukyt\\source\\repos\\CollectBag\\CollectBag\\briefcase.png");
            moneyBagImage = Image.FromFile("C:\\Users\\pukyt\\source\\repos\\CollectBag\\CollectBag\\moneybag.png");

            player = new Rectangle(375, 275, 50, 50);

            moneyBags = new List<Rectangle>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                moneyBags.Add(new Rectangle(random.Next(0, 750), random.Next(0, 550), 50, 50));
            }

            gameLoop = new System.Windows.Forms.Timer();
            gameLoop.Interval = 16; 
            gameLoop.Tick += GameLoop_Tick;
            gameLoop.Start();

            spawnTimer = new System.Windows.Forms.Timer();
            spawnTimer.Interval = 2000;
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            int x = random.Next(0, this.ClientSize.Width - 50);
            int y = random.Next(0, this.ClientSize.Height - 50);
            moneyBags.Add(new Rectangle(x, y, 50, 50));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(briefcaseImage, player);

            foreach (var moneyBag in moneyBags)
            {
                g.DrawImage(moneyBagImage, moneyBag);
            }

            g.DrawString($"Puntuación: {score}", new Font("Open sans", 16), Brushes.Black, 10, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            if (keysPressed.Contains(Keys.Up) && player.Top > 0) player.Y -= 5;
            if (keysPressed.Contains(Keys.Down) && player.Bottom < this.ClientSize.Height) player.Y += 5;
            if (keysPressed.Contains(Keys.Left) && player.Left > 0) player.X -= 5;
            if (keysPressed.Contains(Keys.Right) && player.Right < this.ClientSize.Width) player.X += 5;

            for (int i = moneyBags.Count - 1; i >= 0; i--)
            {
                if (player.IntersectsWith(moneyBags[i]))
                {
                    moneyBags.RemoveAt(i);
                    score++;
                }
            }

            this.Invalidate();
        }
    }
}
