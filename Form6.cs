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
    public partial class Form6 : Form
    {
        // Declare a Button named restartButton
        Button restartButton = new Button();

        // Declare a Button named exitButton
        Button exitButton = new Button();

        // Declare a Rectangle named victoryBox
        Rectangle victoryBox;

        // Declare an Image named victory
        Image victory;

        // Declare an AudioFilePlayer object named endSong for playing end audio
        private AudioFilePlayer endSong = new AudioFilePlayer();

        // Set the file path for the audio file
        private string FilePath = @"D:\KhanFinalGame1\KhanFinalGame1\KhanFinalGame1\bin\Debug\goodaudio.wav";//Sets the file path

        // Method to start playing the end audio in a loop
        public void playLooping()
        {
            endSong.setAudioFile(FilePath);//Set the audio file for endSong
            endSong.playLooping();//Start playing the audio file in a loop
        }

        //Method to stop playing the end audio
        public void stop()
        {
            endSong.stop();//Stop playing the end audio
        }

        public Form6()
        {
            InitializeComponent(); // Initialize the form and its components
        }

        // Occurs when the form loads
        private void Form6_Load(object sender, EventArgs e)
        {
            playLooping();//Start playing the end audio
            this.Width = 800;//Set the width of the form to 800 pixels
            this.Height = 640;//Set the height of the form to 640 pixels
            this.BackColor = Color.Black;//Set the background color of the form to black
            this.Paint += Form6_Paint; //Create the paint method
            this.Controls.Add(exitButton);// Add exitButton to the form
            this.exitButton.Click += ExitButton_Click;//Creates an event for when the exit button is pressed
            exitButton.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);// Set the location of exitButton at the center of the form
            exitButton.Visible = true;// Make exitButton visible
            exitButton.Width = 190;// Set the width of exitButton to 190 pixels
            exitButton.Height = 100;// Set the height of exitButton to 100 pixels
            exitButton.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);//Set the font of exitButton
            exitButton.ForeColor = Color.White;//Set the foreground color of exitButton to white
            exitButton.Text = "GO HOME?";//Set the text displayed on exitButton
            this.Controls.Add(restartButton);//Add restartButton to the form's Controls collection
            this.restartButton.Click += RestartButton_Click;//Creates the event for when the restart button is clicked
            restartButton.Location = new Point(this.ClientSize.Width / 2 - 200, this.ClientSize.Height / 2);//Set the location of restartButton
            restartButton.Visible = true;//Make restartButton visible
            restartButton.Width = 190;//Set the width of restartButton to 190 pixels
            restartButton.Height = 100;//Set the height of restartButton to 100 pixels
            restartButton.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);// Set the font of restartButton
            restartButton.ForeColor = Color.White;//Set the foreground color of restartButton to white
            restartButton.Text = "PLAY AGAIN?";//Set the text displayed on restartButton
            victory = Image.FromFile(Application.StartupPath + @"\victory.png");//Load the victory image from file
            victoryBox = new Rectangle(this.ClientSize.Width / 2 - 210, this.ClientSize.Height / 2 - 200, 400, 200);//Declare the rectangle for displaying victory image
            this.Resize += Form6_Resize;//Create the resize event
        }

        //Occurs for when the form is resized
        private void Form6_Resize(object sender, EventArgs e)
        {
            exitButton.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);// Adjust the location of exitButton when form is resized
            restartButton.Location = new Point(this.ClientSize.Width / 2 - 200, this.ClientSize.Height / 2);// Adjust the location of restartButton when form is resized
            victoryBox.Location = new Point(this.ClientSize.Width / 2 - 210, exitButton.Top - 150);// Adjust the location of victoryBox when form is resized
        }

        //Occurs when the restart button is pressed
        private void RestartButton_Click(object sender, EventArgs e)
        {
            stop();// Stop playing the end audio
            this.Hide();//Hide the current form
            this.Close(); // Closes the form
            Form1 form1 = new Form1();//Create a form 1
            form1.Show();//Show Form1
        }

        // Occurs when the exit button is pressed
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();//Exit the game
        }

        //Occurs when the computer has to draw the objects on screen
        private void Form6_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(victory, victoryBox);//Draw the victory image on the form
        }

        //Method to load a form
        private void LoadForm(Form frm)
        {
            frm.FormClosed += new FormClosedEventHandler(frm_LoadForm);//Creates a load form event
            this.Hide();//Hide the current form
            frm.Show();//Show the new form
        }

        // Event to show the form
        private void frm_LoadForm(object sender, FormClosedEventArgs e)
        {
            this.Show();//Show the current form
        }
    }
}
