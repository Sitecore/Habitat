namespace Sitecore.Foundation.MultiSite.Tests
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Foundation.MultiSite.Tests.Extensions;
  using Xunit;

  public class ConfigurationSiteDefinitionProviderTests
  {
    [Theory]
    [AutoDbData]
    public void SiteDefinitions_ShouldReturnSiteDefinitionFromTheSiteDefinitions(ConfigurationSiteDefinitionsProvider provider)
    {
      var results = provider.SiteDefinitions;
      results.Should().As<IEnumerable<SiteDefinition>>();
    }
  }
}
