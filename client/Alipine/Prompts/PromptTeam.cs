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

    // prompt form for entering teams
    public partial class PromptTeam : Form
    {
        public string TeamName => tb_name.Text;
        public string Coach => cb_Coach.Text;
        public string FirstSkier => cb_SkierOne.Text;
        public string SecondSkier => cb_SkierTwo.Text;

        // list of all the skiers
        private List<string> allSkiers = new();

        // locking system
        private bool updating = false;

        public PromptTeam()
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;

            this.Text = "Create a team.";

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
            await LoadMembersAsync();

            // auto select the first and last items
            cb_Coach.SelectedIndex = 0;
            cb_SkierOne.SelectedIndex = 0;
            cb_SkierTwo.SelectedIndex = cb_SkierTwo.Items.Count - 1;

            
            this.Visible = true; // finally show this form
        }

        // guided by chatgpt, the following methods are what make it so you cannot select the same skier in both fields

        // whenever we select an item from the first combobox
        private void cb_SkierOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSkierTwoItems(); // we update the second comboboxes items
        }

        // whenever we select an item from the second combobox
        private void cb_SkierTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSkierOneItems(); // we update the first comboboxes items
        }

        // we update the first skiers
        private void UpdateSkierOneItems()
        {
            // if we are alreadying updating do not try to update
            if (updating)
            {
                return;
            }

            // lock the door
            updating = true;

            // keep track of which ones are selected
            string keepSelection = cb_SkierOne.SelectedItem?.ToString();
            string selectedTwo = cb_SkierTwo.SelectedItem?.ToString();

            // remove all items from the first check box
            cb_SkierOne.Items.Clear();

            // for each skier
            foreach (string s in allSkiers)
            {
                // as long as it is not the one that was selected in the second combobox
                if (s != selectedTwo)
                {
                    cb_SkierOne.Items.Add(s); // add it to the combobox
                }
                    
            }

            // if we found a selection from before and the combobox actually has that item
            if (keepSelection != null && cb_SkierOne.Items.Contains(keepSelection))
            {
                cb_SkierOne.SelectedItem = keepSelection; // we reselect that item
            }
            else if (cb_SkierOne.Items.Count > 0)
            {
                cb_SkierOne.SelectedIndex = 0; // otherwise we just select the first item
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

        // we update the second skiers
        private void UpdateSkierTwoItems()
        {
            // if we are alreadying updating do not try to update
            if (updating)
            {
                return;
            }

            // lock the door
            updating = true;

            // keep track of which ones are selected
            string keepSelection = cb_SkierTwo.SelectedItem?.ToString();
            string selectedOne = cb_SkierOne.SelectedItem?.ToString();

            // remove all items from the second check box
            cb_SkierTwo.Items.Clear();

            // for each skier
            foreach (string s in allSkiers)
            {
                // as long as it is not the one that was selected in the first combobox
                if (s != selectedOne)
                {
                    cb_SkierTwo.Items.Add(s); // add it to the combobox
                }
            }

            // if we found a selection from before and the combobox actually has that item
            if (keepSelection != null && cb_SkierTwo.Items.Contains(keepSelection))
            {
                cb_SkierTwo.SelectedItem = keepSelection; // we reselect that item
            }

            else if (cb_SkierTwo.Items.Count > 0)
            {
                cb_SkierTwo.SelectedIndex = 0; // otherwise we just select the first item
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

        // check some stuff when we click okay
        private void btnOk_Click(object sender, EventArgs e)
        {

            // check if stuff is missing first, no use trying to send data we know the server will reject
            if (TeamName == "" || Coach == "" || FirstSkier == "" || SecondSkier == "")
            {
                MessageBox.Show(
                    "Fields cannot be empty!",
                    "Fields cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // check if the skiers are the same, no use trying to send data we know the server will reject
            if (FirstSkier == SecondSkier)
            {
                MessageBox.Show(
                    "Skiers cannot be the same!",
                    "Skiers cannot be the same!",
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
                        if (m.role.Equals("coach")) // if it is a coach we add it to the coach box
                        {
                            cb_Coach.Items.Add(m.name);
                        }
                        else if (m.role.Equals("skier")) // if it is a skier we add it to our skier list
                        {
                            allSkiers.Add(m.name);
                        }
                    }

                    // clear out the comboboxes
                    cb_SkierOne.Items.Clear();
                    cb_SkierTwo.Items.Clear();

                    // add the new items to the list
                    cb_SkierOne.Items.AddRange(allSkiers.ToArray());
                    cb_SkierTwo.Items.AddRange(allSkiers.ToArray());

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
