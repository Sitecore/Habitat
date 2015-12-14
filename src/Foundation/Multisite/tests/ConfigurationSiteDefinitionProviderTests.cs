namespace Sitecore.Foundation.Multisite.Tests
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.Multisite.Tets.Extensions;
  using Xunit;

  public class ConfigurationSiteDefinitionProviderTests
  {
    [Theory]
    [AutoDbData]
    public void SiteDefinitions_ShouldReturnSiteDefinitionFromTheSiteDefinitions(ConfigurationSiteDefinitionsProvider provider)
    {
      var results = provider.SiteDefinitions;
      results.Should().As<IEnumerable<SiteDefinitionItem>>();
    }
  }
}
