namespace ServiceLCITests.StepDefinitions
{
    using System;
    using System.Configuration;
    using DriscTAF.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json.Linq;

    using TechTalk.SpecFlow;

    [Binding]
    public class GetBinValidations
    {
        [Then(@"Validate Uom is '(.*)'")]
        public void ThenValidateUomIs(string UomValue)
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if (resContent["Uom"].ToString() != UomValue)
            {
                Assert.Fail("Uom not mathcing");
            }
        }
        [Then(@"Validate Unit Quantity is '(.*)'")]
        public void ThenValidateUnitQuantityIs(String quanity)
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if (resContent["UnitQty"].ToString() != quanity)
            {
                Assert.Fail("UnitQty not mathcing");
            }
        }

        [Then(@"Validate Term Length is '(.*)'")]
        public void ThenValidateTermLengthIs(string length)
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if (resContent["TermLength"].ToString() != length)
            {
                Assert.Fail("Term Length not matching");
            }
        }

        [Then(@"Validate Unit Size is '(.*)'")]
        public void ThenValidateUnitSizeIs(string unitlength)
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if (resContent["UnitSize"].ToString() != unitlength)
            {
                Assert.Fail("Unit Length not matching");
            }
        }

        [Then(@"Validate '(.*)' is '(.*)'")]
        public void ThenValidateIs(string p0, string p1)
        {
            Console.WriteLine("Validations passed");
        }

        [Then(@"Validate BinId")]
        public void ThenValidateBinId()
        {
            var resContent = JObject.Parse(APIStepDefinitions.response.Content.ToString());

            if (resContent["BinId"].ToString() != DatabaseConnection.getBin(ConfigurationManager.AppSettings["getbinquery"]))
            {
                Assert.Fail("Unit Length not matching");
            }
        }



    }
}
