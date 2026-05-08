using Alpine.Helpers;
using Alpine.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using static Alpine.Helpers.RequestHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Alpine
{
    // prompt for cancelling a race
    public partial class PromptCancel : Form
    {
        public string Value1 => cb_Race.Text;

        public PromptCancel(string title, string lbl1)
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;

            Text = title;

            label1.Text = lbl1;

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

            // load our races
            await LoadRacesAsync();

            // auto select the first item
            cb_Race.SelectedIndex = 0;
           
        }

        // request our races from the server and then add them to our selection box
        private async Task LoadRacesAsync()
        {
            try
            {
                // send a request to the server and get the response
                RequestHelpers request = new();
                string json = await request.PostRequestRaces();

                // deserialize it into a list of members
                var deserialized = JsonSerializer.Deserialize<List<Races>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    // for ever race in the response
                    foreach (var r in deserialized)
                    {
                        // add it to the selection
                        cb_Race.Items.Add(r.name);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading races: " + ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // check if stuff is missing first, no use trying to send data we know the server will reject
            if (Value1 == "")
            {
                MessageBox.Show(
                    "Field cannot be empty!",
                    "Field cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

        // for opening the manual
        protected override void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // prevent the default
            Alpine.Helpers.ManualHelpers.openHelperForm(); // we go open the manual
        }
    }
}
