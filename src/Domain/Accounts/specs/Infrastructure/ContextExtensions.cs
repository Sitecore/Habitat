using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat.Accounts.Specflow.Steps
{
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
      
  }
}
