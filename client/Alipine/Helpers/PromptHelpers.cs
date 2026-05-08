using System;
using System.Collections.Generic;
using System.Text;

// classes that help us construct our user prompts, originally made in a more complex way for modularity, but perhaps a bit uneeded now
namespace Alpine.Helpers
{
    public static class PromptCoachHelperClass
    {
        public static (bool ok, string email, string username, string password) Show()
        {
            // we are using the prompt coach form
            using var form = new PromptCoach();

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true, form.Email, form.Username, form.Password);
            }

            // otherwise we default in a logical way
            return (false, "", "", "");
        }
    }

    public static class PromptSingleHelperClass
    {
        public static (bool ok, string a) Show(string title, string label1)
        {
            // we are using the prompt single form
            using var form = new PromptSingle(title, label1);

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true, form.Value1);
            }

            // otherwise we default in a logical way
            return (false, "");
        }
    }

    public static class PromptCancelHelperClass
    {
        public static (bool ok, string a) Show(string title, string label1)
        {

            // we are using the prompt cancel form
            using var form = new PromptCancel(title, label1);

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true, form.Value1);
            }

            // otherwise we default in a logical way
            return (false, "");
        }
    }

    public static class PromptTeamHelperClass
    {
        public static (bool ok, string teamName, string coach, string firstSkier, string secondSkier) Show()
        {

            // we are using the prompt team form
            using var form = new PromptTeam();

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true, form.TeamName, form.Coach, form.FirstSkier, form.SecondSkier);
            }

            // otherwise we default in a logical way
            return (false, "", "", "", "");
        }
    }

    public static class PromptScheduleHelperClass
    {
        public static (bool ok, string name, string teama, string teamb, string courseName, string dateTime, string minutes ) Show()
        {

            // we are using the prompt schedule form
            using var form = new PromptSchedule();

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true,form.RaceName, form.TeamA, form.TeamB, form.CourseName, form.DateTimeMe, form.Minutes);
            }

            // otherwise we default in a logical way
            return (false, "", "", "", "", "", "");
        }
    }

    public static class PromptTimesHelperClass
    {
        public static (bool ok, string raceName, string TeamASkierOne, string Time) Show()
        {

            // we are using the prompt times form
            using var form = new PromptTimes();

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true, form.RaceName, form.TeamASkierOne, form.Time);
            }

            // otherwise we default in a logical way
            return (false, "", "", "");
        }
    }

    public static class PromptRemoveCoachHelperClass
    {
        public static (bool ok, string a) Show(string title, string label1)
        {

            // we are using the prompt remove coach form
            using var form = new PromptRemoveCoach();

            // we show the form and log its results
            var result = form.ShowDialog();

            // if the user clicked okay
            if (result == DialogResult.OK)
            {
                // we return what the user selected
                return (true, form.Team);
            }

            // otherwise we default in a logical way
            return (false, "");
        }
    }

}

