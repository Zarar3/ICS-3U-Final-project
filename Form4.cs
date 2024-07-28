using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using AudioPlayer;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace KhanFinalGame1
{
    public partial class Form4 : Form
    {
        PrivateFontCollection fontCollection;// Collection to hold custom fonts
        Font retrofont; // Custom font object
        Image enemyImage;// Image object for enemy
        Rectangle enemyImageHolder;// Rectangle to hold enemy image position and size
        Image playerImage;// Image object for player
        Rectangle playerImageHolder;// Rectangle to hold player image position and size
        Image obstacleImage;// Image object for obstacles
        Rectangle obstacleImageHolder;// Rectangle to hold obstacle image position and size
        Rectangle storyBox;//Rectangle to display story text
        Brush brush;//Brush object for drawing text
        Rectangle instructionBox;// Rectangle to display instructions
        Image backgroundImage;// Image object for background
        private AudioFilePlayer introSong = new AudioFilePlayer();// Audio player variable for the song
        private string FilePath = @"D:\KhanFinalGame1\KhanFinalGame1\KhanFinalGame1\bin\Debug\intro.wav";//Sets the filepath

        //Method to play music
        public void playLooping()
        {
            introSong.setAudioFile(FilePath);// Sets the audio file
            introSong.playLooping();//Starts looping of audio
        }
        //Method to stop music
        public void stop()
        {
            introSong.stop();//stops the audio
        }

        public Form4()
        {
            InitializeComponent();  // Initializes the form and its components
        }
        //Event for when the fom loads
        private void Form4_Load(object sender, EventArgs e)
        {
            playLooping();// Starts playing the intro music in a loop
            backgroundImage = Image.FromFile(Application.StartupPath + @"\station.jpg");// Loads the background image from file
            fontCollection = new PrivateFontCollection();// Initializes the private font collection
            fontCollection.AddFontFile(Application.StartupPath + @"\retrofont.TTF");// Loads a custom font from file into the collection
            retrofont = new Font(fontCollection.Families[0], 12, FontStyle.Bold);// Creates a custom font object with specific attributes
            this.BackgroundImage = backgroundImage;// Sets the background image of the form
            this.BackgroundImageLayout = ImageLayout.Stretch;// Sets the layout style of the background image to stretch
            this.Width = 800;// Sets the width of the form
            this.Height = 640;// Sets the height of the form
            this.Resize += Form4_Resize;// Binds the form resize event to Form4_Resize method
            instructionBox = new Rectangle(playerImageHolder.Right + 200, this.ClientSize.Height - 220, 550, 450);//Defines the position and size of the instruction box
            storyBox = new Rectangle(this.ClientSize.Width - 700, this.ClientSize.Height - 490, 500, 320);// Defines the position and size of the story box
            obstacleImage = Image.FromFile(Application.StartupPath + @"\asteroid.png");// Loads the obstacle image from file
            obstacleImageHolder = new Rectangle(this.ClientSize.Width - 150, 0 + 200, 100, 100);// Defines the position and size of the obstacle image
            enemyImage = Image.FromFile(Application.StartupPath + @"\enemy.png");//Loads the enemy image from file
            enemyImageHolder = new Rectangle(this.ClientSize.Width - 150, 0 + 100, 100, 100);// declars the positoin and size of the enemy image
            playerImage = Image.FromFile(Application.StartupPath + @"\player.jpg");//Loads the player image from file
            playerImageHolder = new Rectangle(75, this.ClientSize.Height - 200, 100, 100);//States the position and size of the player image
            this.KeyDown += Form4_KeyDown;//Creates a key down event
            this.Paint += Form4_Paint;// Creates a paint method
        }
        //occurs when the form chnages sizes
        private void Form4_Resize(object sender, EventArgs e)
        {
            instructionBox = new Rectangle(playerImageHolder.Right + 50, this.ClientSize.Height - 220, 550, 450);// Adjusts the position and size of the instruction box on form resize
            storyBox = new Rectangle(this.ClientSize.Width - 700, 100, 500, 320);// Adjusts the position and size of the story box on form resize
            obstacleImageHolder = new Rectangle(this.ClientSize.Width - 150, 0 + 200, 100, 100);//Adjusts the position and size of the obstacle image on form resize
            enemyImageHolder = new Rectangle(this.ClientSize.Width - 150, 0 + 100, 100, 100);// Adjusts the position and size of the enemy image on form resize
            playerImageHolder = new Rectangle(75, this.ClientSize.Height - 200, 100, 100);//Adjusts the position and size of the player image on form resize
        }
        //Event that draws on the screen
        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            brush = Brushes.White;// Sets the brush color to white
            string instructions = "INTERCOM: \n 'COMMANDER, I KNOW ITS BEEN A WHILE SINCE YOU'VE FLEW THIS PIECE OF WORK SO IMMA GO OVER HOW MANUEVER THIS THING\n\n - USE THE ARROW KEYS TO CONTROL THE DIRECTION OF THE MOVING SHIP, THE SPACE BAR IS USED TO SHOOT. REMEMBER, YOUR PROJECTILES ARE LIMITED TO ONLY TWO, AFTER THAT YOU HAVE TO WAIT FOR IT TO RELOAD\n\n - MAKE SURE YOU DODGE THE ENEMY PROJECTILES TOO, OH AND ANOTHER THING, DODGE THE ASTEROIDS FLYING THROUGH SPACE, ONE OF THOSE HITS YOUR SHIP AND YOU MAY AS WELL BE SIX FEET UNDER. ALRIGHT COMMANDER ITS GO TIME. VLORBIANS APPROACHING NORTH!(SPACE TO START)";//Instructions for the game and sets it to a variable
            string story = "INTERCOM: \n'CHARLES CONNECTING TO BEST PILOT...CAN YOU HEAR ME? HELLO? COMMANDER! NEWS FROM THE VLORBIANS! THEY'VE DECIDED TO ATTACK! THEY'VE TAKEN DOWN OUR ALLIES SHIPS...PREVENT THE VLORBIANS FROM ENTERING OUR ATMOSPHERE BY SHOOTING THOSE ALIENS DOWN! SHOW EM THE INDOMITABLE HUMAN SPIRIT EH CAPTAIN?'";//Story for the game and sets it to avairable
            this.BackColor = Color.Black;// Sets the background color of the form to black
            e.Graphics.DrawString(instructions, retrofont, brush, instructionBox);// Draws the instructions text on the form
            retrofont = new Font(retrofont.Name, 10, FontStyle.Bold);// Changes the font size for the story text
            brush = Brushes.Red;//Sets the brush color to red
            e.Graphics.DrawString(story, retrofont, brush, storyBox);//Draws the story text on the form
            e.Graphics.DrawImage(obstacleImage, obstacleImageHolder);// Draws the obstacle image on the form
            e.Graphics.DrawImage(playerImage, playerImageHolder);//Draws the player image on the form
            e.Graphics.DrawImage(enemyImage, enemyImageHolder);//Draws the enemy image on the form
        }
        //Event that occurs if a key is pressed
        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)//Occurs if space is pressed
            {
                stop();  // Stops the intro music
                this.Hide();  // Hides the current form
                this.Close();  // Closes the current form
                Form1 form1 = new Form1();  // Creates an instance of Form1
                form1.Show();  // Shows Form1
            }
        }
        //A method to close the form playing and start the new one
        private void LoadForm(Form frm)
        {
            frm.FormClosed += new FormClosedEventHandler(frm_LoadForm);  // Binds the closed event of 'frm' to frm_LoadForm method
            this.Hide();  // Hides the current form
            frm.Show();  // Shows the form 'frm'
        }
        //a method to load the other form
        private void frm_LoadForm(object sender, FormClosedEventArgs e)
        {
            this.Show();  // Shows the current form
        }
    }
}