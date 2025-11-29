using Reqnroll;
using RestSharp;
using System;
using System.Net;

namespace WebAPITests.StepDefinitions
{
    [Binding]
    public class CurrencyConversionStepDefinitions
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse<CurrencyConvertResponse> response;

        [Given("connected to currency data db")]
        public void GivenConnectedToCurrencyDataDb()
        {
            client = new RestClient("https://api.apilayer.com/currency_data/");
        }

        [Given("create convert request with amount {string} from {string} to {string} for {string}")]
        public void GivenCreateConvertRequestWithAmountFromToFor(string AMOUNT, string FROM, string TO, string DATE)
        {
            request = new RestRequest("convert", Method.Get);
            request.AddParameter("amount", AMOUNT);
            request.AddParameter("from", FROM);
            request.AddParameter("to", TO);
            request.AddParameter("date", DATE);
            request.AddHeader("apikey", "4qT58h9pCCKl2HwAXuBjXaIGJqB8WF3v");
            request.RequestFormat = DataFormat.Json;

        }

        [When("send currency request")]
        public void WhenSendCurrencyRequest()
        {
            response = client.Execute<CurrencyConvertResponse>(request);
        }

        [Then("conversion response is sucess")]
        public void ThenConversionResponseIsSucess()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.Data.success);
        }

        [Then("converted amount is returned")]
        public void ThenConvertedAmountIsReturned()
        {
            Assert.AreEqual("USD", response.Data.query.from);
            Assert.AreEqual("UAH", response.Data.query.to);
            Assert.AreEqual(200, response.Data.query.amount);
            Console.WriteLine($"Converted amount: {response.Data.result}");
        }
    }
}
