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
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static Alpine.Helpers.RequestHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Alpine
{
    public partial class PromptTeam : Form
    {
        public string TeamName => tb_name.Text;
        public string Coach => cb_Coach.Text;
        public string FirstSkier => cb_SkierOne.Text;
        public string SecondSkier => cb_SkierTwo.Text;

        private List<string> allSkiers = new();
        private bool updating = false;

        public PromptTeam()
        {
            InitializeComponent();
            this.Text = "Create a team.";

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            this.Visible = false; // we dont show stuff until we get our data from the server
            initMe();

        }

        private async Task initMe()
        {
            await LoadMembersAsync();

            // auto select the first and last items
            cb_Coach.SelectedIndex = 0;
            cb_SkierOne.SelectedIndex = 0;
            cb_SkierTwo.SelectedIndex = cb_SkierTwo.Items.Count - 1;


            this.Visible = true;
        }

        private void validate(object sender, EventArgs e)
        {
            // TODO WE SHOULD VALIDATE HERE BUT I DONT HAVE TIME RIGHT NOW HAHA
        }

        // thanks chatgpt...
        private void cb_SkierOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSkierTwoItems();
        }

        private void cb_SkierTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSkierOneItems();
        }

        private void UpdateSkierOneItems()
        {
            if (updating) return;
            updating = true;

            string keepSelection = cb_SkierOne.SelectedItem?.ToString();
            string selectedTwo = cb_SkierTwo.SelectedItem?.ToString();

            cb_SkierOne.Items.Clear();

            foreach (string s in allSkiers)
            {
                if (s != selectedTwo)
                    cb_SkierOne.Items.Add(s);
            }

            if (keepSelection != null && cb_SkierOne.Items.Contains(keepSelection))
                cb_SkierOne.SelectedItem = keepSelection;
            else if (cb_SkierOne.Items.Count > 0)
                cb_SkierOne.SelectedIndex = 0;

            updating = false;
        }

        private void UpdateSkierTwoItems()
        {
            if (updating) return;
            updating = true;

            string keepSelection = cb_SkierTwo.SelectedItem?.ToString();
            string selectedOne = cb_SkierOne.SelectedItem?.ToString();

            cb_SkierTwo.Items.Clear();

            foreach (string s in allSkiers)
            {
                if (s != selectedOne)
                    cb_SkierTwo.Items.Add(s);
            }

            if (keepSelection != null && cb_SkierTwo.Items.Contains(keepSelection))
                cb_SkierTwo.SelectedItem = keepSelection;
            else if (cb_SkierTwo.Items.Count > 0)
                cb_SkierTwo.SelectedIndex = 0;

            updating = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

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
        private async Task LoadMembersAsync()
        {
            try
            {
                // get our json response
                RequestHelpers request = new();
                string json = await request.PostRequestMembers();

                // deserialize it into the class whatever
                var deserialized = JsonSerializer.Deserialize<List<Member>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    //allSkiers.Clear();
                    foreach (var m in deserialized)
                    {
                        if (m.role.Equals("coach"))
                        {
                            cb_Coach.Items.Add(m.name);
                        }
                        else if (m.role.Equals("skier"))
                        {
                            allSkiers.Add(m.name);
                        }
                    }
                    cb_SkierOne.Items.Clear();
                    cb_SkierTwo.Items.Clear();

                    cb_SkierOne.Items.AddRange(allSkiers.ToArray());
                    cb_SkierTwo.Items.AddRange(allSkiers.ToArray());

                    cb_SkierOne.SelectedIndex = 0;
                    cb_SkierTwo.SelectedIndex = cb_SkierTwo.Items.Count - 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message);
            }
        }
    }
 
}
