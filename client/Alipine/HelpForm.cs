using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Alpine.Views
{
    // this form contains a webview which opens up our html manual
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();

            // event for when we load
            this.Load += Help_Load;
        }

        private void Help_Load(object sender, EventArgs e)
        {
            // we build the path of the manual file we want from the current form kept in globals
            string path = Path.Combine(Application.StartupPath, "manual/" + Globals.Current_Form + ".html");

            // we open that html
            wv_help.Source = new Uri(path);
        }

        // method for changing what page an already open helper form is on
        public void ChangePage()
        {
            // we build the path of the manual file we want from the current form kept in globals
            string path = Path.Combine(Application.StartupPath, "manual/" + Globals.Current_Form + ".html");

            // we change to that page
            wv_help.CoreWebView2.Navigate(path);
        }
    }
}
