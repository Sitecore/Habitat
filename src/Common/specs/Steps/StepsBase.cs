using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Specflow.Steps
{
  using System.Threading;
  using Common.Specflow.Infrastructure;
  using Common.Specflow.UtfService;
  using OpenQA.Selenium;
  using OpenQA.Selenium.Firefox;
  using TechTalk.SpecFlow;

  public class StepsBase
  {
    [BeforeStep()]
    public static void Timeout()
    {
#warning shitcode
      Thread.Sleep(3000);
    }
    public static IWebDriver Driver
    {
      get { return FeatureContext.Current.Get<IWebDriver>(); }
      set { FeatureContext.Current.Set(value); }
    }

    [BeforeFeature]
    public static void Setup()
    {
      Driver = new FirefoxDriver();
    }

    [AfterScenario]
    public void Cleanup()
    {
      ContextExtensions.CleanupPool.ForEach(CleanupExecute);
    }

    public void EditItem(string idOrPath, string fieldName, string fieldValue, string db = "Master")
    {
      ContextExtensions.UtfService.EditItem(idOrPath, fieldName, fieldValue, Settings.UserName, Settings.Password, (Database)Enum.Parse(typeof(Database), db));
    }

    private void CleanupExecute(TestCleanupAction payload)
    {
      if (payload.ActionType == ActionType.RemoveUser)
      {
        ContextExtensions.HelperService.DeleteUser(payload.GetPayload<string>());
        return;
      }
      if (payload.ActionType == ActionType.CleanFieldValue)
      {
        var fieldPayload = payload.GetPayload<EditFieldPayload>();
        ContextExtensions.UtfService.EditItem(fieldPayload.ItemIdOrPath, fieldPayload.FieldName, fieldPayload.FieldValue, Settings.UserName, Settings.Password, fieldPayload.Database);
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


    [AfterFeature]
    public static void TeardownTest()
    {
      try
      {
        Driver.Quit();
      }
      catch (Exception)
      {
        // Ignore errors if unable to close the browser
      }
    }

    private BaseSettings Settings => new BaseSettings();
  }
}
