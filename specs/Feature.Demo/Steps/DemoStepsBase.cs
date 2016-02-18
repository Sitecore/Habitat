using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Feature.Demo.Specflow.Infrastructure;
using Sitecore.Feature.Demo.Specflow.Settings;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Demo.Specflow.Steps
{
  [Binding]
  public class DemoStepsBase:StepsBase
  {
    public SiteDemo SiteDemo = new SiteDemo();

    public SiteBase SiteBase = new SiteBase();

    public BaseSettings BaseSettings = new BaseSettings();

  }
}
