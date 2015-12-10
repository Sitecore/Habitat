namespace Common.Specflow.Infrastructure
{
  using System.Collections.Generic;
  using System.ServiceModel;
  using Common.Specflow.TestHelperService;
  using Common.Specflow.UtfService;
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