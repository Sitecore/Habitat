using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Extensions;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using Sitecore.Foundation.Forms.Specflow.Infrostructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Forms.Specflow.Steps
{
  [Binding]
  public class FormStepsBase : StepsBase
  {
    public NavigationStepsBase NavigationStepsBase => new NavigationStepsBase();

    public CommonLocators BaseLocators => new CommonLocators();

    public BaseSettings BaseSettings => new BaseSettings();

    public CommonResults CommonResults => new CommonResults();

    public FormLocators FormLocators = new FormLocators();

    public XDbPanelResults XDbPanelResults = new XDbPanelResults();


  }
}
