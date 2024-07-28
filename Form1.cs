using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using AudioPlayer;
using System.Media;


namespace KhanFinalGame1
{
    /*Zarar Khan
    Mr.Chalksiris
    ICS-3U1
    1 June 2024*/
    public partial class Form1 : Form
    {
        //Sets the background image
        Image backgroundImage;
        // Variable to keep track of the score
        int score;
        // Rectangle to represent the player
        Rectangle player;
        // Change in player's x-coordinate (movement speed)
        int playerdx;
        //List to store enemy rectangles
        List<Rectangle> enemy = new List<Rectangle>();
        //List to store enemy projectiles
        List<Rectangle> enemyProj = new List<Rectangle>();
        //List to store player's projectiles
        List<Rectangle> proj = new List<Rectangle>();
        //List to store obstacles
        List<Rectangle> obstacles = new List<Rectangle>();
        //Timer for game updates
        Timer time;
        // Image for player
        Image playerImage;
        //Image for enemy
        Image enemyImage;
        // Image for player's projectiles
        Image projImage;
        //Image for enemy's projectiles
        Image enemyProjImage;
        // Label to display score
        Label scoreLabel = new Label();
        //Label to display lives
        Label livesLabel = new Label();
        //Random number generator for various game elements
        Random randomNum = new Random();
        // Label to display timer
        Label timerLabel = new Label();
        //Change in projectile's y-coordinate (movement speed)
        int projdy;
        // Change in enemy projectile's y-coordinate (movement speed)
        int enemyProjdy;
        // Image for explosion effect
        Image explosion;
        // Variable to keep track of lives
        int lives;
        // Image for enemy explosion effect
        Image enemyExplosion;
        // Change in player's y-coordinate 
        int playerdy;
        // Change in enemy's x-coordinate 
        int enemydx;
        // Random number generator for obstacles
        Random randomObstacles = new Random();
        // Image for obstacles
        Image obstaclesImage;
        // Change in obstacle's x-coordinate (movement speed)
        int obstaclesdx;
        // Timer for enemy actions
        Timer enemyTimer;
        // Variable to keep track of timer count
        int timerCount;
        //Ammo count
        int ammo;
        //Ammo timer
        Timer ammoTimer;
        //Ammo number label
        Rectangle ammoNumberLabel;
        //Timer for drawings
        Timer drawTimer = new Timer();
        //sets a text color variable
        Brush brush;

        public AudioFilePlayer song = new AudioFilePlayer();//creates a variable to deal with sounds called song
        public string audioFilePath = @"D:\KhanFinalGame1\KhanFinalGame1\KhanFinalGame1\bin\Debug\fight.wav"; // sets the file path for the computer to reach the song

        // Constructor for Form1
        public Form1()
        {
            InitializeComponent(); // Initializes the form's components
        }
        //a method that is used to loop the song
        public void playLooping()
        {
            song.setAudioFile(audioFilePath);//Sets the song to the filepath
            song.playLooping();//Plays and loops the song
        }
        //A method to stop the song
        public void stop()
        {
            song.stop();//Stops the song 
        }
        // Method to handle form load event
        private void Form1_Load(object sender, EventArgs e)
        {
            playLooping();//Calls the playlooping method for the song
            //Create background image
            backgroundImage = Image.FromFile(Application.StartupPath + @"\infinitevoid.png");
            // Load obstacle image
            obstaclesImage = Image.FromFile(Application.StartupPath + @"\asteroid.png");
            lives = 1;//sets the lives amount
            ammo = 2;//sets the ammo amount
            // Load enemy explosion image
            enemyExplosion = Image.FromFile(Application.StartupPath + @"\projexplosion.png");
            // Load player explosion image
            explosion = Image.FromFile(Application.StartupPath + @"\explosion.png");
            // Load enemy projectile image
            enemyProjImage = Image.FromFile(Application.StartupPath + @"\enemyproj.png");
            // Load player projectile image
            projImage = Image.FromFile(Application.StartupPath + @"\hollowpurple.png");
            // Load enemy image
            enemyImage = Image.FromFile(Application.StartupPath + @"\enemy.png");
            // Set background of the form
            this.BackgroundImage = backgroundImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // Load player image
            playerImage = Image.FromFile(Application.StartupPath + @"\player.jpg");
            // Set form width and height
            this.Width = 800;
            this.Height = 500;
            // Enable double buffering to reduce flickering
            this.DoubleBuffered = true;
            // sets player rectangle
            player = new Rectangle(400, this.Height - 100, 35, 35);
            // Initialize timer for game updates
            time = new Timer();
            // Initialize timer for enemy actions
            enemyTimer = new Timer();
            //Sets a timer for the ammo
            ammoTimer = new Timer();
            // Set capacity for enemy projectiles list
            enemyProj.Capacity = 10;
            // Set capacity for player projectiles list
            proj.Capacity = 10;
            // Set capacity for enemy list
            enemy.Capacity = 10;
            // Set capacity for obstacles list
            obstacles.Capacity = 5;
            // Call method to line up enemies
            enemyLineup(enemy);
            // Set initial enemy movement speed
            enemydx = 2;
            // Display lives label
            livesLabel.Visible = true;//makes it visible
            livesLabel.Text = "Lives: " + lives;//writes the count of lives
            livesLabel.ForeColor = Color.White;//makes the color of the text white
            livesLabel.BackColor = Color.Transparent;//makes the background transparent
            // Configure timer label
            timerLabel.Visible = true;//makes it visible
            timerLabel.Text = "Timer: " + timerCount.ToString();//writes the count of time
            timerLabel.ForeColor = Color.White;//makes the color of the text white
            timerLabel.BackColor = Color.Transparent;//makes the background transparent
            // Configure score label
            scoreLabel.Visible = true;//makes it visible
            scoreLabel.Text = "Score: " + score;//writes the count of score
            scoreLabel.ForeColor = Color.White;//makes the color of the text white
            scoreLabel.BackColor = Color.Transparent;//makes the background transparent
            // Adds the key down and key up events
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            // Adds the timer tick events
            this.time.Tick += Time_Tick;//general game clock
            this.enemyTimer.Tick += EnemyTimer_Tick;//enemy clock
            this.ammoTimer.Tick += AmmoTimer_Tick;//ammo clock
            this.drawTimer.Tick += DrawTimer_Tick;//Drawing the labels clock
            // Set enemy timer interval and start it
            enemyTimer.Interval = 1000;
            enemyTimer.Start();
            //Set ammo timer interval and starts it
            ammoTimer.Interval = 2000;
            ammoTimer.Start();
            //Drawing timer interval and starts it
            drawTimer.Interval = 500;
            drawTimer.Start();
            // Subscribe to paint event
            this.Paint += Form1_Paint;
            // Configure lives label appearance
            livesLabel.Location = new Point(0, 0);
            livesLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold | FontStyle.Underline);
            // Configure score label appearance
            scoreLabel.Location = new Point(this.ClientSize.Width - 100, 0);
            scoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold | FontStyle.Underline);
            // Configure timer label appearance
            timerLabel.Location = new Point(this.ClientSize.Width/2, 0);
            timerLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold | FontStyle.Underline);
            //Configure ammo number label location
            ammoNumberLabel.Location = new Point(player.X, player.Y - 25);
            // Add labels to the form
            this.Controls.Add(livesLabel);
            this.Controls.Add(scoreLabel);
            this.Controls.Add(timerLabel);
            // Set game update timer interval and start it
            time.Interval = 1000 / 60;
            time.Start();
            // Force the form to repaint
            this.Invalidate();
        }

        //Draws the timer and score
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            scoreLabel.Location = new Point(this.ClientSize.Width - 100, 0);//Writes the score
            timerLabel.Location = new Point(this.ClientSize.Width / 2, 0);//Writes the time
        }
        //Coutns the players ammo
        private void AmmoTimer_Tick(object sender, EventArgs e)
        {
            if (ammo<2)//Only occurs when ammo is less than two
            {
                ammo++;//Adds one to ammo
            }
        }

        // Method to handle enemy timer tick event
        private void EnemyTimer_Tick(object sender, EventArgs e)
        {
            // Increment timer count
            timerCount++;
            // Update timer label
            timerLabel.Text = "Timer: " + timerCount.ToString();
            // only occurs if enemy count is greater than zero and spawns in obstacles and sets the shooting of enemies
            if (enemy.Count > 0)
            {
                int randomObstacles = randomNum.Next(300, this.Height);//Random number
                obstacles.Add(new Rectangle(0, randomObstacles - 90, 50, 50));//Adds the obstacles
                obstaclesdx = 10;//Sets the obstacle speed
                int minNumber = 0;//Sets the minimum number for the next random number generator
                int randomIndex = randomNum.Next(minNumber, enemy.Count);//generates a random number from 0-10
                enemyProj.Add(new Rectangle(enemy[randomIndex].X, enemy[randomIndex].Y, 30, 30));//makes a random enemy shoot
                enemyProjdy = 10;//Sets the enemy projectile speed
            }
        }



        // Method to initialize enemy lineup
        private static bool enemyLineup(List<Rectangle> enemy)
        {
            int enemyposition;
            // Add first enemy
            enemy.Add(new Rectangle(50, 50, 50, 50));
            // Add rest of the enemies
            for (int i = 0; i < 9; i++)
            {
                enemyposition = enemy[i].X;//Finds the x value of the enemies postion
                enemy.Add(new Rectangle(enemyposition + 50, 50, 50, 50));//Adds new enemies next to each other
            }
            return true;//returns true to complete the process
        }

        //Event for drawing on screen
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Define the ammo rectangle 
             ammoNumberLabel = new Rectangle(player.X, player.Y-25, 20, 20);//Makes the label track the player

            // Create a font for the text
            Font font = new Font("Arial", 12); // Adjust the font and size as needed

            // Create a color of the ammo number based on how much is left
            if (ammo == 2)
            {
                brush = Brushes.Green;//sets the color green
            }
            else if (ammo == 1)
            {
                brush = Brushes.Yellow;//sets the color yellow
            }
            else if (ammo == 0)
            {
                brush = Brushes.Red;//sets the color red
            }

            // Create the text to be displayed
            string text = ammo.ToString();

            // Draw the text within the rectangle
            e.Graphics.DrawString(text, font, brush, ammoNumberLabel);
            // Draw player image
            e.Graphics.DrawImage(playerImage, player);
            // Draw each enemy image
            for (int i = 0; i < enemy.Count; i++)
            {
                e.Graphics.DrawImage(enemyImage, enemy[i]);//draws the enemy image
            }
            // Draw each obstacle image and handle off-screen obstacles
            for (int i = 0; i < obstacles.Count; i++)
            {
                e.Graphics.DrawImage(obstaclesImage, obstacles[i]);//Draws the obstacle image
                if (obstacles[i].X > this.ClientSize.Width)//Occurs if the obstacles pass the screen
                {
                    obstacles.RemoveAt(i);//Rewmoves the obstacle
                    i--;//takes one away from the list 
                    break;//Breaks the loop when this happens
                }
            }
            // Draw each player projectile image and handle off-screen projectiles
            for (int i = 0; i < proj.Count; i++)
            {
                e.Graphics.DrawImage(projImage, proj[i]);//Draws the projectile image
                if (proj[i].Y < 0)//Occurs if the obstacles pass the screen
                {
                    proj.RemoveAt(i);//Rewmoves the projectile
                    i--;//takes one away from the list 
                    break;//Breaks the loop when this happens
                }
            }
            // Draw each enemy projectile image and handle off-screen projectiles
            for (int i = 0; i < enemyProj.Count; i++)
            {
                e.Graphics.DrawImage(enemyProjImage, enemyProj[i]);//draws enemy proj
                if (enemyProj[i].Y > this.ClientSize.Height)//Occurs if enemy proj goes off screen
                {
                    enemyProj.RemoveAt(i);//Removes the enemy projectile
                    i--;//reduces one from the list
                }
            }
            // Handle game win condition
            if (enemy.Count == 0)
            {
                // Stop the game update timer
                time.Stop();
                stop();//stops the music
                //Stop the enemy action timer
                enemyTimer.Stop();
                // Stop enemy movement
                enemydx = 0;
                // Display win message
                this.FormClosed += new FormClosedEventHandler(frm_LoadForm);//Creates the form closed event
                this.Hide();//Hides the form
                Form6 form6 = new Form6();//Creates the victory song
                form6.Show();//Shows the victory
            }
            // Check for collisions between player projectiles and enemies
            for (int i = 0; i < proj.Count; i++)
            {
                for (int j = 0; j < enemy.Count; j++)//A for loop to run this as long as enemies are alive
                {
                    if (proj[i].IntersectsWith(enemy[j]))//If the player projectile intersects with the enemy
                    {
                        // Remove projectile upon collision
                        proj.RemoveAt(i);
                        // Draw explosion image
                        e.Graphics.DrawImage(enemyExplosion, enemy[j]);
                        // Remove enemy upon collision
                        enemy.RemoveAt(j);
                        // Increase score and update score label
                        score = score + 20;
                        scoreLabel.Text = "Score: " + score;
                        // Adjust index to account for removed projectile
                        i--;
                        // Exit inner loop after handling collision
                        break;
                    }
                }
            }
            // a for loop that checks for collisions between obstacles and player
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i].IntersectsWith(player))//Occurs when the obstacles intersects with players
                {
                    // Stop the enemy timer, timer and music
                    enemyTimer.Stop();
                    time.Stop();
                    stop();
                    // Set lives to 0 and update lives label
                    lives = 0;
                    livesLabel.Text = "Lives: " + lives;
                    // Clear all obstacles, enemy projectiles, and player projectiles
                    obstacles.RemoveAll(x => true);
                    enemyProj.RemoveAll(x => true);
                    proj.RemoveAll(x => true);
                    // Draw explosion image
                    e.Graphics.DrawImage(explosion, player);
                    // Stop enemy and player movement
                    enemydx = 0;
                    playerdy = 0;
                    playerdx = 0;
            
                    this.Hide();//hides the form
                    this.Close();//closes the form
                    Form5 form5 = new Form5();//changes to the game over form
                    form5.Show();//Shows the game over form
                }
            }
            // Check for collisions between enemy projectiles and player
           for (int i = 0; i < enemyProj.Count; i++)
            {
                if (enemyProj[i].IntersectsWith(player))//occurs if the enemy projectile intersects with the player
                {
                    time.Stop();//Stops the game timer
                    stop();//stops the music
                    // Stop the enemy timer
                    enemyTimer.Stop();
                    // Set lives to 0 and update lives label
                    lives = 0;
                    livesLabel.Text = "Lives: " + lives;
                    // Clear all enemy projectiles, player projectiles, and obstacles
                    enemyProj.RemoveAll(x => true);
                    proj.RemoveAll(x => true);
                    obstacles.RemoveAll(x => true);
                    // Stop enemy movement
                    enemydx = 0;
                    // Draw explosion image
                    e.Graphics.DrawImage(explosion, player);
                    // Stop player movement
                    playerdx = 0;
                    playerdy = 0;

                    this.Hide();//hides the form
                    Form5 form5 = new Form5();//changes it to the game over form
                    form5.Show();//Shows the game over form
                }
            }
        }

        // Method to handle game update timer tick event
        private void Time_Tick(object sender, EventArgs e)
        {
            ammoNumberLabel.Location = new Point(player.X, player.Y - 25);//Keeps the ammo tracker next to the player
            // Update player's position based on movement speed
            player.X += playerdx;
            player.Y += playerdy;

            // Update position of player projectiles
            for (int i = 0; i < proj.Count; i++)
            {
                Rectangle currentProj = proj[i];//Sets a placeholder variable to the current projectile
                currentProj.Y += projdy;//adds the movement of the projectile to the placeholder
                proj[i] = currentProj;//Makes the current proj y value equal to the placeholder variable
            }
            // Update position of enemy projectiles
            for (int i = 0; i < enemyProj.Count; i++)
            {
                Rectangle currentEnemyProj = enemyProj[i];//Sets a placeholder variable to the current enemy proj
                currentEnemyProj.Y += enemyProjdy;//adds the movement of the enemy projectile to the placeholder
                enemyProj[i] = currentEnemyProj;//Makes the current enemy proj y value equal to the placeholder variable
            }

            // Update position of enemies and handle screen edge collisions
            for (int i = 0; i < enemy.Count; i++)
            {
                Rectangle currentEnemy = enemy[i];//Sets a placeholder variable to the current enemy 
                currentEnemy.X += enemydx;//adds the movement of the enemy to the placeholder
                enemy[i] = currentEnemy;//Makes the current enemy y value equal to the placeholder variable

                if (currentEnemy.Right >= this.ClientSize.Width)//Occurs if the enemy tries to pass the right of the screen
                {
                    currentEnemy.X -= 50;//Makes sure the current enemy x value doesnt pass the right border
                    enemydx = -2;//Makes the enemies move left
                }
                else if (currentEnemy.Left <= 0)//Occurs if the enemies try to pass the left of the screen
                {
                    currentEnemy.X += 50;//Makes sure the current enemy x value doesnt pass the left border
                    enemydx = 2;//Makes the enemies move rght
                }
            }

            // Update position of obstacles
            for (int i = 0; i < obstacles.Count; i++)
            {
                Rectangle currentObstacle = obstacles[i];//Sets a placeholder variable to the current obstacle
                currentObstacle.X += obstaclesdx;//adds the movement of the enemy to the placeholder
                obstacles[i] = currentObstacle;//Makes the current obstacle x value equal to the placeholder variable
            }

            // Restrict player's movement within the screen bounds
            if (player.Top < 200)//occurs if the player tries to go to high
            {
                player.Y = 200;//Sets the player y value to keep it from passing the height limit
            }
            if (player.Bottom > this.ClientSize.Height)//Occurs when the player reaches the bottom of the screen
            {
                player.Y = this.ClientSize.Height - player.Height;//Stops the player from going under the screen
            }
            if (player.Right > this.ClientSize.Width)//Occurs when the player reaches the right of the screen
            {
                player.X = this.ClientSize.Width - player.Width;//Stops the player from going out of the right of the screen
            }
            else if (player.Left < 0)//Occurs when the player reaches the left of the screen
            {
                player.X = 0;////Stops the player from going out of the left of the screen
            }

            // Force the form to repaint
            this.Invalidate();
        }

        // Method to handle key up event (stopping player movement)
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            playerdx = 0;//Stops the player horizontal movement
            playerdy = 0;//Stops the players vertical movement
        }

        // Method to handle key down event (starting player movement and shooting)
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)//Occurs if the down arrow is pressed
            {
                playerdy = 8;//Makes the player move down
                ammoNumberLabel.Location = new Point(player.X, player.Y - 25);//The ammo tracks the players location
            }
            else if (e.KeyCode == Keys.Up)//Occurs if the up arrow is pressed
            {
                playerdy = -8;//Makes the player move down
                ammoNumberLabel.Location = new Point(player.X, player.Y - 25);//The ammo tracks the players location
            }
            if (e.KeyCode == Keys.Right)//Occurs if the right arrow is pressed
            {
                playerdx = 8;//Makes the player move right
                ammoNumberLabel.Location = new Point(player.X, player.Y - 25);//The ammo tracks the players location
            }
            if (e.KeyCode == Keys.Left)//Occurs if the left arrow is pressed
            {
                playerdx = -8;//Makes the player move left
                ammoNumberLabel.Location = new Point(player.X, player.Y - 25);//The ammo tracks the players location
            }
            if (e.KeyCode == Keys.Space)//Occurs if the space is pressed
            {
                if (ammo == 0)//Checks if the ammo count is zero
                {
                    return;//does nothing
                }
                else//occurs if ammo count is not 0
                {
                    ammo = ammo - 1;//Takes one ammo away
                }
                if (enemy.Count > 0 && ammo <= 2 && ammo > -1)//If the enemies are still alive, and the player has ammo
                {
                    proj.Add(new Rectangle(player.X, player.Y, 30, 30));//Adds a projectile 
                    projdy = -5;//Sets the movement of the projectile
                }
            }
        }
        //Method for loading the form
        private void LoadForm(Form frm)
        {
            frm.FormClosed += new FormClosedEventHandler(frm_LoadForm);//Creates an event for loading the form
            this.Hide();//Hides the form playing
            frm.Show();//Shows new form
        }
        //Method to load the new form
        private void frm_LoadForm(object sender, FormClosedEventArgs e)
        {
            this.Show();//Shows the new form
        }
    }
}