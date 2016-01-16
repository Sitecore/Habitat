namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System.Collections.Generic;
  using System.ServiceModel;
  using TechTalk.SpecFlow;

  public static class ContextExtensions
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

    public static AutoTestsHelperServiceSoapClient HelperService => new AutoTestsHelperServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(new BaseSettings().TestHelperService));
    public static HelperWebServiceSoapClient UtfService => new HelperWebServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(new BaseSettings().UtfHelperService));
  }
}