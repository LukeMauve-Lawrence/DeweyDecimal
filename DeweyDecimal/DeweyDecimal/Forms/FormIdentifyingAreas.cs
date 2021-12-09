using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DeweyDecimal.IdentifyingAreas;

namespace DeweyDecimal.Forms
{
    public partial class FormIdentifyingAreas : Form
    {
        //Fields
        private int NUMBER_OF_LEFT_BUTTONS = 4;
        private int NUMBER_OF_RIGHT_BUTTONS = 7;
        private Button[] leftButtons;
        private Button[] rightButtons;

        //Scoring variables
        private int INCORRECT_POINT = -100;
        private int CORRECT_POINT = 250;
        private int TIME_BONUS = 10000;
        private int seconds;
        private static int highScore = 0;
        private static int currentPoints = 0;

        public FormIdentifyingAreas()
        {
            InitializeComponent();
            StartGame();
        }

        /// <summary>
        /// starts/restarts the sorting game
        /// </summary>
        private void StartGame()
        {
            //resetting variables
            seconds = 0;
            lblTime.Text = seconds.ToString();
            lblHighScore.Text = highScore.ToString();
            leftButtons = new Button[] { btnLeft1, btnLeft2, btnLeft3, btnLeft4 };
            rightButtons = new Button[] { btnRight1, btnRight2, btnRight3, btnRight4, btnRight5, btnRight6, btnRight7 };
            GenerateRandomCallNumbers(leftButtons, NUMBER_OF_LEFT_BUTTONS, rightButtons, NUMBER_OF_RIGHT_BUTTONS);
            lblCurrentPoints.Text = currentPoints.ToString();
            this.Refresh();
            timer1.Start();
        }

        //-----------------------------------------------------------------------------------------------------------------------
        // Left Column clicks
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        private void btnLeft1_Click(object sender, EventArgs e)
        {
            ActivateLeftButton(sender);
        }

        private void btnLeft2_Click(object sender, EventArgs e)
        {
            ActivateLeftButton(sender);
        }

        private void btnLeft3_Click(object sender, EventArgs e)
        {
            ActivateLeftButton(sender);
        }

        private void btnLeft4_Click(object sender, EventArgs e)
        {
            ActivateLeftButton(sender);
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Left Column clicks
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Right Column clicks
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        private void btnRight1_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }

        private void btnRight2_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }

        private void btnRight3_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }

        private void btnRight4_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }

        private void btnRight5_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }

        private void btnRight6_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }

        private void btnRight7_Click(object sender, EventArgs e)
        {
            ActivateRightButton(sender, NUMBER_OF_RIGHT_BUTTONS, rightButtons, leftButtons);
            this.Refresh();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Right Column clicks
        //-----------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Draws line to match the columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            // Create pen.
            Pen redPen = new Pen(Color.Red, 3);
            Pen bluePen = new Pen(Color.Blue, 3);
            Pen greenPen = new Pen(Color.FromArgb(0, 192, 0), 3);
            Pen yellowPen = new Pen(Color.FromArgb(192, 192, 0), 3);
            
            // Create coordinates of points that define line.
            float x1 = 0.0F;
            float x2 = 310.0F;

            // Draw line to screen.
            if (pens["red"] != 0)
            {
                e.Graphics.DrawLine(redPen, x1, yLocation[0], x2, pens["red"]);
            }

            // Draw line to screen.
            if (pens["blue"] != 0)
            {
                e.Graphics.DrawLine(bluePen, x1, yLocation[1], x2, pens["blue"]);
            }

            // Draw line to screen.
            if (pens["green"] != 0)
            {
                e.Graphics.DrawLine(greenPen, x1, yLocation[2], x2, pens["green"]);
            }

            // Draw line to screen.
            if (pens["yellow"] != 0)
            {
                e.Graphics.DrawLine(yellowPen, x1, yLocation[3], x2, pens["yellow"]);
            }
        }

        /// <summary>
        /// Check answer button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            // tests answer
            if (CheckAnswer(leftButtons, NUMBER_OF_LEFT_BUTTONS, rightButtons, NUMBER_OF_RIGHT_BUTTONS))
            {
                timer1.Stop();
                if (seconds == 0)
                {
                    seconds = 1;
                }
                // calculate points
                int sessionPoints = CORRECT_POINT + (TIME_BONUS / seconds);

                // check if it is a new high score
                if (sessionPoints > highScore)
                {
                    highScore = sessionPoints;
                }

                // add points
                currentPoints += sessionPoints;
                lblCurrentPoints.Text = currentPoints.ToString();

                // user feeback
                MessageBox.Show("Correct! \nYou matched the columns perfectly!" +
                    "\nThis turn you scored: " + sessionPoints + " points", "Congratulations!");

                // restart game
                StartGame();
            }
            else
            {
                // minus points
                currentPoints += INCORRECT_POINT;
                lblCurrentPoints.Text = currentPoints.ToString();

                // user feedback
                MessageBox.Show("Incorrect! \nHave another look at your matching" +
                    "\n" + INCORRECT_POINT + " points", "Unlucky!");
            }

        }

        /// <summary>
        /// Timer tick for calculating points based on time spent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            lblTime.Text = seconds.ToString();
        }

        /// <summary>
        /// Help button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Match the left column with the right column." +
                "\nClick a left item and then click a right item afterwards." +
                "\nThe columns will then be matched!" +
                "\nMade a mistake? Don't worry, you can always click and match until you are happy with your answers." +
                "\nOnce you are happy with your answers, press the button label Check to have you answers marked." +
                "\nPoints are added based on correct answers within a time frame." +
                "\nPoints will be deducted if you answer incorrectly", "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
            timer1.Start();
        }

        /// <summary>
        /// Restarts the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        /// <summary>
        /// Shows the user the correct answer, then resets game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewAnswerButton_Click(object sender, EventArgs e)
        {
            ViewCorrectAnswers(NUMBER_OF_LEFT_BUTTONS, leftButtons, NUMBER_OF_RIGHT_BUTTONS, rightButtons);
            StartGame();
        }
    }
}
