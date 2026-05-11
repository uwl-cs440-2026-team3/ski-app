using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Media;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Alpine.Helpers
{
    // a class of methods used for validating data and responses.
    internal static class ValidationHelpers
    {

        // validation related properties

        private static readonly int NameLengthMax = 64;

        private static readonly int PasswordLengthMin = 8;

        private static readonly int PasswordLengthMax = 128;

        private static readonly int EmailLocalMax = 64;

        private static readonly int EmailDomainmax = 255;

        private static readonly Regex EmailRegex = new Regex(@"^([^@]+)@([^@]+)$", RegexOptions.Compiled); // thank you chatgpt

        // check if a name is valid
        public static bool CheckName(String name)
        {
            if (name.Length > NameLengthMax)
            {
                ShowError("Name is too long!");
                return false;
            }
            if (name.Length < 0)
            {
                ShowError("Name is of invalid length!");
                return false;
            }

            return true;
        }

        // check if a password is valid
        public static bool CheckPassword(String password)
        {
            if (password.Length > PasswordLengthMax)
            {
                ShowError("Password is too long!");
                return false;
            }
            if (password.Length < PasswordLengthMin)
            {
                ShowError("Password is too short!");
                return false;
            }

            return true;
        }

        // check if an email is valid
        public static bool CheckEmail(String email)
        {
            var match = EmailRegex.Match(email);

            if (!match.Success)
            {
                ShowError("Email is malformed (should be local@domain)!");
                return false;
            }
            if (match.Groups[1].Value.Length > EmailLocalMax)
            {
                ShowError("Email local is too long!");
                return false;
            }
            if (match.Groups[2].Value.Length > EmailDomainmax)
            {
                ShowError("Email domain is too long!");
                return false;
            }

            return true;
        }

        // for showing errors
        private static void ShowError( string message, string text = "" )
        {
            MessageBox.Show(
                text,
                message,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private static void ShowSucess(string message, string text = "")
        {
            MessageBox.Show(
                text,
                message,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // we use this to check server response and tell the user depending on what they get back
        public static void responseChecker(HttpStatusCode response, string text )
        {

            switch (response)
            {
                case HttpStatusCode.OK:
                    SystemSounds.Beep.Play();
                    ShowSucess("Okay!", text);
                    break;

                case HttpStatusCode.Created:
                    SystemSounds.Beep.Play();
                    ShowSucess("Item created!", text);
                    break;

                case HttpStatusCode.BadRequest:
                    SystemSounds.Exclamation.Play();    
                    ShowError("The request sent to the server was malformed.", text);
                    break;

                case HttpStatusCode.Forbidden:
                    SystemSounds.Exclamation.Play();
                    ShowError("Current Session has invalid credentials for this action.", text);
                    break;

                case HttpStatusCode.NotFound:
                    SystemSounds.Exclamation.Play();
                    ShowError("One of the entered fields was not found on the server.", text);
                    break;

                case HttpStatusCode.Conflict:
                    SystemSounds.Exclamation.Play();
                    ShowError("One of the entered fields conflicts with data on the server.", text);
                    break;

                default: 
                    SystemSounds.Exclamation.Play();
                    ShowError("Non-specified server error.", text);
                    break;
            }
        }
    }
}
