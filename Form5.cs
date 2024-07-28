using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer;
using System.Windows.Forms;

namespace KhanFinalGame1
{
    public partial class Form5 : Form
    {
        Button restartButton = new Button();//Creates a restart button
        Button exitButton = new Button();//Creates an exit button
        Rectangle gamerOverBox;//Rectangle to hold game over message position and size
        Image gameOver;//Image object for game over message
        Image explosion;//Image object for explosion
        Rectangle explosionHolder;//Rectangle to hold explosion image position and size
        private AudioFilePlayer loseSong = new AudioFilePlayer();//Audio player object for losing sound
        private AudioFilePlayer deadEx = new AudioFilePlayer();//Audio player object for explosion sound
        private string applicationFilePath = @"D:\KhanFinalGame1\KhanFinalGame1\KhanFinalGame1\bin\Debug\deadex.wav";//Sets the path to the explosion sound file
        private string FilePath = @"D:\KhanFinalGame1\KhanFinalGame1\KhanFinalGame1\bin\Debug\lose.wav";//Sets the path to the losing sound file

        public Form5()
        {
            InitializeComponent(); //Initializes the form and its components
        }

        //A method to play the song
        public void playLooping()
        {
            deadEx.setAudioFile(applicationFilePath);//Sets the audio file for explosion sound
            deadEx.play();//Plays the explosion sound
            loseSong.setAudioFile(FilePath);//Sets the audio file for losing sound
            loseSong.playLooping();//Starts playing the losing sound in a loop
        }

        //A method to stop the song
        public void stop()
        {
            loseSong.stop();//Stops playing the losing sound
        }

        //occurs when the form loads
        private void Form5_Load(object sender, EventArgs e)
        {
            playLooping();//Starts playing the explosion and losing sounds
            this.Width = 800;//Sets the width of the form
            this.Height = 640;//Sets the height of the form
            this.BackColor = Color.Black;//Sets the background color of the form to black
            this.Paint += Form5_Paint1;//creates a paint event
            this.Controls.Add(exitButton);//Adds the exit button to the form's controls
            this.exitButton.Click += ExitButton_Click;//creates an exit button event
            exitButton.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);//Sets the position of the exit button
            exitButton.Visible = true;//Sets the exit button to be visible
            exitButton.Width = 190;//Sets the width of the exit button
            exitButton.Height = 100;//Sets the height of the exit button
            exitButton.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);//Sets the font of the exit button text
            exitButton.ForeColor = Color.White;//Sets the text color of the exit button
            exitButton.Text = "SURRENDER?";//Sets the text of the exit button
            this.Controls.Add(restartButton);//Adds the restart button to the form's controls
            this.restartButton.Click += RestartButton_Click;//creates a click event
            restartButton.Location = new Point(this.ClientSize.Width / 2 - 200, this.ClientSize.Height / 2);//Sets the position of the restart button
            restartButton.Visible = true;//Sets the restart button to be visible
            restartButton.Width = 190;//Sets the width of the restart button
            restartButton.Height = 100;//Sets the height of the restart button
            restartButton.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);//Sets the font of the restart button text
            restartButton.ForeColor = Color.White;//Sets the text color of the restart button
            restartButton.Text = "RESTART?";//Sets the text of the restart button
            explosion = Image.FromFile(Application.StartupPath + @"\deadplane.png");//Loads the explosion image from file
            gameOver = Image.FromFile(Application.StartupPath + @"\gameover (2).png");//Loads the game over image from file
            gamerOverBox = new Rectangle(this.ClientSize.Width / 2 - 210, -25, 400, 200);//Sets the position and size of the game over box
            explosionHolder = new Rectangle(this.ClientSize.Width / 2 - 100, 125, 200, 200);//Sets the position and size of the explosion image holder
            this.Resize += Form5_Resize;//Creates a resize event
        }
        //Occurs when the form resizes
        private void Form5_Resize(object sender, EventArgs e)
        {
            gamerOverBox.Location = new Point(this.ClientSize.Width / 2 - 210, exitButton.Top - 150);//Adjusts the position of the game over box on form resize
            restartButton.Location = new Point(this.ClientSize.Width / 2 - 200, this.ClientSize.Height / 2);//Adjusts the position of the restart button on form resize
            exitButton.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);//Adjusts the position of the exit button on form resize
            explosionHolder.Location = new Point(this.ClientSize.Width / 2 - 100, exitButton.Top - 200);//Adjusts the position of the explosion image holder on form resize
        }
        //Occurs when the form paints the screen
        private void Form5_Paint1(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(gameOver, gamerOverBox);//Draws the game over image at the specified rectangle position
            e.Graphics.DrawImage(explosion, explosionHolder);//Draws the explosion image at the specified rectangle position
        }
        //Occurs when the restartbutton is rpessed
        private void RestartButton_Click(object sender, EventArgs e)
        {
            stop();//Stops playing the losing sound
            this.Hide();//Hides the current form
            this.Close();//Closes the current form
            Form1 form1 = new Form1();//Creates form 1
            form1.Show();//Shows Form1
        }

        //Occurs when the exit button is clicked
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Exits the application
        }
        //A method fo when the form loads
        private void LoadForm(Form frm)
        {
            frm.FormClosed += new FormClosedEventHandler(frm_LoadForm);//creates a load form event
            this.Hide();//Hides the current form
            frm.Show();//Shows the new form
        }
        //A metho to load the form
        private void frm_LoadForm(object sender, FormClosedEventArgs e)
        {
            this.Show();//Shows the current form
        }
    }
}
