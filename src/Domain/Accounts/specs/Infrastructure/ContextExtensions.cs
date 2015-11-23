using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat.Accounts.Specflow.Steps
{
  using System.ServiceModel;
  using Habitat.Accounts.Specflow.TestHelperService;
  using Habitat.Accounts.Specflow.TestsHelperService;
  using TechTalk.SpecFlow;

  static class ContextExtensions
  {
    public static List<TestCleanupAction> CleanupPool {
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
