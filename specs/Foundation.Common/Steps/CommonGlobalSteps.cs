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

    [BeforeFeature]
    public static void Setup()
    {
      FeatureContext.Current.Set((IWebDriver)new FirefoxDriver()); ;
    }
    [AfterScenario]
    public void Cleanup()
    {
      ContextExtensions.CleanupPool.ForEach(CleanupExecute);
    }

   

    public void EditItem(string idOrPath, string fieldName, string fieldValue, string db = "Master")
    {
      ContextExtensions.UtfService.EditItem(idOrPath, fieldName, fieldValue, BaseSettings.UserName, BaseSettings.Password, (Database)Enum.Parse(typeof(Database), db));
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

      throw new NotSupportedException($"Action type '{payload.ActionType}' is not supported");
    }


    [Given(@"Value set to item field")]
    public void GivenValueSetToItemField(IEnumerable<ItemFieldDefinition> fields)
    {
      foreach (var field in fields)
      {
        EditItem(field.ItemPath, field.FieldName, field.FieldValue);
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