using System;
using System.Net;
using Reqnroll;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPITests.Hooks;

namespace WebAPITests.StepDefinitions
{
    [Binding]
    public class APIRequestsStepDefinitions
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;

        public static int CreatedBookingId;

        [Given("connected")]
        public void GivenConnected()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }


        // READ---------------

        [Given("create get request")]
        public void GivenCreateGetRequest()
        {
            request = new RestRequest("booking",Method.Get);
        }

        [When("send request")]
        public void WhenSendRequest()
        {
            response = client.Execute(request);
        }

        [Then("response is success")]
        public void ThenResponseIsSuccess()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        

        // POST ---------------

        [Given("create create request")]
        public void GivenCreateCreateRequest()
        {
            request = new RestRequest("booking",Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new PostModel()
            {
                firstname = "Elliot",
                lastname = "Smith",
                totalprice = 2003,
                depositpaid = false,
                bookingdates = new BookingDates()
                {
                    checkin = "2025-10-14",
                    checkout = "2025-10-21"
                },
                additionalneeds = "Peanut butter sandwich for breakfast"
            });
        }

        [Then("response create is success")]
        public void ThenResponseCreateIsSuccess()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
            CreatedBookingId = (int)json["bookingid"];

            Console.WriteLine($"Created booking ID: {CreatedBookingId}");

        }



        // UPDATE---------------

        [Given("create update request")]
        public void GivenCreateUpdateRequest()
        {

            request = new RestRequest($"booking/{CreatedBookingId}", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", $"token={AuthHooks.token}");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new PostModel()
            {
                firstname = "Bob",
                lastname = "Bobby",
                totalprice = 500,
                depositpaid = true,
                bookingdates = new BookingDates()
                {
                    checkin = "2025-11-01",
                    checkout = "2025-11-10"
                }
            });
        }

        [Then("response update is success")]
        public void ThenResponseUpdateIsSuccess()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        // DELETE---------------

        [Given("create delete request")]
        public void GivenCreateDeleteRequest()
        {
            request = new RestRequest($"booking/{CreatedBookingId}", Method.Delete);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", $"token={AuthHooks.token}");
        }

        [Then("response delete is success")]
        public void ThenResponseDeleteIsSuccess()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

    }
}
