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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using static Alpine.Helpers.RequestHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Alpine
{
    // prompt form for entering race times
    public partial class PromptTimes : Form
    {
        public string RaceName => cb_Race.Text;
        public string TeamASkierOne => nud_time.Text;

        public string Time => nud_time.ToString();

        public PromptTimes()
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;

            // this is to try to solve scaling issues
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            this.Text = "Schedule a race";

            // we want it centered to the main form
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            this.Visible = false; // we dont show stuff until we get our data from the server
            initMe();



        }

        // goes through a few steps to load data from the server
        private async Task initMe()
        {
            // get the races first 
            await LoadRacesAsync();

            // load skiers
            await LoadMembersAsync();

            // auto select the first 
            cb_Race.SelectedIndex = 0;



            this.Visible = true;
        }


        private async void ValidateRaces(object sender, EventArgs e)
        {

        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            // this.... PROBABLY should never be able to happen, but whatever
            if (RaceName == "")
            {
                MessageBox.Show(
                    "Fields cannot be empty!",
                    "Fields cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (int.Parse(TeamASkierOne) < 0)
            {
                MessageBox.Show(
                    "Times cannot be negative!",
                    "Times cannot be negative!",
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

        private async Task LoadRacesAsync()
        {
            try
            {
                // get our json response
                RequestHelpers request = new();
                string json = await request.PostRequestRaces();

                // deserialize it into the class whatever
                var deserialized = JsonSerializer.Deserialize<List<Race>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    foreach (var m in deserialized)
                    {
                        cb_Race.Items.Add(m.name);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading races: " + ex.Message);
            }
        }

        // partially chatgpt, we load our members into the ui
        private async Task LoadMembersAsync()
        {
            try
            {
                // send a request to the server and get the response
                RequestHelpers request = new();
                string json = await request.PostRequestMembers();

                // deserialize it into a list of members
                var deserialized = JsonSerializer.Deserialize<List<Member>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    // for each user
                    foreach (var m in deserialized)
                    {
                        if (m.role.Equals("skier")) // if it is a skier we add it to our skier list
                        {
                            cb_skier.Items.Add(m.name);
                        }
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
