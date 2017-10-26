namespace Sitecore.Feature.Multisite.Tests
{
  using System;
  using System.Web.Mvc;
  using FluentAssertions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.Multisite.Controllers;
  using Sitecore.Feature.Multisite.Models;
  using Sitecore.Feature.Multisite.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class MultisiteControllerTests
  {
    [Theory]
    [AutoDbData]
    public void SwitchSite_ShouldReturnViewActionWithSiteDefinitionsModel([Frozen] ISiteConfigurationRepository repository, [Greedy] MultisiteController controller, MultisiteController multisiteController)
    {
      var result = controller.SwitchSite();
      result.Should().BeOfType<ViewResult>().Which.Model.As<SiteConfigurations>();
    }

    [Theory]
    [AutoDbData]
    public void DefaultConstructor_ShouldNotThrow()
    {
      Action action = () => new MultisiteController();
      action.ShouldNotThrow();
    }
  }
}