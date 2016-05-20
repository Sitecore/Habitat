using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Feature.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Specflow.Steps
{
  [Binding, Scope(Tag = "UI")]
  public class PersonStepsBase: StepsBase
  {
    public CommonLocators SiteBase = new CommonLocators();

    public CommonActions CommonActions => new CommonActions();

    public CommonResults CommonResults => new CommonResults();

    public NavigationStepsBase NavigationStepsBase => new NavigationStepsBase();

    public PersonLocators PersonLocators => new PersonLocators();
  }
}
