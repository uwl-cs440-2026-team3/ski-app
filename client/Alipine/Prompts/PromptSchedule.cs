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
    public partial class PromptSchedule : Form
    {
        public string RaceName => tb_Name.Text;
        public string TeamA => cb_TeamOne.Text;
        public string TeamB => cb_TeamTwo.Text;
        public string CourseName => cb_Course.Text;

        public string DateTimeMe = "-1";

        public string Minutes = "-1";

        private List<string> allTeams = new();
        private bool updating = false;

        public PromptSchedule()
        {
            InitializeComponent();

            this.Text = "Schedule a race";

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            dtp_Date.MinDate = DateTime.Now; // cant be before now 
            this.Visible = false; // we dont show stuff until we get our data from the server
            initMe();

            // TODO database should never have ONLY one team but we should CHECK for that


        }

        private async Task initMe()
        {
            await LoadTeamsAsync();
            await LoadCoursesAsync();

            // auto select the first and second items
            cb_TeamOne.SelectedIndex = 0;
            cb_TeamTwo.SelectedIndex = cb_TeamTwo.Items.Count - 1;
            cb_Course.SelectedIndex = 0;


            this.Visible = true;
        }


        private void validate(object sender, EventArgs e)
        {


        }

        private void cb_TeamOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTeamTwoItems();
        }

        private void cb_TeamTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTeamOneItems();
        }

        // thanks chatgpt

        private void UpdateTeamTwoItems()
        {
            if (updating) return;
            updating = true;

            string keepSelection = cb_TeamOne.SelectedItem?.ToString();
            string selectedTwo = cb_TeamTwo.SelectedItem?.ToString();

            cb_TeamOne.Items.Clear();

            foreach (string s in allTeams)
            {
                if (s != selectedTwo)
                    cb_TeamOne.Items.Add(s);
            }

            if (keepSelection != null && cb_TeamOne.Items.Contains(keepSelection))
                cb_TeamOne.SelectedItem = keepSelection;
            else if (cb_TeamOne.Items.Count > 0)
                cb_TeamOne.SelectedIndex = 0;

            updating = false;
        }

        private void UpdateTeamOneItems()
        {
            if (updating) return;
            updating = true;

            string keepSelection = cb_TeamTwo.SelectedItem?.ToString();
            string selectedOne = cb_TeamOne.SelectedItem?.ToString();

            cb_TeamTwo.Items.Clear();

            foreach (string s in allTeams)
            {
                if (s != selectedOne)
                   cb_TeamTwo.Items.Add(s);
            }

            if (keepSelection != null && cb_TeamTwo.Items.Contains(keepSelection))
                cb_TeamTwo.SelectedItem = keepSelection;
            else if (cb_TeamTwo.Items.Count > 0)
                cb_TeamTwo.SelectedIndex = 0;

            updating = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // this.... PROBABLY should never be able to happen, but whatever
            if (TeamA == "" || TeamB == "" || CourseName == "" || RaceName == "")
            {
                MessageBox.Show(
                    "Fields cannot be empty!",
                    "Fields cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (TeamA == TeamB)
            {
                MessageBox.Show(
                    "Teams cannot be the same!",
                    "Teams cannot be the same!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }


            // thanks chatgpt
            DateTime dateOnly = dtp_Date.Value.Date;
            TimeSpan startTime = dtp_Start.Value.TimeOfDay;
            TimeSpan endTime = dtp_End.Value.TimeOfDay;

            DateTime combinedStart = dateOnly + startTime;
            DateTimeMe = combinedStart.ToString("yyyy-MM-ddTHH:mm");

            int lengthMinutes = (int)Math.Round((endTime - startTime).TotalMinutes);
            Minutes = lengthMinutes.ToString();

            // TODO check time every time user picks a time, cant be negative

            // TODO date should be past today always

            // TODO need to validate 
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // thanks slop bot for some of the help but also you suck ass
        private async Task LoadTeamsAsync()
        {
            try
            {
                // get our json response
                RequestHelpers request = new();
                string json = await request.PostRequestTeams();

                // deserialize it into the class whatever
                var deserialized = JsonSerializer.Deserialize<List<string>>(json); // TODO this is a bit different then before 

                // make sure it isnt null
                if (deserialized != null)
                {
                    allTeams.Clear();
                    foreach (var m in deserialized)
                    {
                        allTeams.Add(m);
                    }
                    cb_TeamOne.Items.Clear();
                    cb_TeamTwo.Items.Clear();

                    cb_TeamOne.Items.AddRange(allTeams.ToArray());
                    cb_TeamTwo.Items.AddRange(allTeams.ToArray());

                    cb_TeamOne.SelectedIndex = 0;
                    cb_TeamTwo.SelectedIndex = cb_TeamTwo.Items.Count - 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
            }
        }

        private async Task LoadCoursesAsync()
        {
            try
            {
                // get our json response
                RequestHelpers request = new();
                string json = await request.PostRequestCourses();

                // deserialize it into the class whatever
                var deserialized = JsonSerializer.Deserialize<List<string>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    foreach (var m in deserialized)
                    {
                        cb_Course.Items.Add(m);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
            }
        }
    }
}
