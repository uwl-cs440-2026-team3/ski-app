using Alpine.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alpine.Helpers
{
    internal class ManualHelpers
    {
        // form largely originally chatgpt
        // manages opening the help form, basically summed up it opns the help form in a new thread so that it is not tied to any of our other forms,
        // allowing us to still use it when we switch between them
        // it also only allows one help form at a time, if we try to open one again it just switches the page of the currently open one


        private static Thread helpThread; // seperate thread
        private static HelpForm helpForm; // the form
        private static readonly object helpLock = new object(); // lock to help prevent race conditions


        public static void openHelperForm()
        {

            lock (helpLock) // one at a time
            {
                // if help form doesn't exist (or was closed), create a new thread + form
                if (helpForm == null || helpForm.IsDisposed)
                {
                    helpThread = new Thread(() =>
                    {
                        helpForm = new HelpForm();

                        helpForm.FormClosed += (s, args) =>
                        {
                            lock (helpLock)
                            {
                                helpForm = null;
                                helpThread = null;
                            }
                        };

                        Application.Run(helpForm);
                    });

                    // winforms nonsense
                    helpThread.SetApartmentState(ApartmentState.STA);
                    helpThread.IsBackground = true;
                    helpThread.Start();
                }
                else
                {
                    // already open -> bring it forward safely from its own UI thread
                    helpForm.BeginInvoke(new Action(() =>
                    {
                        if (helpForm.WindowState == FormWindowState.Minimized)
                            helpForm.WindowState = FormWindowState.Normal;

                        helpForm.ChangePage(); // change the page to the current global one
                        helpForm.Show();
                        helpForm.BringToFront();
                        helpForm.Activate();
                    }));
                }
            }
        }
    }
}
