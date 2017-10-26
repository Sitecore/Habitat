namespace Sitecore.Feature.Multisite.Tests
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Sitecore.Feature.Multisite.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class SiteConfigurationsTests
  {
    [Theory]
    [AutoDbData]
    public void Current_ShouldReturnSiteDefinitionWithIsCurrentPropertySet(SiteConfigurations siteConfigurations, SiteConfiguration configuration)
    {
      configuration.IsCurrent = true;
      siteConfigurations.Items = new List<SiteConfiguration>
                                 {
                                   configuration
                                 };
      siteConfigurations.Current.ShouldBeEquivalentTo(configuration);
    }
  }
}