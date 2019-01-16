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

    [Scope(Feature = "LCI_GetBinSearchByFilter")]
    [Binding]
    public class LCI_GetBinSearchByFilterSteps
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
            binReqEndUrl = ConfigurationManager.AppSettings["getbininvlist"];
            request.AddHeader("Authorization", token);
        }
        
        [When(@"I pass no filter")]
        public void WhenIPassNoFilter()
        {
            binReqEndPoint = binReqEndUrl;
            this.restClient = new RestClient(binReqEndPoint);
        }
        
        [When(@"I pass with GrowerNbr filter")]
        public void WhenIPassWithGrowerNbrFilter()
        {
            //String grwNbr = DatabaseConnection.getRandBinIdFromInventory(ConfigurationManager.AppSettings["getRandBinIdFromInventory"]);
            binReqEndPoint = binReqEndUrl + "?GrowerNumber=000000";
            this.restClient = new RestClient(binReqEndPoint);
        }
        
        [When(@"I pass with RanchNbr filter")]
        public void WhenIPassWithRanchNbrFilter()
        {
            binReqEndPoint = binReqEndUrl + "?RanchNumber=000000";
            this.restClient = new RestClient(binReqEndPoint);
        }
        
        [When(@"I pass with BerryType filter")]
        public void WhenIPassWithBerryTypeFilter()
        {
            binReqEndPoint = binReqEndUrl + "?BerryType=STRAW";
            this.restClient = new RestClient(binReqEndPoint);
        }
        
        [When(@"I pass with WarehouseNbr filter")]
        public void WhenIPassWithWarehouseNbrFilter()
        {
            binReqEndPoint = binReqEndUrl + "?BerryType=STRAW";
            this.restClient = new RestClient(binReqEndPoint);
        }
        
        [Then(@"I should get status code '(.*)'")]
        public void ThenIShouldGetStatusCode(string statusCode)
        {
            if (response.StatusCode.ToString() != statusCode)
            {
                Assert.Fail("Status code is " + response.StatusCode.ToString() + "\n" + response.Content);
            }
        }
        
        [Then(@"a response with valid bin id")]
        public void ThenAResponseWithValidBinId()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string respBinId = (String)item.GetValue("BinId");
                Assert.AreEqual(binValue, respBinId);
            }
        }
        
        [Then(@"the response contains only records with specified grower number")]
        public void ThenTheResponseContainsOnlyRecordsWithSpecifiedGrowerNumber()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string eventType = (String)item.GetValue("EventType");
                Assert.AreEqual("Grower", eventType);
            }
        }
        
        [Then(@"the response contains only records with specified ranch number")]
        public void ThenTheResponseContainsOnlyRecordsWithSpecifiedRanchNumber()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string eventType = (String)item.GetValue("EventType");
                Assert.AreEqual("Ranch", eventType);
            }
        }
        
        [Then(@"the response contains only records with specified Berry Type")]
        public void ThenTheResponseContainsOnlyRecordsWithSpecifiedBerryType()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string eventType = (String)item.GetValue("EventType");
                Assert.AreEqual("Berry", eventType);
            }
        }
        
        [Then(@"the response contains only records with specified WarehouseNbr")]
        public void ThenTheResponseContainsOnlyRecordsWithSpecifiedWarehouseNbr()
        {
            JArray a = JArray.Parse(response.Content);

            foreach (JObject item in a.Children())
            {
                string eventType = (String)item.GetValue("EventType");
                Assert.AreEqual("Ware", eventType);
            }
        }
    }
}
