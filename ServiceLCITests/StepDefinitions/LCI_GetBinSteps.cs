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
    [Scope(Feature = "LCI_GetBin")]
    [Binding]
    public class LCI_GetBinSteps
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

        GetToken tokenID;
        BinDictionary binDict;
        BinInventoryDictionary binInvDict;
        public void Setup()
        {
            tokenID = new GetToken();
            binDict = new BinDictionary();
         //   binDict.PopulateBinEvents();
            binInvDict = new BinInventoryDictionary();
        //    binInvDict.PopulateInventories();
        }
        public string binValue = "";

        [Given(@"I have a Bin request")]
        public void GivenIHaveABinRequest()
        {
            Setup();
            request = new RestRequest(Method.GET);
            TimeSpan timeSpan = DateTime.Now - timeTokenGenerated;
            if (token == null || timeSpan.Minutes > 60)
            {
                token = "Bearer " + IncludeTokenID.GivenIncludeToken();
                
            }
            binReqEndUrl = ConfigurationManager.AppSettings["getbinurl"];
            request.AddHeader("Authorization", token);
   
        }

        [When(@"I pass a Bin Id existing in Inventory")]
        public void WhenIPassABinIdExistingInInventory()
        {
            // Random r = new Random();
            // int index = r.Next(0, binInvDict.BinInventories.Count - 1);
            // binReqEndPoint = binReqEndUrl + binInvDict.BinInventories[0].BinId;
            binValue = DatabaseConnection.getRandBinIdFromInventory(ConfigurationManager.AppSettings["getRandBinIdFromInventory"]);
            binReqEndPoint = binReqEndUrl +binValue;
            this.restClient = new RestClient(binReqEndPoint);
        }

        [Then(@"I should get status code '(.*)'")]
        public void ThenIShouldGetStatusCode(string statusCode)
        {
            dynamic jsonObject = JObject.Parse(response.Content);
            if (response.StatusCode.ToString() != statusCode)
            {
                Assert.Fail("Status code is " + response.StatusCode.ToString() + "\n" + response.Content);
            }
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

        [Then(@"a response with valid bin id")]
        public void ThenAResponseWithValidBinId()
        {
            dynamic jsonObject = JObject.Parse(response.Content);
            string respBinId = jsonObject.BinId;
            Assert.AreEqual(binValue,respBinId);
            if(!respBinId.Equals(binValue))
            Assert.Fail("BinId is"+respBinId);
        }
        [Then(@"the response contains '(.*)'")]
        public void ThenTheResponseContains(string msg)
        {
            dynamic jsonObject = JObject.Parse(response.Content);
            string respMsg = jsonObject.Message;
            Assert.AreEqual(respMsg, msg);
        }


        [When(@"I pass a Bin Id not existing in Inventory")]
        public void WhenIPassABinIdNotExistingInInventory()
        {
            binValue = DatabaseConnection.getRandBinIdFromInventory(ConfigurationManager.AppSettings["getBinIdNotExistsInInventory"]);
            binReqEndPoint = binReqEndUrl + binValue;
            this.restClient = new RestClient(binReqEndPoint);
        }

        [When(@"I pass a Bin Id existing in Inventory and Current Location Type is GrowerField")]
        public void WhenIPassABinIdExistingInInventoryAndCurrentLocationTypeIsGrowerField()
        {
            binValue = DatabaseConnection.getRandBinIdFromInventory(ConfigurationManager.AppSettings["getBinIdFromInventoryWithGrowerFeild"]);
            binReqEndPoint = binReqEndUrl + binValue;
            this.restClient = new RestClient(binReqEndPoint);
        }

    }
}
