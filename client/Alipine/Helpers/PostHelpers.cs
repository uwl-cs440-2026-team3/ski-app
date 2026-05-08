using System;
using System.Collections.Generic;
using System.Media;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static Alpine.Helpers.RequestHelpers;

namespace Alpine.Helpers
{
    // this class consists of methods used to send POST requests to the server
    internal class PostHelpers
    {

        public static async Task<(HttpStatusCode status, string response)> PostTeam(String name, String skier1, String skier2, String coach)
        {
            
            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
                skier1_email = skier1,
                skier2_email = skier2,
                coach_email = coach
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("team", user);

            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);


        }

        
        public static async Task<(HttpStatusCode status, string response)> PostCourse(String name)
        {
            // attach our auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            // build our json message
            var user = new
            {
                name = name,
            };

            // post our request
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("course", user);

            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);

        }

        
        public static async Task<(HttpStatusCode status, string response)> PostRegisterCoach(String email, String name, String password)
        {
            // attach our auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            // build our json message
            var user = new
            {
                email = email,
                name = name,
                password = password
            };

            // post our request
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("registercoach", user);


            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);
        }

        
        public static async Task<(HttpStatusCode status, string response)> PostScheduleRace(String name, String team_a, String team_b, String course_name, String datetime, String minutes)
        {
            
            // sql sanitization? check email? make sure stuff makes sense?

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
                team_a = team_a,
                team_b = team_b,
                course = course_name,
                start = datetime,
                duration = minutes
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("schedule", user);

            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);
        }

        public static async Task<(HttpStatusCode status, string response)> PostCancel(String name)
        {
            

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("cancel", user);

            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);
        }

        public static async Task<(HttpStatusCode status, string response)> PostTimes(String name, String email, String time)
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            var user = new
            {
                race = name,
                email = email,
                time = time

            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("postscore", user);

            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);
        }

        public static async Task<(HttpStatusCode status, string response)> PostRemoveCoach(String name)
        {

            // attach the auth token
            Globals.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Globals.Token);

            // how ever this end point will work
            var user = new
            {
                name = name,
            };

            // send our post
            using HttpResponseMessage response = await Globals.Client.PostAsJsonAsync("removecoach", user);

            // get back what the server says 
            string text = await response.Content.ReadAsStringAsync();

            // return the response and the text
            return (response.StatusCode, text);
        }
    }
}
