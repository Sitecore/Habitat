namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System.Collections.Generic;
  using System.Linq;
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using Sitecore.Foundation.Common.Specflow.Steps;
  using TechTalk.SpecFlow;

  public class CommonLocators
  {
    private readonly FeatureContext featureContext;

    public CommonLocators(FeatureContext featureContext)
    {
      this.featureContext = featureContext;
    }

    public IWebDriver Driver => this.featureContext.Driver();

    public IWebElement SiteSwitcherIcon
      => this.Driver.FindElement(By.CssSelector(".fa.fa-home"));

    public IEnumerable<IWebElement> SiteSwitcherIconDropDownChildElements
      => this.Driver.WaitUntilElementsPresent(By.CssSelector(".dropdown-menu li a, .active a"));

    public IEnumerable<IWebElement> DropDownActiveValues
      => this.Driver.FindElements(By.CssSelector(".dropdown-menu li.active"));


    public IEnumerable<IWebElement> SiteSwitcherelements
      => this.Driver.FindElements(By.CssSelector(".dropdown-menu>li>a"));

    public IWebElement DemoSiteLogo
      => this.Driver.FindElement(By.CssSelector("#hplogo"));

    public IEnumerable<IWebElement> OnsideBehaviorData 
      => this.Driver.FindElements(By.CssSelector(".list-unstyled li div"));

    public void NavigateToPage(string url)
    {
      this.Driver.Navigate().GoToUrl(url);
    }

    public void ExperianceEditor(string url)
    {
      this.Driver.Navigate().GoToUrl(url);
    }

    public IWebElement SubmitButton => this.Driver.FindElement(By.CssSelector("input[type=submit]"));

    public IEnumerable<IWebElement> RegisterPageFields
      => this.Driver.FindElements(By.CssSelector("#registerEmail, #registerPassword, #registerConfirmPassword"));

    public IWebElement UserIcon => this.Driver.FindElement(By.CssSelector(".fa-user"));

    public IEnumerable<IWebElement> UserIconButtons
      => this.Driver.FindElements(By.CssSelector(".btn.btn-block.btn-primary, .btn.btn-block.btn-default"));

    public IEnumerable<IWebElement> UserIconDropDownButtonLinks
      => this.Driver.FindElements(By.CssSelector(".btn.btn-block.btn-primary"));

    public IEnumerable<IWebElement> LoginPageLinks
      => this.Driver.WaitUntilElementsPresent(By.CssSelector(".btn.btn-link, .btn.btn-default.btn-lg.btn-block")).Where(el => el.Displayed).ToList();

    public IEnumerable<IWebElement> LoginPageButtons
      => this.Driver.FindElements(By.CssSelector(".btn.btn-primary.btn-lg.btn-block")).Where(el => el.Displayed).ToList();

    public IEnumerable<IWebElement> OpenXdbSlidebar
      => this.Driver.WaitUntilElementsPresent(By.CssSelector(".btn.btn-info.sidebar-closed"));

    public IEnumerable<IWebElement> XdBpanelHeader
      => this.Driver.WaitUntilElementsPresent(By.CssSelector(".panel-title.collapsed"));

    public IEnumerable<IWebElement> UserIconOnPersonalInformation
      => this.Driver.FindElements(By.CssSelector(".panel-body div div .fa.fa-user"));

    public IEnumerable<IWebElement> MediaTitleOnPersonalInformation
      => this.Driver.FindElements(By.CssSelector(".panel-body div div .media-title"));

    public IEnumerable<IWebElement> IdentificationUknownStatusIcon
      => this.Driver.FindElements(By.CssSelector(".panel-body div div .fa.fa-user-secret.icon-lg"));

    public IEnumerable<IWebElement> IdentificationKnownStatusIcon
      => this.Driver.FindElements(By.CssSelector(".fa.fa-user-plus.icon-lg"));

    public IEnumerable<IWebElement> XdBpanelMediaBody
      => this.Driver.FindElements(By.CssSelector(".media-body"));

    public IEnumerable<IWebElement> ManageXdBpanelButtons
      => this.Driver.FindElements(By.CssSelector(".hover-only"));

    public IWebElement GlobeIcon => this.Driver.FindElement(By.CssSelector(".navbar-right .fa.fa-globe"));

    public bool GlobeIconExists() => this.Driver.FindElements(By.CssSelector(".navbar-right .fa.fa-globe")).Any();

    public IEnumerable<IWebElement> GlobeIconList
      => this.Driver.WaitUntilElementsPresent(By.CssSelector(".navbar-right .dropdown.open .dropdown-menu a"));

    public IEnumerable<IWebElement> MetakeywordTag => this.Driver.FindElements(By.CssSelector("meta[name=keywords]"));

    public IEnumerable<IWebElement> DatasourceCommand => this.Driver.WaitUntilElementsPresent(By.CssSelector(".scChromeToolbar.undefined a.scChromeCommand[title='Associate a content item with this component.']"));

    public IEnumerable<IWebElement> SitecoreLoginFields => this.Driver.WaitUntilElementsPresent(By.CssSelector("#UserName, #Password"));

    public IEnumerable<IWebElement> TwitterPlaceholder => this.Driver.FindElements(By.CssSelector(".well.bg-dark.scEnabledChrome"));

    public IEnumerable<IWebElement> TwitterTreeContent
      => this.Driver.FindElements(By.CssSelector(".scContentTreeNodeTitle"));


    public void WaitRibbonPreLoadingIndicatorInvisible()
    {
      this.Driver.WaitUntilElementsInvisible(By.CssSelector("#ribbonPreLoadingIndicator"));
    }

    public void NavigateToExperienceEditorDialogWindow()
    {
      this.Driver.SwitchTo().Frame(this.Driver.FindElement(By.Id("jqueryModalDialogsFrame")));
      this.Driver.SwitchTo().Frame(this.Driver.FindElement(By.Id("scContentIframeId0")));
    }

    public void NavigateFromExperienceEditorDialogWindow()
    {
      this.Driver.SwitchTo().DefaultContent();
    }
  }
}