using Alpine.Helpers;
using Alpine.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static Alpine.Helpers.RequestHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Alpine
{

    // prompt form for removing coaches from a team
    public partial class PromptRemoveCoach : Form
    {
        public string Team => cb_Team.Text;

        // locking system
        private bool updating = false;

        public PromptRemoveCoach()
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;

            this.Text = "Remove Coach From Team.";

            // we start from the center of the main form
            StartPosition = FormStartPosition.CenterParent;

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            this.Visible = false; // we dont show stuff until we get our data from the server
            initMe();

        }

        // goes through a few steps to load data from the server
        private async Task initMe()
        {
            // load our members
            await LoadTeamsAsync();

            // auto select the first and last items
            cb_Team.SelectedIndex = 0;

            
            this.Visible = true; // finally show this form
        }


        // check some stuff when we click okay
        private void btnOk_Click(object sender, EventArgs e)
        {

            // check if stuff is missing first, no use trying to send data we know the server will reject
            if (Team == "")
            {
                MessageBox.Show(
                    "Fields cannot be empty!",
                    "Fields cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

        // partially chatgpt, we load our members into the ui
        private async Task LoadTeamsAsync()
        {
            try
            {
                // send a request to the server and get the response
                RequestHelpers request = new();
                string json = await request.PostRequestTeams();

                // deserialize it into a list of strings
                var deserialized = JsonSerializer.Deserialize<List<string>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    // for each team
                    foreach (var t in deserialized)
                    {
                        cb_Team.Items.Add(t); // add this team to the gathered teams
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message);
            }
        }

        // for opening the manual
        protected override void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // prevent the default
            Alpine.Helpers.ManualHelpers.openHelperForm(); // we go open the manual
        }
    }
}
