using FluentAssertions;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using Sitecore.Foundation.Common.Specflow.Infrastructure;
    using Sitecore.Foundation.Common.Specflow.Service_References.UtfService;
    using TechTalk.SpecFlow;

    [Binding]
    public class CommonGlobalSteps
    {
        [BeforeStep]
        public static void Timeout()
        {
#warning shitcode
            Thread.Sleep(3000);
        }

        [BeforeFeature("UI")]
        public static void Setup()
        {
            FeatureContext.Current.Set((IWebDriver)new FirefoxDriver()); ;
        }

        [AfterScenario]
        public void Cleanup()
        {
            ContextExtensions.CleanupPool.ForEach(CleanupExecute);
        }


        public void CreateItem(string parentIdOrPath, string fieldName, string fieldValue, string db = "Master")
        {
            ContextExtensions.UtfService.CreateItem(fieldName, parentIdOrPath, fieldValue, BaseSettings.UserName,
                BaseSettings.Password, (Database)Enum.Parse(typeof(Database), db));
        }


        public void EditItem(string idOrPath, string fieldName, string fieldValue, string db = "Master")
        {
            ContextExtensions.UtfService.EditItem(idOrPath, fieldName, fieldValue, BaseSettings.UserName, BaseSettings.Password, (Database)Enum.Parse(typeof(Database), db));
        }

        public void GetItemFieldValue(string idOrPath, string db = "Master")
        {
            ContextExtensions.UtfService.ItemExistsByPath(idOrPath, (Database)Enum.Parse(typeof(Database), db))
                .Should().NotBeEmpty();
        }

        private static void CleanupExecute(TestCleanupAction payload)
        {
            if (payload.ActionType == ActionType.RemoveUser)
            {
                ContextExtensions.HelperService.DeleteUser(payload.GetPayload<string>());
                return;
            }
            if (payload.ActionType == ActionType.CleanFieldValue)
            {
                var fieldPayload = payload.GetPayload<EditFieldPayload>();
                ContextExtensions.UtfService.EditItem(fieldPayload.ItemIdOrPath, fieldPayload.FieldName, fieldPayload.FieldValue, BaseSettings.UserName, BaseSettings.Password, fieldPayload.Database);
                return;
            }

            if (payload.ActionType == ActionType.DeleteItem)
            {
                var fieldPayload = payload.GetPayload<EditFieldPayload>();
                ContextExtensions.UtfService.DeleteItem(fieldPayload.ItemIdOrPath, fieldPayload.Database, false);
                return;
            }

            throw new NotSupportedException($"Action type '{payload.ActionType}' is not supported");
        }


        [Given(@"Value set to item field")]
        [Given(@"en is Selected on the following item")]
        [When(@"The following languages have been selected")]
        [When(@"Value set to item field")]
        [When(@"The sitecore keyword has been selected")]
        public void GivenValueSetToItemField(IEnumerable<ItemFieldDefinition> fields)
        {
            foreach (var field in fields)
            {
                EditItem(field.ItemPath, field.FieldName, field.FieldValue);
            }
        }

        [Given(@"Following languages defined in Sitecore")]
        [Given(@"The following Metadata keywords are defined in Sitecore")]
        [When(@"Admin opens following item")]
        [Then(@"new item with title NewKeyWord should be added under following item")]
        public void GivenValueGetItemField(IEnumerable<ItemFieldDefinition> fields)
        {
            foreach (var field in fields)
            {
                GetItemFieldValue(field.ItemPath);
            }
        }

        [Given(@"Admin create a new Metakeyword")]
        public void CreateItem(IEnumerable<ItemFieldDefinition> fields)
        {
            foreach (var field in fields)
            {
                CreateItem(field.ItemPath, field.FieldName, field.FieldValue);
            }
        }




        [Given(@"Actor Ends user visit")]
        [When(@"Actor Ends user visit")]
        public void WhenActorEndsUserVisit()
        {
            FeatureContext.Current.Get<IWebDriver>().Navigate().GoToUrl(BaseSettings.EndVisitUrl);
        }



        [AfterFeature]
        public static void TeardownTest()
        {
            try
            {
                FeatureContext.Current.Get<IWebDriver>().Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        private static BaseSettings Settings => new BaseSettings();
    }
}