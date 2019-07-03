using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GitHubTestApi.Tests
{
    [TestClass]
    public class Setup
    {
        protected static HttpClient client;

        [AssemblyInitialize]
        public static void setupHttpClient(TestContext context)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36");
                       //client.DefaultRequestHeaders.Add("If-None-Match", @"W/""b9198208124beb305242187c7b5555a8""");
        }

        [AssemblyCleanup]
        public static void disposeHttpClient()
        {
            client.Dispose();
        }
    }
}
