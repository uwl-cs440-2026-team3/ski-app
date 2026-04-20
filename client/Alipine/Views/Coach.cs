using Alpine.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;
using static Alpine.Helpers.RequestHelpers;

namespace Alpine
{
    public partial class Coach : Form
    {

        public Coach()
        {
            InitializeComponent();
            lb_name.Text = "Hello " + Globals.Name + "!";
            PopulateDataGrid();
        }


        private void PopulateDataGrid()
        {
            // we'll do a server request

            // put it into a json

            // then fill the data grid as we view this json

            _ = LoadMyTeamAsync();


        }

        private async Task LoadMyTeamAsync()
        {
            try
            {
                // get our json response
                RequestHelpers request = new();
                string json = await request.PostRequestMembers();

                // deserialize it into the class whatever
                var deserialized = JsonSerializer.Deserialize<List<MyTeam>>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    //allSkiers.Clear();
                    foreach (var m in deserialized)
                    {
                        lb_coach.Text = "vfdsdfb";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message);
            }
        }
    }
}
