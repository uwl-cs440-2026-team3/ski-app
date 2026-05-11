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
    // prompt form for scheduling races
    public partial class PromptSchedule : Form
    {
        public string RaceName => tb_Name.Text;
        public string TeamA => cb_TeamOne.Text;
        public string TeamB => cb_TeamTwo.Text;
        public string CourseName => cb_Course.Text;

        public string DateTimeMe = "-1";

        public string Minutes = "-1";

        // list of all teams
        private List<string> allTeams = new();

        // locking system
        private bool updating = false;

        public PromptSchedule()
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;


            this.Text = "Schedule a race";

            // we start from the center of the main form
            StartPosition = FormStartPosition.CenterParent;


            AcceptButton = btnOk;
            CancelButton = btnCancel;

            dtp_Date.MinDate = DateTime.Now; // cant be before now 
            this.Visible = false; // we dont show stuff until we get our data from the server
            initMe();


        }

        // goes through a few steps to load data from the server
        private async Task initMe()
        {
            // load our teams
            await LoadTeamsAsync();

            // load our courses
            await LoadCoursesAsync();

            // auto select the first and second items
            cb_TeamOne.SelectedIndex = 0;
            cb_TeamTwo.SelectedIndex = cb_TeamTwo.Items.Count - 1;
            cb_Course.SelectedIndex = 0;


            this.Visible = true; // finally show this form
        }

        // guided by chatgpt, the following methods are what make it so you cannot select the same skier in both fields

        // whenever we select an item from the first combobox
        private void cb_TeamOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTeamTwoItems_(); // we update the second comboboxes items
        }

        private void cb_TeamTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTeamTwoItems(); // we update the first comboboxes items
        }

        // thanks chatgpt

        // we update the first teams
        private void UpdateTeamTwoItems_()
        {
            // if we are alreadying updating do not try to update
            if (updating)
            {
                return;
            }

            // lock the door
            updating = true;

            // keep track of which ones are selected
            string keepSelection = cb_TeamOne.SelectedItem?.ToString();
            string selectedTwo = cb_TeamTwo.SelectedItem?.ToString();

            // remove all items from the first check box
            cb_TeamOne.Items.Clear();

            // for each team
            foreach (string s in allTeams)
            {
                // as long as it is not the one that was selected in the second combobox
                if (s != selectedTwo)
                    cb_TeamOne.Items.Add(s);
            }

            // if we found a selection from before and the combobox actually has that item
            if (keepSelection != null && cb_TeamOne.Items.Contains(keepSelection))
            {
                cb_TeamOne.SelectedItem = keepSelection; // we reselect that item
            }  
            else if (cb_TeamOne.Items.Count > 0)
            {
                cb_TeamOne.SelectedIndex = 0; // otherwise we just select the first item
            }        
            else // something has gone wrong
            {
                MessageBox.Show("PromptTeam error loading items into first combobox");
                updating = false;
                return;
            }

            // unlock the door
            updating = false;
        }

        // we update the second teams
        private void UpdateTeamTwoItems()
        {
            // if we are alreadying updating do not try to update
            if (updating)
            {
                return;
            }

            // lock the door
            updating = true;

            // keep track of which ones are selected
            string keepSelection = cb_TeamTwo.SelectedItem?.ToString();
            string selectedOne = cb_TeamOne.SelectedItem?.ToString();

            // remove all items from the second check box
            cb_TeamTwo.Items.Clear();

            // for each skier
            foreach (string s in allTeams)
            {
                // as long as it is not the one that was selected in the first combobox
                if (s != selectedOne)
                {
                    cb_TeamTwo.Items.Add(s);
                }

            }

            // if we found a selection from before and the combobox actually has that item
            if (keepSelection != null && cb_TeamTwo.Items.Contains(keepSelection))
            {
                cb_TeamTwo.SelectedItem = keepSelection; // we reselect that item
            }
            else if (cb_TeamTwo.Items.Count > 0)
            {
                cb_TeamTwo.SelectedIndex = 0; // otherwise we just select the first item
            }
            else // something has gone wrong
            {
                MessageBox.Show("PromptSchedule error loading items into first combobox");
                updating = false;
                return;
            }

            // unlock the door
            updating = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // check if stuff is missing first, no use trying to send data we know the server will reject
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

            // check if the teams are the same, no use trying to send data we know the server will reject
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


            // get the dates from the selection as dat time values
            DateTime dateOnly = dtp_Date.Value.Date;
            TimeSpan startTime = dtp_Start.Value.TimeOfDay;
            TimeSpan endTime = dtp_End.Value.TimeOfDay;

            // combine the date and start time to get the starting date and time
            DateTime combinedStart = dateOnly + startTime;
            DateTimeMe = combinedStart.ToString("yyyy-MM-ddTHH:mm");

            // get the length from the two times
            int lengthMinutes = (int)Math.Round((endTime - startTime).TotalMinutes);
            Minutes = lengthMinutes.ToString();

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
                    // clear out the gathered teams
                    allTeams.Clear();

                    // for each team
                    foreach (var t in deserialized)
                    {
                        allTeams.Add(t); // add this team to the gathered teams
                    }

                    // clear out the comboboxes
                    cb_TeamOne.Items.Clear();
                    cb_TeamTwo.Items.Clear();

                    // add the new items to the list
                    cb_TeamOne.Items.AddRange(allTeams.ToArray());
                    cb_TeamTwo.Items.AddRange(allTeams.ToArray());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
            }
        }

        // load courses into the ui
        private async Task LoadCoursesAsync()
        {
            try
            {
                // send a request to the server and get the response
                RequestHelpers request = new();
                string json = await request.PostRequestCourses();

                // deserialize it into a list of strings
                var deserialized = JsonSerializer.Deserialize<List<string>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    // for each course
                    foreach (var c in deserialized)
                    {
                        // add it to the combobox
                        cb_Course.Items.Add(c);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
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
