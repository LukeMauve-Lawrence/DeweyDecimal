using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal
{
    public partial class FormMainMenu : Form
    {
        //Fields
        private Button currentButton;
        private Form activeForm;

        //Constructor
        public FormMainMenu()
        {
            InitializeComponent();
            btnCloseChildForm.Visible = false;
        }

        /// <summary>
        /// Navigation button selected styling
        /// </summary>
        /// <param name="btnSender"></param>
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    // style all buttons back to default
                    DisableButton();

                    // then style the selected one to stand out
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(59, 148, 94);
                    currentButton.ForeColor = Color.White;

                    btnCloseChildForm.Visible = true;
                }
            }
        }

        /// <summary>
        /// Turns all navigation buttons to unselected styling
        /// </summary>
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(24, 38, 40);
                    previousBtn.ForeColor = Color.Gainsboro;                
                }
            }
        }

        /// <summary>
        /// Opens form into panel
        /// </summary>
        /// <param name="childForm"></param>
        /// <param name="btnSender"></param>
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        //-----------------------------------------------------------------------------------------------------------------------
        // Navigation Button click methods
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        private void btnReplacingBooks_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormReplacingBooks(), sender);
        }

        private void btnIdentifyingAreas_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormIdentifyingAreas(), sender);
        }

        private void btnFindingCallNumbers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormFindCallNumbers(), sender);
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Navigation Button click Methods
        //-----------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Reset to the home page, hide other forms
        /// </summary>
        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "HOME";

            currentButton = null;
            btnCloseChildForm.Visible = false;
        }

        /// <summary>
        /// Close form button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            Reset();
        }
    }
}
