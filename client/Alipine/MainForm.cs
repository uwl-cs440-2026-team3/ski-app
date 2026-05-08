using Alpine.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Alpine
{
    // the main form of the application, all three views and the landing will open into this one and switch between each other seemlessly
    public partial class MainForm : Form
    {

        // locally keep track of what form we are in
        private Form currentForm = null;

        public MainForm()
        {
            InitializeComponent();

            // these allow us to have the help button.... the windows api is really picky
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = true;

            ShowLanding();

            Globals.Current_Form = "landing"; // keep track of what form we are in for the manual

        }

        // this will show the landing / login registration page
        private void ShowLanding()
        {
            // we create a new landing form
            var landing = new Landing();

            // actions to perform when the landing form is "done" 
            landing.OnFinished += (result) =>
            {

                // depending on what role we logged in as, we open the view associated
                switch (Globals.Role)
                {
                    case "admin":
                        Globals.Current_Form = "admin"; // keep track of what form we are in for the manual
                        ShowAdmin(); // open the admin view
                        break;

                    case "coach":
                        Globals.Current_Form = "coach"; // keep track of what form we are in for the manual
                        ShowCoach(); // open the coach view
                        break;

                    case "skier":
                        Globals.Current_Form = "skier"; // keep track of what form we are in for the manual
                        ShowSkier(); // open the skier view
                        break;

                    default: // if this somehow fails we do not go to a different view
                        SystemSounds.Exclamation.Play();
                        MessageBox.Show("Error switching to view from landing, the odds of you seeing this are low if you do it likely means the server connection failed in the middle of transactions. Please tell a dev!");
                        Application.Exit(); // close the program
                        ShowLanding();
                        break;
                }
            };

            // after we attached the onfinished event to the new landing, we open it into the main form
            OpenFormInPanel(landing); 
        }

        // for oepning the admin view
        private void ShowAdmin()
        {
            // we create a new admin form
            var admin = new Admin();

            // attach events to perfom when the user logs out of the admin form
            admin.OnLogout += () =>
            {
                Globals.InitFields(); // on log out we clear all of our global data
                ShowLanding(); // we reopen the landing form
            };

            // after we attached the onfinished event to the new landing, we open it into the main form
            OpenFormInPanel(admin);
        }

        // for oepning the coach view
        private void ShowCoach()
        {
            // we create a new coach form
            var coach = new Coach();

            // attach events to perfom when the user logs out of the coach form
            coach.OnLogout += () =>
            {
                Globals.InitFields(); // on log out we clear all of our global data
                ShowLanding(); // we reopen the landing form
            };

            // after we attached the onfinished event to the new landing, we open it into the main form
            OpenFormInPanel(coach);
        }

        // for oepning the skier view
        private void ShowSkier()
        {
            // we create a new skier form
            var skier = new Skier();

            // attach events to perfom when the user logs out of the skier form
            skier.OnLogout += () =>
            {
                Globals.InitFields(); // on log out we clear all of our global data
                ShowLanding(); // we reopen the landing form
            };

            // after we attached the onfinished event to the new landing, we open it into the main form
            OpenFormInPanel(skier);
        }

        // method for opening a form into this mainform, decently wrote by chatgpt
        private void OpenFormInPanel(Form childForm)
        {
            // if there is already a current form
            if (currentForm != null)
            {
                currentForm.Close(); // close it
                currentForm.Dispose(); // dispose of it
                currentForm = null; // keep track that we have no form open
            }

            // get the size the form was designed to be
            Size wantedSize = childForm.Size;

            // keep track of the newly made form
            currentForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill; // fill panel cleanly

            p_Main.Controls.Clear();

            // set panel size first
            p_Main.Size = wantedSize;

            p_Main.Controls.Add(childForm);

            childForm.Show();

            // resize MainForm so panel fully fits inside ClientArea
            this.ClientSize = new Size(
                p_Main.Left + wantedSize.Width,
                p_Main.Top + wantedSize.Height
            );
        }

        // for opening the manual
        protected override void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // prevent the default
            Alpine.Helpers.ManualHelpers.openHelperForm(); // we go open the manual
        }
    }
}
