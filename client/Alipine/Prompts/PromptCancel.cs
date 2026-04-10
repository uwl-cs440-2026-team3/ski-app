using Alpine.Helpers;
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
    public partial class PromptCancel : Form
    {
        public string Value1 => cb_Race.Text;

        public PromptCancel(string title, string lbl1)
        {
            InitializeComponent();

            Text = title;

            label1.Text = lbl1;

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
            await LoadRacesAsync();

            // auto select the first and second items
            cb_Race.SelectedIndex = 0;
        }

        private async Task LoadRacesAsync()
        {
            try
            {
                // get our json response
                RequestHelpers request = new();
                string json = await request.PostRequestRaces();

                // deserialize it into the class whatever
                var deserialized = JsonSerializer.Deserialize<List<Member>>(json);

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

        private void btnOk_Click(object sender, EventArgs e)
        {
            // fields cannot be empty, why wait for the server to tell us this when we can nip it in the bud right here
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
    }
}
