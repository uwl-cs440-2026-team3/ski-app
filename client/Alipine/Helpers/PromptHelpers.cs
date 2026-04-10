using System;
using System.Collections.Generic;
using System.Text;

namespace Alpine.Helpers
{
    public static class PromptCoachHelperClass
    {
        public static (bool ok, string email, string username, string password) Show()
        {
            using var form = new PromptCoach();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (true, form.Email, form.Username, form.Password);
            }

            return (false, "", "", "");
        }
    }

    public static class PromptSingleHelperClass
    {
        public static (bool ok, string a) Show(string title, string label1)
        {
            using var form = new PromptSingle(title, label1);
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (true, form.Value1);
            }

            return (false, "");
        }
    }

    public static class PromptCancelHelperClass
    {
        public static (bool ok, string a) Show(string title, string label1)
        {
            using var form = new PromptCancel(title, label1);
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (true, form.Value1);
            }

            return (false, "");
        }
    }

    public static class PromptTeamHelperClass
    {
        public static (bool ok, string teamName, string coach, string firstSkier, string secondSkier) Show()
        {
            using var form = new PromptTeam();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (true, form.TeamName, form.Coach, form.FirstSkier, form.SecondSkier);
            }

            return (false, "", "", "", "");
        }
    }

    public static class PromptScheduleHelperClass
    {
        public static (bool ok, string name, string teama, string teamb, string courseName, string dateTime, string minutes ) Show()
        {
            using var form = new PromptSchedule();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (true,form.RaceName, form.TeamA, form.TeamB, form.CourseName, form.DateTimeMe, form.Minutes);
            }

            return (false, "", "", "", "", "", "");
        }
    }

    public static class PromptTimesHelperClass
    {
        public static (bool ok, string raceName, string TeamASkierOne, string TeamASkierTwo, string TeamBSkierThree, string TeamBSkierFour) Show()
        {
            using var form = new PromptTimes();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (true, form.RaceName, form.TeamASkierOne, form.TeamASkierTwo, form.TeamBSkierOne, form.TeamBSkierTwo);
            }

            return (false, "", "", "", "", "");
        }
    }
}

