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
    public partial class Admin : Form
    {

        public Admin()
        {
            InitializeComponent();
            lb_name.Text = "Hello " + Globals.Name + "!";
        }

        #region buttons

        async private void btn_CreateTeam_Click(object sender, EventArgs e)
        {
            // we create the prompt, and it will get the result
            var result = PromptTeamHelperClass.Show();

            if (result.ok)
            {
                await PostTeam(result.teamName, result.firstSkier, result.secondSkier, result.coach);
            }

        }
        async private void btn_CreateCourse_Click(object sender, EventArgs e)
        {
            // we create the prompt, and it will get the result
            var result = PromptSingleHelperClass.Show(
                "Create Course",
                "Course Name:"
            );

            if (result.ok)
            {
                await PostCourse(result.a);
            }

        }

        async private void btn_CreateCoach_Click(object sender, EventArgs e)
        {
            // we create the prompt, and it will get the result
            var result = PromptCoachHelperClass.Show();

            if (result.ok)
            {
                await PostRegisterCoach(result.email, result.username, result.password);
            }
        }

        async private void btn_ScheduleRace_Click(object sender, EventArgs e)
        {
            // we create the prompt, and it will get the result
            var result = PromptScheduleHelperClass.Show();

            if (result.ok)
            {
                await PostScheduleRace(result.name, result.teama, result.teamb, result.courseName, result.dateTime, result.minutes);
            }
        }

        private async void btn_Cancel_Click(object sender, EventArgs e)
        {
            // we create the prompt, and it will get the result
            var result = PromptCancelHelperClass.Show(
                "Cancel Race",
                "Race Name:"
            );

            if (result.ok)
            {
                await PostCancel(result.a);
            }
        }

        async private void btn_InsertTimes_Click(object sender, EventArgs e)
        {
            // we create the prompt, and it will get the result
            var result = PromptTimesHelperClass.Show();

            if (result.ok)
            {
                await PostTimes(result.raceName, result.TeamASkierOne, result.TeamASkierTwo, result.TeamBSkierThree, result.TeamBSkierFour);
            }
        }


        async private void btn_LogOut_Click(object sender, EventArgs e)
        {
            // tell it everything went okay and close
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion


        // TODO these should be externalized like our requests at some point
        #region endpoints

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostTeam(String name, String skier1, String skier2, String coach)
        {
            // we can check our data here
            String coachEmail = "";
            String skierOneEmail = "";
            String skierTwoEmail = "";

            // this sucks do it better later..... store members as objects in combo boxes and then we can just get the fields directly instead of re asking the server TODO
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
                        if (m.name == coach)
                        {
                            coachEmail = m.email;
                        }
                        if (m.name == skier1)
                        {
                            skierOneEmail = m.email;
                        }
                        if (m.name == skier2)
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

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
                skier1_email = skierOneEmail,
                skier2_email = skierTwoEmail,
                coach_email = coachEmail
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("team", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostCourse(String name)
        {
            // we can check our data here

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("course", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostRegisterCoach(String email, String name, String password)
        {
            // we can check our data here
            // sql sanitization? check email? make sure stuff makes sense?

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                email = email,
                name = name,
                password = password
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("registercoach", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostScheduleRace(String name, String team_a, String team_b, String course_name, String datetime, String minutes)
        {
            // we can check our data here
            // sql sanitization? check email? make sure stuff makes sense?

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
                team_a = team_a,
                team_b = team_b,
                course = course_name,
                start = datetime,
                duration = minutes
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("schedule", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText($"Status: {(int)response.StatusCode} {response.StatusCode}\n");
            rtb_log.AppendText($"Response: {responseBody}\n");
        }

        private async Task PostCancel(String name)
        {
            // we can check our data here

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("cancel", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        private async Task PostTimes(String name, String skier1, String skier2, String skier3, String skier4)
        {
            // we can check our data here

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            // how ever this end point will work
            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("cancel", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        #endregion




    }

}
