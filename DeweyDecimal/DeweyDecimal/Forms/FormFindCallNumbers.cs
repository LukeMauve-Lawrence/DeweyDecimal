using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DeweyDecimal.FindCallNumbers;

namespace DeweyDecimal.Forms
{
    public partial class FormFindCallNumbers : Form
    {        
        // form controls
        private Button[] optionButtons;
        private Label toFind;

        public FormFindCallNumbers()
        {
            InitializeComponent();
            StartGame();
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        private void StartGame()
        {
            // resetting scoring
            seconds = 0;
            sessionPoints = 0;
            lblTime.Text = seconds.ToString();
            lblHighScore.Text = highScore.ToString();

            // form controls
            optionButtons = new Button[] { btnOption1, btnOption2, btnOption3, btnOption4 };
            toFind = lblToFind;

            PopulateRandomLevel1();
            PopulateNodes(optionButtons);
            SetToFindLabel(toFind);
            lblCurrentPoints.Text = sessionPoints.ToString();
            timer1.Start();
        }

        //-----------------------------------------------------------------------------------------------------------------------
        // Button clicks
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        private void btnOption1_Click(object sender, EventArgs e)
        {
            if (ActivateButton(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            if (ActivateButton(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            if (ActivateButton(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            if (ActivateButton(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        /// <summary>
        /// Reset game button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            //restart game
            StartGame();
        }

        /// <summary>
        /// Help button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Select the call number and description that is a " +
                "\nhigher level version of the description" +
                "\nlisted at the top of the page", "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
            timer1.Start();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // Button clicks
        //-----------------------------------------------------------------------------------------------------------------------

        private void FormFindCallNumbers_Load(object sender, EventArgs e)
        {

        }

        
        /// <summary>
        /// Timer, used to calculate points
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            lblTime.Text = seconds.ToString();
        }
    }
}
