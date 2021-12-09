using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DeweyDecimal.ReplacingBooks;

namespace DeweyDecimal.Forms
{
    public partial class FormReplacingBooks : Form
    {
        //Fields
        private int NUMBER_OF_CALL_NUMBERS = 10;
        private int INCORRECT_POINT = -100;
        private int CORRECT_POINT = 250;
        private int TIME_BONUS = 10000;
        private int seconds;
        private static int highScore = 0;
        private List<string> callNumbers = new List<string>();
        private static int currentPoints = 0;

        private Button[] orderButtons;
        private Button[] callButtons;

        public FormReplacingBooks()
        {
            InitializeComponent();
            StartGame();
        }

        //-----------------------------------------------------------------------------------------------------------------------
        // Setup Methods
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// starts/restarts the sorting game
        /// </summary>
        private void StartGame()
        {
            seconds = 0;
            lblTime.Text = seconds.ToString();
            lblHighScore.Text = highScore.ToString();

            callNumbers = GenerateRandomCallNumbers(NUMBER_OF_CALL_NUMBERS);

            orderButtons = new Button[] { btnOrder1, btnOrder2, btnOrder3,
                btnOrder4, btnOrder5, btnOrder6, btnOrder7, btnOrder8, btnOrder9, btnOrder10};

            callButtons = new Button[] { btnCall1, btnCall2, btnCall3,
                btnCall4, btnCall5, btnCall6, btnCall7, btnCall8, btnCall9, btnCall10};

            setCallNumberButtons(callButtons, callNumbers, NUMBER_OF_CALL_NUMBERS);
            setOrderNumberButtons(orderButtons, NUMBER_OF_CALL_NUMBERS);

            Quicksort(callNumbers, 0, callNumbers.Count - 1);
            lblCurrentPoints.Text = currentPoints.ToString();
            timer1.Start();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Setup Methods
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Button click methods and point scoring
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Check button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            // Check if they ordered the books correctly, if so congratulate and give points, else remove points
            if (checkOrder(NUMBER_OF_CALL_NUMBERS, orderButtons, callNumbers))
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
                MessageBox.Show("Correct! \nYou ordered the books perfectly!" +
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
                MessageBox.Show("Incorrect! \nHave another look at your ordering" +
                    "\n" + INCORRECT_POINT + " points", "Unlucky!");
            }

        }

        /// <summary>
        /// Help button to explain how things work to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("10 Random generated call numbers from the Dewey Decimal System are displayed on the top shelf, called Unsorted Call Numbers Shelf." +
                "\nYour job is to then place them into the correct order on the bottom shelf. \nIt must be ordered in ascending order from left to right." +
                "\nAfter your are happy with the order you have made, please select the Check button to submit your answer!" +
                "\n\nSeconds Elapsed: Seconds gone by. The faster you order it correctly, the more points you will receive!" +
                "\n\nTotal Score: All the points you have gathered and lost while playing this session." +
                "\n\nHighscore: The highest score you got in one round!" +
                "\n\nReset button: Resets round, getting you new number but still keeping your score", "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
            timer1.Start();
        }
        /// <summary>
        /// Timer tick for calculating points based on how fast they solved it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            lblTime.Text = seconds.ToString();
        }

        /// <summary>
        /// Resets the round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            StartGame();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Button click methods and point scoring
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Button click methods for sorted list buttons
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        private void btnOrder1_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder2_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder3_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder4_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder5_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder6_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder7_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder8_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }      

        /// <summary>
        /// Button 9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }

        private void btnOrder10_Click(object sender, EventArgs e)
        {
            ActivateOrderButton(sender, NUMBER_OF_CALL_NUMBERS, orderButtons, callButtons);
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Button click methods for sorted list buttons
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Call number buttons
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        private void btnCall1_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall2_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall3_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall4_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall5_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall6_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall7_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall8_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall9_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }

        private void btnCall10_Click(object sender, EventArgs e)
        {
            ActivateCallButton(sender);
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Call number buttons
        //-----------------------------------------------------------------------------------------------------------------------
        private void label6_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Method to display correct order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewCorrectOrderButton_Click(object sender, EventArgs e)
        {
            string correctOrder = "";
            int j;
            for (int i = 0; i < NUMBER_OF_CALL_NUMBERS + 1; i++)
            {
                j = i + 1;
                if (i == NUMBER_OF_CALL_NUMBERS - 1)
                {
                    correctOrder += j + ".\t" + callNumbers[i];
                    break;
                }
                correctOrder += j + ".\t" + callNumbers[i] + "\n";
            }
            MessageBox.Show(correctOrder, "Correct Order", MessageBoxButtons.OK);
            StartGame();
        }
    }
}
