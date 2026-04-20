using System;
using System.Collections.Generic;
using System.Media;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Alpine.Helpers
{
    internal class RequestHelpers
    {
        public class Member
        {
            public string email { get; set; }
            public string name { get; set; }
            public string role { get; set; }
            public string team { get; set; }
        }

        public class Team
        {
            public string name { get; set; }
        }

        public class MyTeam
        {
            public string name { get; set; }
            public List<string> skiers { get; set; }
            public string coach { get; set; }
        }

        public class Course
        {
            public string name { get; set; }
        }

        public class Race
        {
            public string name { get; set; }
            public string teamA { get; set; }
            public string teamB { get; set; }
        }

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
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

        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient / chatgpt / https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0
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

            var user = new { };

            // send our post
            using HttpResponseMessage response = await Globals.Client.GetAsync("getmyteam");

            // await the rest of the response text
            string responseBody = await response.Content.ReadAsStringAsync();

            // okay then we return the json string

            return responseBody;
        }
    }
}
