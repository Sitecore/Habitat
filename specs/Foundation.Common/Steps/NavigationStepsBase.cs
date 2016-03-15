using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Sitecore.Foundation.Common.Specflow.Extensions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  public class NavigationStepsBase:StepsBase
  {


    [Given(@"Habitat website is opened on Main Page")]
    public void GivenHabitatWebsiteIsOpenedOnMainPage()
    {
      SiteBase.NavigateToPage(BaseSettings.BaseUrl);
    }

    [Given(@"Actor moves cursor over the User icon")]
    [When(@"Actor moves cursor over the User icon")]
    public void WhenActorMovesCursorOverTheUserIcon()
    {
      //TODO: Old code
      /*
      SiteBase.UserIcon.MoveToElement();

#warning hack for selenium hover behavoiur
      var dropdown = SiteBase.UserIcon.FindElement(By.XPath("../../ul"));
      var js = Driver as IJavaScriptExecutor;
      js?.ExecuteScript("arguments[0].style.display='block'", dropdown);*/
    }




  }
}
