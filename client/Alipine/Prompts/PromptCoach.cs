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
    public partial class PromptCoach : Form
    {
        public string Email => tb_Email.Text;
        public string Username => tb_Username.Text;
        public string Password => mtb_Password.Text;

        public PromptCoach()
        {

            InitializeComponent();
            this.Text = "Register a coach";

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
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
            else if (!(Alpine.Helpers.ValidationHelpers.CheckEmail(Email) && Alpine.Helpers.ValidationHelpers.CheckName(Username) && Alpine.Helpers.ValidationHelpers.CheckPassword(Password)))
            {
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
