namespace ServiceLCITests.StepDefinitions
{
    using System;

    using Newtonsoft.Json.Linq;

    using TechTalk.SpecFlow;

    [Binding]
   public class GetToken
    {

        [Then(@"Save Token ID")]
        public String ThenSaveTokenID()
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());
            var tokenID=   resContent["access_token"].ToString();

            return tokenID;

            
        }

    }
}
