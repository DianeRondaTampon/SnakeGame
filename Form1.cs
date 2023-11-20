namespace WinFormsSnakeGame
{
    public partial class Form1 : Form
    {
        private Snake snake;
        private Scenary scenary;
        private Point size = new Point(20, 20);
        private Point gridSize;

        Image snakeImage;
        Image fruitImage;
        Image wallImage;


        public Form1()
        {
            //StaticClass clase = new StaticClass();
            //int i = StaticClass.MyProperty;
            //StaticClass.doSomething();


            InitializeComponent();
            snake = new Snake();
            scenary = new Scenary();

            //folder of images, is diferent in debug than in release
            string imagePath;
#if DEBUG
            // Load your snake image for debug build
            imagePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "images");
#else
                // Load your snake image for release build
            imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
#endif

            string snakePath = Path.Combine(imagePath, "snake-point-20.png");
            string fruitPath = Path.Combine(imagePath, "cherry-20.jpg");
            string wallPath = Path.Combine(imagePath, "wall-20.png");

            // Load image into a variable
            snakeImage = Image.FromFile(snakePath);
            fruitImage = Image.FromFile(fruitPath);
            wallImage = Image.FromFile(wallPath);

            timer.Interval = 500; //1 second is 1000 miliseconds, so 100 is the 10th part of a second, so timer be exec 10 times per second

            // This property determines whether the form receives keyboard events before the event is passed to the control that has focus.
            KeyPreview = true;

            KeyDown += Form1_KeyDown;

            // Set focus to the form
            this.Focus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //the pictureBox is 1200*500
            //the sprites are 20*20
            //the grid will be 60*25
            gridSize = new Point(60, 25);

            int centerX = gridSize.X / 2;
            int centerY = gridSize.Y / 2;
            snake.InitializeSnake(new Point(centerX, centerY));

            scenary.InitializeScenary(new Point(0, 0), gridSize, new TimeSpan(0, 0, 0, 0, 200));

            timer.Enabled = true;
            btnStart.Visible = false;
        }

        private void changeSpeed()
        {
            if (snake.points.Count() < 5)
                timer.Interval = 300;
            else if (snake.points.Count() < 10)
                timer.Interval = 250;
            else if (snake.points.Count() < 15)
                timer.Interval = 200;
            else if (snake.points.Count() < 20)
                timer.Interval = 150;
            else if (snake.points.Count() < 25)
                timer.Interval = 100;
            else if (snake.points.Count() < 30)
                timer.Interval = 70;
            else if (snake.points.Count() < 40)
                timer.Interval = 50;
            else if (snake.points.Count() < 50)
                timer.Interval = 40;
            else if (snake.points.Count() < 100)
                timer.Interval = 30;
            else if (snake.points.Count() < 200)
                timer.Interval = 20;
            else
                timer.Interval = 10;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //timer be exec 10 times per second

            changeSpeed();

            //get user input (done by event keyDown)

            //update
            snake.Update();
            scenary.Update(snake);

            //update score
            lblScore.Text = snake.points.Count().ToString();

            //draw game
            DrawGame();

            if (!scenary.alive)
            {
                btnStart.Visible = true;
                timer.Enabled = false;
            }
        }

        private void DrawGame()
        {
            // Create an off-screen bitmap to draw on
            Bitmap bitmap = new Bitmap(pcbGraphics.Width, pcbGraphics.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Clear the background
                g.Clear(Color.White);

                // Draw the parts of snake image at the specified positions (snake.points)
                int snakeX, snakeY;
                foreach (Point point in snake.points)
                {
                    snakeX = point.X * size.X;
                    snakeY = point.Y * size.Y;
                    g.DrawImage(snakeImage, snakeX, snakeY);
                }

                // Draw the fruits at the specified positions (scenary.fruits)
                int fruitX, fruitY;
                foreach (Point point in scenary.fruits)
                {
                    fruitX = point.X * size.X;
                    fruitY = point.Y * size.Y;
                    g.DrawImage(fruitImage, fruitX, fruitY);
                }

                // Draw the walls (boundaries)
                int wallX, wallY;
                //top wall
                for (int i = 0; i < scenary.boundariesRightDown.X; i++)
                {
                    wallX = i * size.X;
                    wallY = 0;
                    g.DrawImage(wallImage, wallX, wallY);
                }
                //down wall
                for (int i = 0; i < scenary.boundariesRightDown.X; i++)
                {
                    wallX = i * size.X;
                    wallY = (scenary.boundariesRightDown.Y - 1) * size.Y;
                    g.DrawImage(wallImage, wallX, wallY);
                }
                //left wall
                for (int i = 0; i < scenary.boundariesRightDown.Y; i++)
                {
                    wallX = 0;
                    wallY = i * size.Y;
                    g.DrawImage(wallImage, wallX, wallY);
                }
                //right wall
                for (int i = 0; i < scenary.boundariesRightDown.Y; i++)
                {
                    wallX = (scenary.boundariesRightDown.X - 1) * size.X;
                    wallY = i * size.Y;
                    g.DrawImage(wallImage, wallX, wallY);
                }

                // Draw the game over if you not alive
                if (!scenary.alive)
                {
                    // Set the font and brush for drawing "Game Over"
                    Font font = new Font("Arial", 40, FontStyle.Bold);
                    Brush brush = new SolidBrush(Color.Red);

                    // Measure the size of the text to center it on the screen
                    SizeF textSize = g.MeasureString("Game Over", font);
                    float x = (pcbGraphics.Width - textSize.Width) / 2;
                    float y = (pcbGraphics.Height - textSize.Height) / 2;

                    // Draw "Game Over" in big red letters
                    g.DrawString("Game Over", font, brush, x, y);
                }
            }

            // Draw the off-screen bitmap to the PictureBox
            pcbGraphics.Image?.Dispose(); // Dispose the previous image to avoid memory leaks
            pcbGraphics.Image = bitmap;
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            //MessageBox.Show("Form1_KeyDown");
            // Handle arrow key presses
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (snake.direction != Direction.Down)
                    {
                        snake.direction = Direction.Up;
                    }
                    break;
                case Keys.Down:
                    if (snake.direction != Direction.Up)
                    {
                        snake.direction = Direction.Down;
                    }
                    break;
                case Keys.Left:
                    if (snake.direction != Direction.Right)
                    {
                        snake.direction = Direction.Left;
                    }
                    break;
                case Keys.Right:
                    if (snake.direction != Direction.Left)
                    {
                        snake.direction = Direction.Right;
                    }
                    break;
            }
            // Set focus to the form again to ensure continued keyboard input
            this.Focus();
        }

        //private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MessageBox.Show("Form1_KeyPress");
        //}
    }
}