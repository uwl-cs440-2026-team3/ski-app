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
    // the landing form of our application, this form contains the log in and registration controls
    public partial class Landing : Form
    {
        // we need this form to have an event it calls on finished, this is what makes switching between forms in the mainform work
        public event Action<string> OnFinished;
        public Landing()
        {
            // for scaling stuff
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeComponent();

            // we try to load the ssl right away
            Globals.LoadSSL();

        }

        #region buttons

        // for when the user clicks to log in
        private async void LoginCLick(object sender, EventArgs e)
        {
            // check if stuff is missing first, no use trying to send data we know the server will reject
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
            // we validate our email and password here, again, no use trying to send data we know the server will reject
            else if (!(Alpine.Helpers.ValidationHelpers.CheckEmail(tb_Email.Text) && Alpine.Helpers.ValidationHelpers.CheckPassword(mtb_Password.Text)))
            {
                return;
            }

            // we send off to the server to log in
            await PostLogin(tb_Email.Text, mtb_Password.Text);
        }

        // for when the user clicks to register
        private async void RegisterClick(object sender, EventArgs e)
        {
            // check if stuff is missing first, no use trying to send data we know the server will reject
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

            // we validate our email, username and password here, again, no use trying to send data we know the server will reject
            else if (!(Alpine.Helpers.ValidationHelpers.CheckEmail(tb_Email.Text) && Alpine.Helpers.ValidationHelpers.CheckName(tb_Username.Text) &&  Alpine.Helpers.ValidationHelpers.CheckPassword(mtb_Password.Text)))
            {
                return;
            }

            // we send off to the server to register
            await PostRegister(tb_Email.Text, tb_Username.Text, mtb_Password.Text);
            
        }


        #endregion

        #region endpoints 


        // for sending a post to the server that we want to register users
        private async Task PostRegister(String email, String name, String password)
        {
            // build our json body
            var user = new
            {
                email = email,
                name = name,
                password = password
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("register", user);

            // get the response
            string responseBody = await response.Content.ReadAsStringAsync();

            // it would make sense to replace this with responseChecker but, i am a bit pressed for time

            // if we do not get a response that it created the email
            if (response.StatusCode != HttpStatusCode.Created)
            {
                // alert the user that registration failed
                MessageBox.Show("Server responded:" + responseBody, "Registration failed.",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // if we succeed we should log in! 
                await PostLogin(tb_Email.Text, mtb_Password.Text);

            }

        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
        private async Task PostLogin(String email, String password)
        {

            // build our json body
            var user = new
            {
                email = email,
                password = password
            };

            try
            {
                // send our post
                using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("login", user);

                // await the rest of the response text
                string responseBody = await response.Content.ReadAsStringAsync();

                // it would make sense to replace this with responseChecker but, i am a bit pressed for time

                // you ONLY get to log in and unlock controls if we get an okay, #leastprivlege
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // alert the user that login failed
                    MessageBox.Show("Server responded:" + responseBody, "Login failed.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // we get the token and the type of user here
                using JsonDocument tempJson = JsonDocument.Parse(responseBody);
                JsonElement tempObj = tempJson.RootElement;

                Globals.Role = tempObj.GetProperty("role").ToString();
                Globals.Token = tempObj.GetProperty("token").ToString();

                // TODO:: if we modified what login responds with we could get the user name here as well, which would allow us to have the usersname instead of just their email

                // want this for display purposes later
                Globals.Name = tb_Email.Text;

                // unlock stuff for roles

                OnFinished?.Invoke("success");
                this.Close();
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
