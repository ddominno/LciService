using System;
using TechTalk.SpecFlow;

namespace ServiceLCITests.StepDefinitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestSharp;
    using ServiceLCITests.Utilities;
    using System.Configuration;
    using Newtonsoft.Json.Linq;
    using DriscTAF.Utilities;
    [Scope(Feature = "LCI_GetBinRejectEventByEventFilter")]
    [Binding]
    public class LCI_GetBinRejectEventByEventFilterSteps
    {
        RestClient restClient;
        public static RestRequest request;
        public static string json = "";
        public static IRestResponse response;
        public static string binReqEndUrl = "";
        public static string binReqEndPoint = "";
        public static string jsonpath = "";
        public static DateTime timeTokenGenerated;
        public string token;

        public string binValue = "";
        GetToken tokenID;

        [Given(@"I have a Bin request")]
        public void GivenIHaveABinRequest()
        {
            request = new RestRequest(Method.GET);
            TimeSpan timeSpan = DateTime.Now - timeTokenGenerated;
            if (token == null || timeSpan.Minutes > 60)
            {
                token = "Bearer " + IncludeTokenID.GivenIncludeToken();

            }
            binReqEndUrl = ConfigurationManager.AppSettings["getrejectgrowerurl"];
            request.AddHeader("Authorization", token);
        }
        
        [When(@"I pass a grower Id existing")]
        public void WhenIPassAGrowerIdExisting()
        {
            binReqEndPoint = binReqEndUrl + "0993142";
            this.restClient = new RestClient(binReqEndPoint);
        }
        
        [When(@"I pass a grower Id not existing")]
        public void WhenIPassAGrowerIdNotExisting()
        {
            binReqEndPoint = binReqEndUrl + "0993142";
            this.restClient = new RestClient(binReqEndPoint);

        }
        [When(@"Execute the Request")]
        public void WhenExecuteTheRequest()
        {
            {
                if (request.Method != Method.DELETE || request.Method != Method.GET)
                {
                    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                    response = this.restClient.Execute(request);
                }

            }
        }
        [Then(@"I should get status code '(.*)'")]
        public void ThenIShouldGetStatusCode(string statusCode)
        {

            if (response.StatusCode.ToString() != statusCode)
            {
                Assert.Fail("Status code is " + response.StatusCode.ToString() + "\n" + response.Content);
            }
        }
        
        [Then(@"a response with valid event type")]
        public void ThenAResponseWithValidEventType()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string eventType = (String)item.GetValue("EventType");
                Assert.AreEqual("PreReceiptReject", eventType);
            }
        }
        
        [Then(@"the response content is empty")]
        public void ThenTheResponseContentIsEmpty()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string eventType = (String)item.GetValue("EventType");
                Assert.AreEqual("PreReceiptReject", eventType);
            }
        }
    }
}
