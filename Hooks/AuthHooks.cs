using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITests.Hooks
{
    [Binding]
    public sealed class AuthHooks
    {

        public static string token { get; private set; }

        [BeforeTestRun]
        public static void BeforeScenarioWithTag()
        {
            var client = new RestClient("https://restful-booker.herokuapp.com/");
            var authRequest = new AuthRequest();

            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(authRequest);

            var response = client.Execute<AuthResponse>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Auth failed: {response.StatusCode}");

            token = response.Data.token;

        }


    }
}
