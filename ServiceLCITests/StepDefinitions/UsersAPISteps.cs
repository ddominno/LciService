namespace ServiceLCITests.StepDefinitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json.Linq;

    using TechTalk.SpecFlow;

    [Binding]
    public class UsersAPISteps
    {

        [Then(@"Validate UserID is '(.*)'")]
        public void ThenValidateUserIDIs(string username)
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if(resContent["name"].ToString()!= username)
            {
                Assert.Fail("Username not mathcing");
            }
        }
        [Then(@"Validate Job is '(.*)'")]
        public void ThenValidateJobIs(string job)
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if (resContent["job"].ToString() != job)
            {
                Assert.Fail("Job not mathcing");
            }
        }

    }
}
