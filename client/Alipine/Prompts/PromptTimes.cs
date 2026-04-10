using Alpine.Helpers;
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
    public partial class PromptTimes : Form
    {
        public string RaceName => cb_Race.Text;
        public string TeamASkierOne => nud_SkierOne.Text;
        public string TeamASkierTwo => nud_SkierTwo.Text;
        public string TeamBSkierOne => nud_SkierThree.Text;
        public string TeamBSkierTwo => nud_SkierFour.Text;

        public PromptTimes()
        {
            InitializeComponent();

            this.Text = "Schedule a race";

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            this.Visible = false; // we dont show stuff until we get our data from the server
            initMe();

            // TODO database should never have ONLY one team but we should CHECK for that


        }

        private async Task initMe()
        {
            // get the races first 
            await LoadRacesAsync();

            // auto select the first 
            cb_Race.SelectedIndex = 0;

            await FillStuffPlease();


            this.Visible = true;
        }

        private async Task FillStuffPlease() {

            //this.Visible = false;
            // get the races first 
            await LoadRacesAsyncAgain();




            this.Visible = true;

        }


        private void validate(object sender, EventArgs e)
        {
            FillStuffPlease();

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

            if (int.Parse(TeamASkierOne) < 0 || int.Parse(TeamASkierTwo) < 0 || int.Parse(TeamBSkierOne) < 0 || int.Parse(TeamBSkierTwo) < 0)
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
                        lb_TeamOne.Text = m.teamA;
                        lb_TeamTwo.Text = m.teamB;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading races: " + ex.Message);
            }
        }

        private async Task LoadRacesAsyncAgain()
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
                        lb_TeamOne.Text = m.teamA;
                        lb_TeamTwo.Text = m.teamB;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading races: " + ex.Message);
            }
        }

        private void PromptTimes_Load(object sender, EventArgs e)
        {

        }
    }
}
