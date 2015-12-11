using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Multisite.Tests
{
  using FluentAssertions;
  using Sitecore.Feature.Multisite.Models;
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
