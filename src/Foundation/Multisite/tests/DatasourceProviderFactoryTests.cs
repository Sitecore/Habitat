namespace Sitecore.Foundation.MultiSite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Foundation.MultiSite.Tests.Extensions;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.FakeDb;

  public class DatasourceProviderFactoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetProvider_ConfigurationIsNotDefined_ShouldReturnNull(DatasourceProviderFactory factory, Database db)
    {
      var provider = factory.GetProvider(db);
      provider.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void GetFallbackProvider_ConfigurationIsNotDefined_ShouldReturnNull(DatasourceProviderFactory factory, Database db)
    {
      var provider = factory.GetFallbackProvider(db);
      provider.Should().BeNull();
    }
  }
}
