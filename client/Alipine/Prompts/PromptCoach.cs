using Alpine.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Alpine
{
    // prompt for registering a new coach
    public partial class PromptCoach : Form
    {
        public string Email => tb_Email.Text;
        public string Username => tb_Username.Text;
        public string Password => mtb_Password.Text;

        public PromptCoach()
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;

            this.Text = "Register a coach";

            // we start from the center of the main form
            StartPosition = FormStartPosition.CenterParent;

            AcceptButton = btn_Submit;
            CancelButton = btn_Cancel;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // fields cannot be empty, why wait for the server to tell us this when we can nip it in the bud right here
            if (Email == "" || Username == "" || Password == "")
            {
                MessageBox.Show(
                    "Fields cannot be empty!",
                    "Fields cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            // validate that the email, username, and password make sense, if it does not, return
            else if (!(Alpine.Helpers.ValidationHelpers.CheckEmail(Email) && Alpine.Helpers.ValidationHelpers.CheckName(Username) && Alpine.Helpers.ValidationHelpers.CheckPassword(Password)))
            {
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // if the user clicks cancel we do nothing
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // for opening the manual
        protected override void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // prevent the default
            Alpine.Helpers.ManualHelpers.openHelperForm(); // we go open the manual
        }
    }
}
