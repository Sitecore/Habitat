namespace Sitecore.Feature.MultiSite.Tests
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Xunit;

  public class SiteDefinitionsTests
  {
    [Theory]
    [AutoDbData]
    public void Current_ShouldReturnSiteDefinitionWithIsCurrentPropertySet(SiteDefinitions siteDefinitions, SiteDefinition definition)
    {
      definition.IsCurrent = true;
      siteDefinitions.Sites = new List<SiteDefinition> {definition};
      siteDefinitions.Current.ShouldBeEquivalentTo(definition);
    }
  }
}
