using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Feature.Demo.Specflow.Infrastructure;
using Sitecore.Feature.Demo.Specflow.Settings;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Demo.Specflow.Steps
{
  [Binding]
  public class DemoStepsBase:StepsBase
  {

    public DemoLocators DemoLocators = new DemoLocators();

    public CommonLocators BaseLocators => new CommonLocators();

    public BaseSettings BaseSettings => new BaseSettings();

    public CommonResults CommonResults => new CommonResults();   
  }
}
