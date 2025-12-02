using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Reqnroll;
using RestSharp;
using System;
using System.Net;
using WebAPITests.Hooks;

namespace WebAPITests.StepDefinitions
{
    [Binding]
    public class APIRequestsStepDefinitions
    {
        private RestClient client;
        private RestResponse response;

        public static int BookingId;

        [Given("the API is initialized")]
        public void GivenTheAPIIsInitialized()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }

        [When("I create a booking")]
        public void WhenICreateABooking()
        {
            var request = new RestRequest("booking", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

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
                additionalneeds = "Peanut butter sandwich"
            });

            response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                BookingId = (int)json["bookingid"];
            }
        }

        [Then("the booking is created successfully")]
        public void ThenTheBookingIsCreatedSuccessfully()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [When("I get the booking")]
        public void WhenIGetTheBooking()
        {
            var request = new RestRequest($"booking/{BookingId}", Method.Get);
            request.AddHeader("Accept", "application/json");

            response = client.Execute(request);
        }

        [Then("the booking is retrieved successfully")]
        public void ThenTheBookingIsRetrievedSuccessfully()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [When("I update the booking")]
        public void WhenIUpdateTheBooking()
        {
            var request = new RestRequest($"booking/{BookingId}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", $"token={AuthHooks.token}");

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
                },
                additionalneeds = "Green tea"
            });

            response = client.Execute(request);
        }

        [Then("the booking is updated successfully")]
        public void ThenTheBookingIsUpdatedSuccessfully()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [When("I delete the booking")]
        public void WhenIDeleteTheBooking()
        {
            var request = new RestRequest($"booking/{BookingId}", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", $"token={AuthHooks.token}");

            response = client.Execute(request);
        }

        [Then("the booking is deleted successfully")]
        public void ThenTheBookingIsDeletedSuccessfully()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }


    }
}



