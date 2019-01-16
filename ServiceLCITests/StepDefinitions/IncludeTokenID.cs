namespace ServiceLCITests.StepDefinitions
{
    using System;
    using System.Configuration;

    using Newtonsoft.Json.Linq;

    using RestSharp;

    using TechTalk.SpecFlow;

    [Binding]
    public class IncludeTokenID
    {

       
        public static string GivenIncludeToken()
        {

            RestClient restClient;
            RestRequest request;
            IRestResponse response;
          
            string getTokenUrl = ConfigurationManager.AppSettings["testurl"] + "dlcis/api/security/acct/postman.tstst6vA/oktakey";
            restClient = new RestClient(getTokenUrl);
            request = new RestRequest(Method.GET);
            response = restClient.Execute(request);

            var resContent = JObject.Parse(response.Content.ToString());
            var tokenID = resContent["access_token"].ToString();
            APIStepDefinitions.timeTokenGenerated = DateTime.Now;
            return tokenID;

        }

    }
}
