namespace ServiceLCITests.StepDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;

    using ServiceLCITests.Utilities;
    using DriscTAF.Utilities;
    using AventStack.ExtentReports.Gherkin.Model;


 

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RestSharp;

    using TechTalk.SpecFlow;
    using RestSharp.Authenticators;

    using ServiceLCITests.DataSources.Models;
    using ServiceLCITests.Features;

    [Binding]
    public class APIStepDefinitions
    {

        RestClient restClient;
        public static RestRequest request;
        public static string json = "";
        public static IRestResponse response;
        public static string baseURL = "";
        public static string endpoint = "";
        public static string jsonpath = "";
        public static DateTime timeTokenGenerated;
        public string token;

        public string binValue = "";

        public string eventId;

        public DataRetriever dataRetriever = new DataRetriever();

        public DataTable dbValues;

        GetToken tokenID; 
        BinDictionary binDict; 
        public void Setup()
        {
            tokenID = new GetToken();
            binDict = new BinDictionary();
    //        binDict.PopulateBinEvents();
        }

        [Given(@"URL for API is '(.*)'")]
        public void GivenURLForAPIIs(string epoint)
        {
            Setup();
            if (epoint.Contains("BIN_ID"))
            {
                string binvalue = DatabaseConnection.getBin(ConfigurationManager.AppSettings["getbinquery"]);

              //  string binvalue = binDict.binEvents[1].BinId;
                epoint = "dlcis/api/v1/lcis/inv/bininv/" + binvalue;
            }

            endpoint = epoint;
            baseURL = ConfigurationManager.AppSettings["url"];

            this.restClient = new RestClient(baseURL + endpoint);

          //  restClient.Authenticator = new SimpleAuthenticator("UserName", "0oa34vdqadadfHSV3356", "password", "EUa-lupZgdmhksGuVOhWh4L7Cjd_ldChikgKoGLU");

        }

        [Given(@"the Payload is set for PostBinMoveEvent")]
        public void GivenThePayloadIsSetForPostBinMoveEvent()
        {
            json = PayloadGenerator.PostBinMove("0004", "subLocTest", binValue);
        }

        [Given(@"the event Id of an inspection")]
        public void GivenTheEventIdOfAnInspection()
        {
            this.dbValues = this.dataRetriever.GetInspectionDataTable();
            this.eventId = dbValues.Rows[0].ItemArray[0].ToString();
        }

        [Given(@"the event Id of a '(.*)'")]
        public void GivenTheEventIdOfA(string eventType)
        {
            //pass the value of the EventType to be retrieved from the BinEvent Table 
            this.dbValues = this.dataRetriever.GetEventTypeDataTable(eventType);
            this.eventId = this.dbValues.Rows[0].ItemArray[0].ToString();
        }


        [Given(@"a bin Id unrelated to the inspection")]
        public void GivenABinIdUnrelatedToTheInspection()
        {
            this.binValue = dbValues.Rows[1].ItemArray[1].ToString();
        }

        [Given(@"a bin Id related to the inspection")]
        public void GivenABinIdRelatedToTheInspection()
        {
            this.binValue = dbValues.Rows[0].ItemArray[1].ToString();
        }

        [Given(@"the Payload is set for PostBinRemoveInspection")]
        public void GivenThePayloadIsSetForPostBinRemoveInspection()
        {
            PayloadGenerator.PostBinInspectionRemove(this.binValue, this.eventId);
        }




        [Given(@"the Payload is set for a new bin")]
        public void GivenThePayloadIsSetForANewBin()
        {
            //next step needs to know about the binValue
            binValue = BinIDGenerator.GenerateBinID();
            json = PayloadGenerator.PostReceipt(binValue);
        }

        [Given(@"the Payload is set for an existing bin")]
        public void GivenThePayloadIsSetForAnExistingBin()
        {
            binValue = DatabaseConnection.GetExistingBin();
            json = PayloadGenerator.PostReceipt(binValue);
        }

        [Given(@"a bin Id that does not exist in inventory")]
        public void GivenABinIdThatDoesNotExistInInventory()
        {
            this.binValue = BinIDGenerator.GenerateBinID();    
        }


        [Given(@"Create GET Request")]
        public void GivenCreateGETRequest()
        {
            Setup();
            request = new RestRequest(Method.GET);
            // IncludeTokenID tokenObj = new IncludeTokenID();
            // string  header = OAuthHelper.GetAuthenticationHeader(true);
            // Console.WriteLine(header);
           TimeSpan timeSpan =  DateTime.Now - timeTokenGenerated;
            if (token==null||timeSpan.Minutes > 60)
            {
                token = "Bearer " + IncludeTokenID.GivenIncludeToken();
                Console.WriteLine("1st time");
            }

            response = restClient.Execute(request);
            request.AddHeader("Authorization", token);

        }

        [Given(@"a binId has been created")]
        public void GivenABinIdHasBeenCreated()
        {
            PostReceipt postReceiptObject = ObjectGenerator.PostReceipt(BinIDGenerator.GenerateBinID());
            json = postReceiptObject.ToString();
            this.GivenCreatePOSTRequest();
            this.WhenTheRequestIsExecuted();
            this.ThenStatusCodeIs("Created");
        }

        [Given(@"Create POST Request")]
        public void GivenCreatePOSTRequest()
        {
            request = new RestRequest(Method.POST);
            TimeSpan timeSpan = DateTime.Now - timeTokenGenerated;
            if (token == null || timeSpan.Minutes > 60)
            {
                token = "Bearer " + IncludeTokenID.GivenIncludeToken();
                Console.WriteLine("1st time");
            }
            request.AddHeader("Authorization", token);
        }

        [When(@"the Request is executed")]
        public void WhenTheRequestIsExecuted()
        {
            {
                if (request.Method != Method.DELETE || request.Method != Method.GET)
                    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                response = this.restClient.Execute(request);
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
        
        [Then(@"the response reads Processed Successfully")]
        public void ThenTheResponseReadsProcessedSuccessfully()
        {
            if (!response.Content.Contains("Processed Successfully"))
            {
                Assert.Fail("Status code is " + response.Content.ToString() + "\n" + response.Content);
            }
        }

        [Then(@"the response property '(.*)' contains '(.*)'")]
        public void ThenTheResponsePropertyContains(string validationProperty, string validationMessage)
        {
            if (!response.Content.Contains(validationProperty) || !response.Content.Contains(validationMessage))
            {
                Assert.Fail("The response message is missing '" + validationProperty + "' and '" + validationMessage +"'\nActual response:\n" + response.Content);
            }
        }

        [Given(@"the bin appears in inventory")]
        public void GivenTheBinAppearsInInventory()
        {
            this.binValue = DatabaseConnection.doQuery("SELECT TOP 1 BinId from [dbo].[BinInventory] WHERE CurrentLocationType = 'Dc' ORDER BY Id DESC");
            if (binValue.Length != 10)
            {
                Assert.Fail("Could not find a valid Bin Id");
            }
        }


        [Then(@"the bin appears in inventory")]
        public void ThenTheBinAppearsInInventory()
        {
            string foundValue = DatabaseConnection.doQuery($"SELECT TOP 1 BinId from [dbo].[BinInventory] where BinId ='{binValue}'");
            if (foundValue != binValue)
            {
                Assert.Fail("Did not make it to db");
            }
        }

        [Then(@"the inspection record exists on the database")]
        public void ThenTheInspectionRecordExistsOnTheDatabase()
        {
            DataTable foundValue = this.dataRetriever.GetEventById(this.eventId);
            
            if (this.eventId != foundValue.Rows[0].ItemArray[0].ToString())
            {
                Assert.Fail("The record was deleted when it should not have been");
            }
        }

        [Given(@"the Payload is set for adjusting a bin")]
        public void GivenThePayloadIsSetForAdjustingABin()
        {
            json = PayloadGenerator.PostBinAdjustQuantity(this.binValue);
        }


        [Then(@"Status Code is '(.*)'")]
        public void ThenStatusCodeIs(string statuscode)
        {
            if (response.StatusCode.ToString() != statuscode)
            {
                Assert.Fail("Status code is " + response.StatusCode.ToString() + "\n" + response.Content);
            }
        }


        [Then(@"Display Token")]
        public void ThenDisplayToken()
        {
            // GetToken tokenID = new GetToken();
            String token = this.tokenID.ThenSaveTokenID();
            Hooks.scenario.CreateNode<And>(token);
        }

        [Then(@"Response Content")]
        public void ThenResponseContent()
        {
            Hooks.scenario.CreateNode<And>(response.Content.ToString());
        }


        [Given(@"a bin Id that already exists in inventory")]
        public void GivenABinIdThatAlreadyExistsInInventory()
        {
            binValue = DatabaseConnection.doQuery("SELECT TOP 1 BinId from [dbo].[BinInventory] where CurrentLocationType='Dc' ORDER BY Id DESC");
        }

        [Given(@"the Payload is set for PostBinInspection")]
        public void GivenThePayloadIsSetForPostBinInspection()
        {
            json = PayloadGenerator.PostBinInspection(this.binValue);
        }

        [Given(@"the Payload is set for PostBinEditEvent")]
        public void GivenThePayloadIsSetForPostBinEditEvent()
        {
            json = PayloadGenerator.PostBinEdit(this.binValue);
        }


        [Given(@"the Payload is set for PostBinIssuance")]
        public void GivenThePayloadIsSetForPostBinIssuance()
        {
            json = PayloadGenerator.PostBinIssuance(BinIDGenerator.GenerateBinID(), this.binValue);
        }



        [Given(@"Payload is '(.*)'")]
        public void GivenPayloadIs(string path)
        {
            jsonpath = path;
            json = JsonManager.JsonString(@path);


            string checkString = "\"BinId\": \"";
            string bins_checkstring = "\"Bins\": [ \"";
            /*   string checkBinIdList = "BinIdList";
               int checkBinIdListIndex = -1;
               if (json.Contains(checkBinIdList))
               {
                   checkBinIdListIndex = json.IndexOf(checkBinIdList);
               }
               */

            if (json.Contains("EventType"))
            {
                if (json.Contains("BinId"))
                {
                    //int index = json.IndexOf("BinId");
                    List<int> indexesList = BinIDGenerator.AllIndexesOf(json, checkString);


                    foreach (int ind in indexesList)
                    {
                        // if (ind > checkBinIdListIndex)
                        {
                            int startIndex = ind + checkString.Length;
                            int endIndex = startIndex + 10;
                            string check = json.Substring(startIndex, 10);

                            string firstPart = json.Substring(0, startIndex);
                            string midPart = BinIDGenerator.GenerateBinID();
                            string secondPart = json.Substring(endIndex);
                            json = firstPart + midPart + secondPart;
                        }
                    }

                }
                // Console.WriteLine(json);
            }

            else if (json.Contains("IssuanceTagNbr") && json.Contains("Bins"))
            {
                List<int> indexesList = BinIDGenerator.AllIndexesOf(json, bins_checkstring);
                //string binsvalue = Convert.ToString(DatabaseConnection.getBin(ConfigurationManager.AppSettings["postbinissuance"]));
                // string binsvalue = Convert.ToString(DatabaseConnection.getBin("SELECT TOP 1 BinId FROM [dbo].[BinInventory] where CurrentLocationType='Dc' and CurrentStatusType='Receipt' order by Id desc"));

                //string binsvalue = "6804382436";
                string binsvalue = Convert.ToString(DatabaseConnection.getBin(ConfigurationManager.AppSettings["postbinissuance"]));
                foreach (int ind in indexesList)
                {
                    // if (ind > checkBinIdListIndex)
                    {
                        int startIndex = ind + bins_checkstring.Length;
                        int endIndex = startIndex + 10;
                        string check = json.Substring(startIndex, 10);

                        string firstPart = json.Substring(0, startIndex);
                        string midPart = binsvalue;
                        string secondPart = json.Substring(endIndex);
                        json = firstPart + midPart + secondPart;
                    }
                }
            }
        }
    }
}
