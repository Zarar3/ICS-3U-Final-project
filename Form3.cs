using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KhanFinalGame1
{
    public partial class Form3 : Form
    {
        // Declare UI elements and variables
        Button startLabel = new Button();  // Creates a button
        Image enemyImage;  // Declares a variable for holding an enemy image
        Rectangle enemyImageHolder;  // Declares a rectangle to hold the position and size of the enemy image
        Image playerImage;  // Declares a variable for holding a player image
        Rectangle playerImageHolder;  // Declares a rectangle to hold the position and size of the player image
        TextBox titleBox = new TextBox();  // Creates a text box

        Image backgroundImage;  // Declares a variable for holding the background image

        public Form3()
        {
            InitializeComponent();  // Initializes the form and its components
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Set form properties
            BackColor = Color.Black;  // Sets the background color of the form to black
            this.Width = 800;  // Sets the width of the form to 800 pixels
            this.Height = 640;  // Sets the height of the form to 640 pixels

            // Set initial positions and properties for UI elements
            titleBox.Location = new Point(this.ClientSize.Width / 2 - 200, 0);  // Positions the title text box at the top center
            startLabel.Location = new Point(this.ClientSize.Width / 2 - 100, this.ClientSize.Height / 2 - 50);  // Positions the start button at the center
            playerImageHolder = new Rectangle(75, this.ClientSize.Height - 200, 100, 100);  // Defines the initial position and size of the player image
            enemyImageHolder = new Rectangle(this.ClientSize.Width - 200, 75, 100, 100);  // Defines the initial position and size of the enemy image

            // Set background image and layout
            backgroundImage = Image.FromFile(Application.StartupPath + @"\space.jpg");  // Loads the background image from file
            this.BackgroundImage = backgroundImage;  // Sets the background image of the form
            this.BackgroundImageLayout = ImageLayout.Stretch;  // Sets the layout style of the background image to stretch

            // Load specific images from files
            enemyImage = Image.FromFile(Application.StartupPath + @"\enemy.png");  // Loads the enemy image from file
            playerImage = Image.FromFile(Application.StartupPath + @"\player.jpg");  // Loads the player image from file

            // Event bindings
            this.startLabel.Click += StartLabel_Click;  // Binds the click event of the start label button to StartLabel_Click method
            this.Paint += Form3_Paint;  // Binds the paint event of the form to Form3_Paint method

            // Add elements to the form
            this.Controls.Add(startLabel);  // Adds the start label button to the form
            this.Controls.Add(titleBox);  // Adds the title text box to the form

            //Event handler for form resize
            this.Resize += Form3_Resize;  // Binds the form resize event to Form3_Resize method

            //Set properties of the title text box
            titleBox.Visible = true;  // Makes the title text box visible
            titleBox.Width = 375;  // Sets the width of the title text box
            titleBox.Height = 150;  // Sets the height of the title text box
            titleBox.BackColor = Color.Black;  // Sets the background color of the title text box to black
            titleBox.ForeColor = Color.White;  // Sets the text color of the title text box to white
            titleBox.ReadOnly = true;  // Makes the title text box read-only
            titleBox.Font = new System.Drawing.Font("Arial", 30, System.Drawing.FontStyle.Bold);  // Sets the font of the title text box
            titleBox.Text = "SPACE INVADERS";  // Sets the initial text of the title text box

            //Set properties of the start label button
            startLabel.Visible = true;  // Makes the start label button visible
            startLabel.Width = 210;  // Sets the width of the start label button
            startLabel.Height = 100;  // Sets the height of the start label button
            startLabel.ForeColor = Color.White;  // Sets the text color of the start label button to white
            startLabel.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);  // Sets the font of the start label button
            startLabel.Text = "START\n (Click here)";  // Sets the text of the start label button
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            // Adjust positions of UI elements when form is resized
            titleBox.Location = new Point(this.ClientSize.Width / 2 - 200, 0);  // Re-aligns the title text box
            startLabel.Location = new Point(this.ClientSize.Width / 2 - 100, this.ClientSize.Height / 2 - 50);  // Re-aligns the start label button
            playerImageHolder = new Rectangle(75, this.ClientSize.Height - 200, 100, 100);  // Re-adjusts the position and size of the player image
            enemyImageHolder = new Rectangle(this.ClientSize.Width - 200, 75, 100, 100);  // Re-adjusts the position and size of the enemy image
        }

        private void StartLabel_Click(object sender, EventArgs e)
        {
            // Hide current form and show a new form (Form4) when start label button is clicked
            this.Hide();  // Hides the current form
            Form4 form4 = new Form4();  // Creates an instance of Form4
            form4.Show();  // Shows Form4
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            // Paint event handler to draw images on the form
            e.Graphics.DrawImage(playerImage, playerImageHolder);  // Draws the player image on the form
            e.Graphics.DrawImage(enemyImage, enemyImageHolder);  // Draws the enemy image on the form
        }

        private void LoadForm(Form frm)
        {
            // Method to load a new form and handle its closed event
            frm.FormClosed += new FormClosedEventHandler(frm_LoadForm);  // Binds the closed event of the form to frm_LoadForm method
            this.Hide();  // Hides the current form
            frm.Show();  // Shows the new form (passed as 'frm')
        }

        private void frm_LoadForm(object sender, FormClosedEventArgs e)
        {
            // Event handler for when a loaded form is closed; shows the current form
            this.Show();  // Shows the current form
        }
    }
}
