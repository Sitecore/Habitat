using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.TestHelperService;
using System.Collections.Generic;
using System.ServiceModel;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{


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

    public static AutoTestsHelperServiceSoapClient HelperService => new AutoTestsHelperServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(BaseSettings.TestHelperService));
    public static HelperWebServiceWrapper UtfService => new HelperWebServiceWrapper();
  }
}