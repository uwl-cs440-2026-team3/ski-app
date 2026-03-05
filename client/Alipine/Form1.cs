using System.Diagnostics;
using System.Media;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net.Http.Headers;

namespace Alipine
{
    public partial class Form1 : Form
    {
        // we need to hold the token we get
        String token = "";

        // we will hold the user we get
        String role = "";

        // dotnet add package System.Net.Http.Json --version 10.0.3

        // HttpClient lifecycle management best practices:
        // https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
        public static HttpClient sharedClient = new(
                // chatgpt, WE BYPASS SSL CERT FOR NOW FIX THIS LATER DO NOT LEAVE THIS PAST DEMOING
                new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }
                )
        {
            BaseAddress = new Uri("https://localhost:1041/"),
        };

        public Form1()
        {
            InitializeComponent();
        }

        private async void btn_SkierRegistration_Click(object sender, EventArgs e)
        {
            // attempt to send to server
            await PostRegister(sharedClient, rtb_RegisterEmail.Text, rtb_RegisterUsername.Text, rtb_RegisterPassword.Text);
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            // attempt to send to login
            await PostLogin(sharedClient, rtb_loginEmail.Text, rtb_loginPassword.Text);

        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        async Task PostRegister(HttpClient httpClient, String email, String name, String password)
        {
            // we can check our data here
            // sql sanitization? check email? make sure stuff makes sense?

            var user = new
            {
                email = email,
                name = name,
                password = password
            };

            // send our post
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync("register", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            else
            {
                // if we were sucessful we clear these fields, otherwise we leave them so the user can try again(?)
                rtb_RegisterEmail.Text = "";
                rtb_RegisterPassword.Text = "";
                rtb_RegisterUsername.Text = "";
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        async Task PostLogin(HttpClient httpClient, String email, String password)
        {
            // we can check our data here
            // sql sanitization? check email? make sure stuff makes sense?

            var user = new
            {
                email = email,
                password = password
            };

            // send our post
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync("login", user);


            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");

            // you ONLY get to log in and unlock controls if we get an okay, #leastprivlege
            if (response.StatusCode != HttpStatusCode.OK)
            {
                // alert the user that login failed
                SystemSounds.Exclamation.Play();

                return;
            }


            // we get the token and the type of user here
            using JsonDocument tempJson = JsonDocument.Parse(responseBody);
            JsonElement tempObj = tempJson.RootElement;
            role = tempObj.GetProperty("role").ToString();
            token = tempObj.GetProperty("token").ToString();

            // okay since we know we logged in right here we can keep going

            // unlock controls, etc, etc

            // we don't actually know stuff for now!

            // we enable and disable depending on what should be seen now
            rtb_RegisterPassword.Enabled = false;
            rtb_RegisterEmail.Enabled = false;
            rtb_RegisterUsername.Enabled = false;
            btn_Register.Enabled = false;
            rtb_loginEmail.Enabled = false;
            rtb_loginPassword.Enabled = false;

            btn_login.Enabled = false;
            rtb_loginEmail.Text = "";
            rtb_loginPassword.Text = "";

            btn_logout.Enabled = true;
            lb_role.Text = "Logged in as " + role;


            // unlock stuff for roles
            switch (role)
            {
                case "admin":
                    rtb_Team.Enabled = true;
                    btn_CreateTeam.Enabled = true;
                    rtb_Course.Enabled = true;
                    btn_CreateCourse.Enabled = true;
                    rtb_AssignPersonName.Enabled = true;
                    rtb_AssignTeamName.Enabled = true;
                    btn_AssignToTeam.Enabled = true;
                    rtb_RaceName.Enabled = true;
                    rtb_TeamOne.Enabled = true;
                    rtb_TeamTwo.Enabled = true;
                    rtb_Date.Enabled = true;
                    rtb_TimeStart.Enabled = true;
                    rtb_TimeEnd.Enabled = true;
                    rtb_CourseName.Enabled = true;
                    btn_ScheduleRace.Enabled = true;
                    rtb_CoachEmail.Enabled = true;
                    rtb_CoachPassword.Enabled = true;
                    rtb_CoachUser.Enabled = true;
                    btn_CreateCoach.Enabled = true;
                    break;
                case "coach":
                    break;
                case "skier":
                    break;
                default:
                    // ???? this should never happen, 
                    SystemSounds.Exclamation.Play();
                    break;
            }

            // yay!!
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            // landing
            lb_role.Text = "";
            rtb_log.Text = "";
            // bascially we reset everything
            token = "";
            role = "";
            btn_logout.Enabled = false;
            btn_login.Enabled = true;
            rtb_loginEmail.Enabled = true;
            rtb_loginPassword.Enabled = true;
            rtb_RegisterPassword.Enabled = true;
            rtb_RegisterEmail.Enabled = true;
            rtb_RegisterUsername.Enabled = true;
            btn_Register.Enabled = true;

            //admin
            rtb_Team.Enabled = false;
            rtb_Team.Text = "";
            btn_CreateTeam.Enabled = false;
            rtb_Course.Enabled = false;
            rtb_Course.Text = "";
            btn_CreateCourse.Enabled = false;
            rtb_AssignPersonName.Enabled = false;
            rtb_AssignPersonName.Text = "";
            rtb_AssignTeamName.Enabled = false;
            rtb_AssignTeamName.Text = "";
            btn_AssignToTeam.Enabled = false;
            rtb_RaceName.Enabled = false;
            rtb_RaceName.Text = "";
            rtb_TeamOne.Enabled = false;
            rtb_TeamOne.Text = "";
            rtb_TeamTwo.Enabled = false;
            rtb_TeamTwo.Text = "";
            rtb_Date.Enabled = false;
            rtb_Date.Text = "";
            rtb_TimeStart.Enabled = false;
            rtb_TimeStart.Text = "";
            rtb_TimeEnd.Enabled = false;
            rtb_TimeEnd.Text = "";
            rtb_CourseName.Enabled = false;
            rtb_CourseName.Text = "";
            btn_ScheduleRace.Enabled = false;

            rtb_CoachEmail.Enabled = false;
            rtb_CoachPassword.Enabled = false;
            rtb_CoachUser.Enabled = false;
            rtb_CoachEmail.Text = "";
            rtb_CoachPassword.Text = "";
            rtb_CoachUser.Text = "";
            btn_CreateCoach.Enabled = false;
        }

        // admin zone

        private async void btn_CreateTeam_Click(object sender, EventArgs e)
        {
            await PostTeam(sharedClient, rtb_Team.Text);
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        async Task PostTeam(HttpClient httpClient, String name)
        {
            // we can check our data here

            // attach the auth token
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync("team", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            else
            {
                // if we were sucessful we clear these fields, otherwise we leave them so the user can try again(?)
                rtb_Team.Text = "";
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }
        private async void btn_CreateCourse_Click(object sender, EventArgs e)
        {
            await PostCourse(sharedClient, rtb_Course.Text);
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        async Task PostCourse(HttpClient httpClient, String name)
        {
            // we can check our data here

            // attach the auth token
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync("course", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            else
            {
                // if we were sucessful we clear these fields, otherwise we leave them so the user can try again(?)
                rtb_Course.Text = "";
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

        private async void btn_AssignToTeam_Click(object sender, EventArgs e)
        {
            await PostTeamAssign(sharedClient, rtb_AssignPersonName.Text, rtb_AssignTeamName.Text);
        }

        async Task PostTeamAssign(HttpClient httpClient, String name, String teamName)
        {
            // we can check our data here

            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync("register", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            else
            {
                // if we were sucessful we clear these fields, otherwise we leave them so the user can try again(?)
                rtb_AssignPersonName.Text = "";
                rtb_AssignTeamName.Text = "";
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }
        private void btn_ScheduleRace_Click(object sender, EventArgs e)
        {

        }

        private async void btn_CreateCoach_Click(object sender, EventArgs e)
        {
            await PostRegisterCoach(sharedClient, rtb_CoachEmail.Text, rtb_CoachUser.Text, rtb_CoachPassword.Text);
        }
        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        async Task PostRegisterCoach(HttpClient httpClient, String email, String name, String password)
        {
            // we can check our data here
            // sql sanitization? check email? make sure stuff makes sense?

            // attach the auth token
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var user = new
            {
                email = email,
                name = name,
                password = password
            };

            // send our post
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync("registercoach", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            else
            {
                // if we were sucessful we clear these fields, otherwise we leave them so the user can try again(?)
                rtb_RegisterEmail.Text = "";
                rtb_RegisterPassword.Text = "";
                rtb_RegisterUsername.Text = "";
            }
            rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            rtb_log.AppendText(responseBody + "\n");
        }

    }
}
