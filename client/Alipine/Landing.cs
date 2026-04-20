using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Alpine
{
    public partial class Landing : Form
    {
        public Landing()
        {
            InitializeComponent();
            Globals.LoadSSL();


        }

        #region buttons

        private async void LoginCLick(object sender, EventArgs e)
        {
            // check if stuff is missing first
            if (tb_Email.Text == "" || mtb_Password.Text == "")
            {
                MessageBox.Show(
                    "Email and Username cannot be empty!",
                    "Email and Username cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            else if (!(Alpine.Helpers.ValidationHelpers.CheckEmail(tb_Email.Text) && Alpine.Helpers.ValidationHelpers.CheckPassword(mtb_Password.Text)))
            {
                return;
            }

            await PostLogin(tb_Email.Text, mtb_Password.Text);
        }

        private async void RegisterClick(object sender, EventArgs e)
        {
            // check if stuff is missing first
            if (tb_Email.Text == "" || mtb_Password.Text == "" || tb_Username.Text == "")
            {
                MessageBox.Show(
                    "Fields cannot be empty!",
                    "Fields cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            else if (!(Alpine.Helpers.ValidationHelpers.CheckEmail(tb_Email.Text) && Alpine.Helpers.ValidationHelpers.CheckName(tb_Username.Text) &&  Alpine.Helpers.ValidationHelpers.CheckPassword(mtb_Password.Text)))
            {
                return;
            }

            await PostRegister(tb_Email.Text, tb_Username.Text, mtb_Password.Text);
        }


        #endregion

        #region endpoints

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostRegister(String email, String name, String password)
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
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("register", user);


            // log the response code we get
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that a conflict occured
                SystemSounds.Exclamation.Play();
            }
            else
            {
                // if we were sucessful we clear these fields, otherwise we leave them so the user can try again(?)
                tb_Email.Text = "";
                tb_Username.Text = "";
                mtb_Password.Text = "";

            }
            // rtb_log.AppendText(response.StatusCode.ToString() + "\n");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // log it
            // rtb_log.AppendText(responseBody + "\n");
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostLogin(String email, String password)
        {
            // we can check our data here
            // sql sanitization? check email? make sure stuff makes sense?

            var user = new
            {
                email = email,
                password = password
            };

            try
            {
                // send our post
                using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("login", user);

                // rtb_log.AppendText(response.StatusCode.ToString() + "\n");

                // await the rest of the response text
                string responseBody = await response.Content.ReadAsStringAsync();

                // log it
                // rtb_log.AppendText(responseBody + "\n");

                // you ONLY get to log in and unlock controls if we get an okay, #leastprivlege
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // alert the user that login failed
                    MessageBox.Show("Login failed.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


                    return;
                }

                // we get the token and the type of user here
                using JsonDocument tempJson = JsonDocument.Parse(responseBody);
                JsonElement tempObj = tempJson.RootElement;
                Globals.Role = tempObj.GetProperty("role").ToString();
                Globals.Token = tempObj.GetProperty("token").ToString();

                // want this for display purposes later
                Globals.Name = tb_Email.Text;

                // we'll clear some fields and make this home our own
                tb_Email.Text = "";
                tb_Username.Text = "";
                mtb_Password.Text = "";

                // unlock stuff for roles
                switch (Globals.Role)
                {

                    case "admin":

                        // we open the admin form
                        Admin admin = new Admin();

                        // show as modal
                        if (admin.ShowDialog() == DialogResult.OK)
                        {
                            // clear our gathered stuff
                            Globals.InitFields();
                        }
                        break;

                    case "coach":
                        // we open the coach form
                        Coach coach = new Coach();

                        // show as modal
                        if (coach.ShowDialog() == DialogResult.OK)
                        {
                            // clear our gathered stuff
                            Globals.InitFields();
                        }
                        break;

                    case "skier":
                        // we open the coach form
                        Skier skier = new Skier();

                        // show as modal
                        if (skier.ShowDialog() == DialogResult.OK)
                        {
                            // clear our gathered stuff
                            Globals.InitFields();
                        }
                        break;

                    default:
                        // ???? this should never happen, 
                        // clear our gathered stuff
                        Globals.InitFields();
                        SystemSounds.Exclamation.Play();
                        break;
                }
            }

            // should catch failing to connect to the server more better eventually
            catch (Exception ex)
            {

                // we'll clear some fields and make this home our own
                tb_Email.Text = "";
                tb_Username.Text = "";
                mtb_Password.Text = "";

                // clear our gathered stuff
                Globals.InitFields();

                MessageBox.Show(
                    "Connection failed: " + ex.Message,
                    "Exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

        }

        #endregion

    }
}
