using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;


namespace Alpine
{
    // a class that contains runtime global variables, and a few functions related to them
    public static class Globals
    {
        public static string Name { get; set; } = "";
        public static string Role { get; set; } = "";
        public static string Token { get; set; } = "";
        public static string Current_Form { get; set; } = ""; // for keeping track of what url we want to go to for help

        // dotnet add package System.Net.Http.Json --version 10.0.3

        // HttpClient lifecycle management best practices:
        // https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
        public static HttpClient Client { get; private set; }

        static Globals()
        {
            LoadSSL();
        }

        // THIS DOES NOT ACTUALLY USE OUR SSL YET
        public static void LoadSSL()
        {
            // locate the file in the output directory
            //var certPath = Path.Combine(AppContext.BaseDirectory, "server.p12");
            //
            //if (!File.Exists(certPath))
            //    throw new FileNotFoundException("Client certificate not found", certPath);
            //
            //// read p12 as bytes
            //var certBytes = File.ReadAllBytes(certPath);
            //
            //// load the p12 safely
            //var cert = new X509Certificate2(
            //    certBytes,
            //    "thepassword",
            //    X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.Exportable
            //);

            // setup handler with client certificate
            var handler = new HttpClientHandler();
            //handler.ClientCertificates.Add(cert);

            // BYPASS FOR NOW OKLAY I GUESS TODO
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // create HttpClient
            Client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:1041/")
            };
        }

        // clear out our fields, used to make sure when we log out we start with a clean slate
        public static void InitFields()
        {
            Name = "";
            Role = "";
            Token = "";
            Globals.Current_Form = "landing";
        }
    }
}
