using Reqnroll;
using RestSharp;
using System;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPITests.StepDefinitions
{
    [Binding]
    public class CurrencyConversionStepDefinitions
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse<CurrencyConvertResponse> convertResponse;
        private RestResponse<CurrencyChangeResponse> changeResponse;

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

        [When("send currency convert request")]
        public void WhenSendCurrencyConvertRequest()
        {
            convertResponse = client.Execute<CurrencyConvertResponse>(request);
        }

        [Then("conversion response is sucess")]
        public void ThenConversionResponseIsSucess()
        {
            Assert.AreEqual(HttpStatusCode.OK, convertResponse.StatusCode);
            Assert.IsTrue(convertResponse.Data.success);
        }

        [Then("converted amount is returned")]
        public void ThenConvertedAmountIsReturned()
        {
            Assert.AreEqual("USD", convertResponse.Data.query.from);
            Assert.AreEqual("UAH", convertResponse.Data.query.to);
            Assert.AreEqual(200, convertResponse.Data.query.amount);
            Console.WriteLine($"Converted amount: {convertResponse.Data.result}");
        }


        //----------------

        [Given("create change request with currencies {string} relative to source {string} starting {string} ending {string}")]
        public void GivenCreateChangeRequestWithCurrenciesRelativeToSource(string CURRENCIES, string SOURCE, string STARTDATE, string ENDDATE)
        {
            request = new RestRequest("change", Method.Get);
            request.AddParameter("end-date", ENDDATE);
            request.AddParameter("start-date", STARTDATE);
            request.AddParameter("currencies", CURRENCIES);
            request.AddParameter("source", SOURCE);
            request.AddHeader("apikey", "4qT58h9pCCKl2HwAXuBjXaIGJqB8WF3v");
            request.RequestFormat = DataFormat.Json;
        }

        [When("send currency change request")]
        public void WhenSendCurrencyChangeRequest()
        {
            changeResponse = client.Execute<CurrencyChangeResponse>(request);
        }


        [Then("change response is sucess")]
        public void ThenChangeResponseIsSucess()
        {
            Assert.AreEqual(HttpStatusCode.OK, changeResponse.StatusCode);
            Assert.IsTrue(changeResponse.Data.success);
        }

        [Then("change of the currencies is returned")]
        public void ThenChangeOfTheCurrenciesIsReturned()
        {
            
            Assert.AreEqual("2025-12-01", changeResponse.Data.end_date);
            Assert.AreEqual("2025-11-30", changeResponse.Data.start_date);
            Assert.AreEqual("UAH", changeResponse.Data.source);
            foreach (var quote in changeResponse.Data.quotes)
            {
                Console.WriteLine($"{quote.Key}: change={quote.Value.change}, change_pct={quote.Value.change_pct}, start_rate={quote.Value.start_rate}, end_rate={quote.Value.end_rate}");
            }
        }

    }
}
