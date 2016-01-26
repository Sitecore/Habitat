﻿namespace Sitecore.Feature.Multisite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Mvc;
  using FluentAssertions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Sitecore.Feature.Multisite.Controllers;
  using Sitecore.Feature.Multisite.Models;
  using Sitecore.Feature.Multisite.Repositories;
  using Xunit;

   
  public class MultisiteControllerTests
  {
    [Theory]
    [AutoDbData]
    public void SwitchSite_ShouldReturnViewActionWithSiteDefinitionsModel([Frozen]ISiteConfigurationRepository repository, [Greedy]MultisiteController controller, MultisiteController multisiteController)
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
