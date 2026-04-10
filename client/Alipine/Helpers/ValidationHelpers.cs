using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Alpine.Helpers
{
    internal static class ValidationHelpers
    {

        // validation related properties

        private static readonly int NameLength = 9999;

        private static readonly int PasswordLength = 9999;

        private static readonly int EmailLength = 9999;

        private static readonly Regex EmailRegex = new Regex(@".*", RegexOptions.Compiled);


        public static bool CheckName(String name)
        {
            if (name.Length > NameLength)
            {
                ShowError("Name is too long!");
                return false;
            }

            return true;
        }

        public static bool CheckPassword(String password)
        {
            if (password.Length > PasswordLength)
            {
                ShowError("Password is too long!");
                return false;
            }

            return true;
        }

        public static bool CheckEmail(String email)
        {
            if (email.Length > EmailLength)
            {
                ShowError("Email is too long!");
                return false;
            }
            else if (!EmailRegex.IsMatch(email))
            {
                ShowError("Email is invalid!");
                return false;
            }

            return true;
        }


        private static void ShowError( string message )
        {
            MessageBox.Show(
                message,
                message,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
