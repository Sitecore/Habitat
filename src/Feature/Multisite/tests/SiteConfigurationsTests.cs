namespace Sitecore.Feature.MultiSite.Tests
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Xunit;

  public class SiteConfigurationsTests
  {
    [Theory]
    [AutoDbData]
    public void Current_ShouldReturnSiteDefinitionWithIsCurrentPropertySet(SiteConfigurations siteConfigurations, SiteConfiguration configuration)
    {
      configuration.IsCurrent = true;
      siteConfigurations.Items = new List<SiteConfiguration> {configuration};
      siteConfigurations.Current.ShouldBeEquivalentTo(configuration);
    }
  }
}
