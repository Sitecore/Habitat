using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Specflow.Steps
{
  public class PersonNavigationSteps: PersonStepsBase
  {
    [Given(@"Actor navigates to Employees-List page")]
    [When(@"Actor navigates to Employees-List page")]
    public void WhenActorNavigatesToEmployees_ListPage()
    {
      SiteBase.NavigateToPage(BaseSettings.EmployeeList);
    }

  }
}
