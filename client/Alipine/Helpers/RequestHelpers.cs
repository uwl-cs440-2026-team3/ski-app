using System;
using System.Collections.Generic;
using System.Media;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Alpine.Helpers
{
    // helper class containing methods for server data requests
    internal class RequestHelpers
    {
        
        // object that represents a member of our system
        public class Member
        {
            public string email { get; set; }
            public string name { get; set; }
            public string role { get; set; }
            public string team { get; set; }
        }

        // object that represents a team 
        public class Team
        {
            public string name { get; set; }
        }

        // object that represents a specifc users team 
        public class MyTeam
        {
            public string name { get; set; }
            public List<string> skiers { get; set; }
            public string coach { get; set; }
        }

        // object that represents a specifc users races
        public class MyRaces
        {
            public string name { get; set; }
            public string teamA { get; set; }
            public string teamB { get; set; }
            public string course { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
        }

        public class Races
        {
            public string name { get; set; }
            public string teamA { get; set; }
            public string teamB { get; set; }
            public string course { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
        }

        // object that represents a course 
        public class Course
        {
            public string name { get; set; }
        }

        // object that represents a race
        public class Race
        {
            public string name { get; set; }
            public string teamA { get; set; }
            public string teamB { get; set; }
        }

        
        public async Task<string> PostRequestMembers()
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer",Globals.Token);

            var user = new { };

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getmembers");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }

        
        public async Task<string> PostRequestTeams()
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new { };

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getteams");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }

        public async Task<string> PostRequestCourses()
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new { };

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getcourses");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }


        public async Task<string> PostRequestRaces()
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new { };

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getraces");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }

        public async Task<string> PostRequestMyTeam()
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getmyteam");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }

        public async Task<string> PostRequestMyRaces()
        {   

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getmyraces");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }
    }
}
