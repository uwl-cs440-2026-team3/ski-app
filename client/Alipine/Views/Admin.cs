using Alpine.Helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using static Alpine.Helpers.RequestHelpers;

namespace Alpine
{
    // the form the admin sees when they log in
    public partial class Admin : Form
    {
        // we need this form to have an event it calls on finished, this is what makes switching between forms in the mainform work
        public event Action OnLogout;
        public Admin()
        {
            InitializeComponent();
            lb_name.Text = "Hello " + Globals.Name + "!"; // greet the user in the top left
        }

        #region buttons

        // we send to the server to create a team
        async private void btn_CreateTeam_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/team"; // keep track of what form we are in for the manual

            // show the prompt, get its return
            var result = PromptTeamHelperClass.Show();


            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual
            if (result.ok)
            {
                // for holding the emails we get back from the server
                String coachEmail = "";
                String skierOneEmail = "";
                String skierTwoEmail = "";

                // TODO:: ideally i could make this better by having stored the members as objects in the combo box the first time we got them, and i would not have to requery the server
                // sadly i am running out of time though
                try
                {
                    // get our json response
                    RequestHelpers request = new();
                    string json = await request.PostRequestMembers();

                    // deserialize it into a member
                    var deserialized = JsonSerializer.Deserialize<List<Member>>(json);

                    // make sure it isnt null
                    if (deserialized != null)
                    {
                        // for each user, if there name is of one of our three, we take the email
                        foreach (var m in deserialized)
                        {
                            if (m.name == result.coach)
                            {
                                coachEmail = m.email;
                            }
                            if (m.name == result.firstSkier)
                            {
                                skierOneEmail = m.email;
                            }
                            if (m.name == result.secondSkier)
                            {
                                skierTwoEmail = m.email;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading members: " + ex.Message);
                }

                // we send our post to the server
                var responseHttp = await PostHelpers.PostTeam(result.teamName, skierOneEmail, skierTwoEmail, coachEmail);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }

        }
        async private void btn_CreateCourse_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/course"; // keep track of what form we are in for the manual

            // show the prompt, get its return
            var result = PromptSingleHelperClass.Show(
                "Create Course",
                "Course Name:"
            );

            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual

            // if the prompt closed via "ok"
            if (result.ok)
            {
                // we send our post to the server
                var responseHttp = await PostHelpers.PostCourse(result.a);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }

        }
        async private void btn_CreateCoach_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/coach"; // keep track of what form we are in for the manual

            // show the prompt, get its return
            var result = PromptCoachHelperClass.Show();

            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual

            // if the prompt closed via "ok"
            if (result.ok)
            {
                // we send our post to the server
                var responseHttp = await PostHelpers.PostRegisterCoach(result.email, result.username, result.password);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }
        }
        async private void btn_ScheduleRace_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/schedule"; // keep track of what form we are in for the manual

            // show the prompt, get its return
            var result = PromptScheduleHelperClass.Show();

            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual

            // if the prompt closed via "ok"
            if (result.ok)
            {
                // we send our post to the server
                var responseHttp = await PostHelpers.PostScheduleRace(result.name, result.teama, result.teamb, result.courseName, result.dateTime, result.minutes);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }
        }

        private async void btn_Cancel_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/cancel";

            // show the prompt, get its return
            var result = PromptCancelHelperClass.Show(
                "Cancel Race",
                "Race Name:"
            );

            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual

            // if the prompt closed via "ok"
            if (result.ok)
            {
                // we send our post to the server
                var responseHttp = await PostHelpers.PostCancel(result.a);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }
        }

        async private void btn_InsertTimes_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/times";

            // show the prompt, get its return
            var result = PromptTimesHelperClass.Show();

            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual

            // if the prompt closed via "ok"
            if (result.ok)
            {
                // we send our post to the server
                var responseHttp = await PostHelpers.PostTimes(result.raceName, result.TeamASkierOne, result.Time);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }
        }

        async private void btn_RemoveCoach_Click(object sender, EventArgs e)
        {
            Globals.Current_Form = "/admin/removecoach"; // keep track of what form we are in for the manual

            // show the prompt, get its return
            var result = PromptRemoveCoachHelperClass.Show("Select coach to remove", "yeah");

            Globals.Current_Form = "admin"; // keep track of what form we are in for the manual

            // if the prompt closed via "ok"
            if (result.ok)
            {
                // we send our post to the server
                var responseHttp = await PostHelpers.PostRemoveCoach(result.a);

                // then we check if the response was good or not
                ValidationHelpers.responseChecker(responseHttp.status, responseHttp.response);
            }
        }


        async private void btn_LogOut_Click(object sender, EventArgs e)
        {
            // tell it everything went okay and close
            this.DialogResult = DialogResult.OK;
            OnLogout?.Invoke();
            this.Close();
        }
        #endregion
    }

}
