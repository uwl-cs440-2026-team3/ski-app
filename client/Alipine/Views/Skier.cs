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

    // the form a skier sees when they log in
    public partial class Skier : Form
    {
        // we need this form to have an event it calls on finished, this is what makes switching between forms in the mainform work
        public event Action OnLogout;

        // we keep track of if the user is on any teams
        private bool doIExistYet = false;
        public Skier()
        {
            InitializeComponent();
            lb_name.Text = "Hello " + Globals.Name + "!"; // greet the user in the top left

            this.Visible = false; // we dont show stuff until we get our data from the server
            
            // load in data from server
            initMe();


        }

        // goes through a few steps to load data from the server
        private async Task initMe()
        {
            await LoadMyTeamAsync();

            if(doIExistYet) 
            {
                await LoadMyRacesAsync();
            }
            else // if the user does not have a team then we do not want to go forth loading stuff
            {
                // get the team information and display it
                lb_coach.Text = "";
                lb_team.Text = "";
                lb_teammates.Text = "";
                label2.Text = "You are not on a team!";
            }

            this.Visible = true;
        }

        // we ask the server for the details on our team
        private async Task LoadMyTeamAsync()
        {
            try
            {
                // query the server for details on our team
                RequestHelpers request = new();
                string json = await request.PostRequestMyTeam();

                // we do not want to try to deserialize and populate if this user has no team
                if (json.Equals("")) 
                {
                    return;
                }

                // deserialize it into a myteam
                var deserialized = JsonSerializer.Deserialize<MyTeam>(json);

                // make sure it isnt null
                if (deserialized != null)
                {
                    // get the team information and display it
                    lb_coach.Text = "Your coach: " + deserialized.coach;
                    lb_team.Text = "Your team: " + deserialized.name;
                    foreach (var m in deserialized.skiers)
                    {
                        lb_teammates.Text += m.ToString() + ", ";
                    }

                    // keep track that we actually do have a team 
                    doIExistYet = true;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading your team: " + ex.Message);
            }
        }

        // we ask the server for our races and then 
        private async Task LoadMyRacesAsync()
        {
            try
            {
                // query the server for our races
                RequestHelpers request = new();
                string json = await request.PostRequestMyRaces();

                // we do not want to try to deserialize and populate if this user has no races
                if (json.Equals(""))
                {
                    return;
                }

                // thanks chat gpt! (we want to reformat our times from the server so we can turn it into a date time object
                json = System.Text.RegularExpressions.Regex.Replace(
                    json,
                    @"(\d{4}-\d{2}-\d{2}) (\d{2}:\d{2}:\d{2})",
                    "$1T$2"
                );

                // deserialize it into a myraces
                var deserialized = JsonSerializer.Deserialize<List<MyRaces>>(json);

                // sort by start date (ascending)
                deserialized = deserialized
                    .OrderBy(r => r.start)
                    .ToList();

                // set the data gridview to use our deserialized json as its data source
                dgv_races.DataSource = deserialized;

                // name the columns
                dgv_races.Columns["name"].HeaderText = "Race Name";
                dgv_races.Columns["teamA"].HeaderText = "Team One";
                dgv_races.Columns["teamB"].HeaderText = "Team Two";
                dgv_races.Columns["course"].HeaderText = "Course";

                // hide these columns so we can redo them
                dgv_races.Columns["start"].Visible = false;
                dgv_races.Columns["end"].Visible = false;

                // add a new column with the date the race is on
                dgv_races.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "date", 
                    HeaderText = "Date",
                    ReadOnly = true
                });

                // add a new column with the time the race starts
                dgv_races.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "time",
                    HeaderText = "Start Time",
                    ReadOnly = true
                });

                // add a new column with the time the race ends
                dgv_races.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "endtime",
                    HeaderText = "End Time",
                    ReadOnly = true
                });

                // go through each item
                foreach (DataGridViewRow row in dgv_races.Rows)
                {
                    if (row.DataBoundItem is MyRaces race)
                    {
                        // get the day
                        row.Cells["date"].Value = race.start.ToString("MM/dd/yyyy");

                        // get the start time
                        row.Cells["time"].Value = race.start.ToString("t");

                        // get the end time
                        row.Cells["endtime"].Value = race.end.ToString("t");
                    }
                }

                // make it so each column is equal size
                dgv_races.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn col in dgv_races.Columns)
                {
                    col.FillWeight = 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message);
            }
        }


        async private void btn_LogOut_Click(object sender, EventArgs e)
        {
            // tell it everything went okay and close
            this.DialogResult = DialogResult.OK;
            OnLogout?.Invoke();
            this.Close();
        }
    }
}
