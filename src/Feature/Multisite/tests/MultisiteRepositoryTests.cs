namespace Sitecore.Feature.Multisite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Sitecore.Feature.Multisite.Repositories;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Sitecore.Foundation.Multisite.Providers;
  using Xunit;

  public class MultisiteRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Constructor_InstanceOfISiteDefinitionInterface_InstanceShouldBeCreated(ISiteDefinitionsProvider provider, MultisiteRepository multisiteRepository)
    {
      Action action = () => new MultisiteRepository(provider);
      action.ShouldNotThrow();
    }
  }
}
