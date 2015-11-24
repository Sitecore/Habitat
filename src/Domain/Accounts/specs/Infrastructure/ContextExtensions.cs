namespace Habitat.Accounts.Specflow.Steps
{
  using System.Collections.Generic;
  using System.ServiceModel;
  using Habitat.Accounts.Specflow.TestHelperService;
  using TechTalk.SpecFlow;

  internal static class ContextExtensions
  {
    public static List<TestCleanupAction> CleanupPool
    {
      get
      {
        if (!ScenarioContext.Current.ContainsKey("cleanup"))
        {
          ScenarioContext.Current.Add("cleanup", new List<TestCleanupAction>());
        }

        return ScenarioContext.Current.Get<List<TestCleanupAction>>("cleanup");
      }
    }

    public static AutoTestsHelperServiceSoapClient HelperService => new AutoTestsHelperServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(Settings.TestHelperService));
  }
}