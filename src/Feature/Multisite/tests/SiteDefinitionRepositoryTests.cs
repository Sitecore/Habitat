namespace Sitecore.Feature.MultiSite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Feature.MultiSite.Repositories;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.MultiSite.Providers;
  using Xunit;

  public class SiteDefinitionRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Constructor_InstanceOfISiteDefinitionInterface_InstanceShouldBeCreated(ISiteDefinitionsProvider provider, SiteDefinitionRepositoryRepository multisiteRepository)
    {
      Action action = () => new SiteDefinitionRepositoryRepository(provider);
      action.ShouldNotThrow();
    }

    [Theory]
    [AutoDbData]
    public void GetSiteDefinitions_ShouldReturnSiteDefinitiosModel([Frozen]ISiteDefinitionsProvider siteDefinitionProvider, [Greedy] SiteDefinitionRepositoryRepository repository, string name)
    {
      var id = ID.NewID;
      var db = new Db
      {
        new DbItem(name, id, MultiSite.Templates.SiteConfiguration.ID)
        {
          new DbField(MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu)
          {
            {"en", "1"}
          }
        }
      };

      var item = db.GetItem(id);

      siteDefinitionProvider.SiteDefinitions.Returns(new List<SiteDefinitionItem> {new SiteDefinitionItem {Item = item} });
      var definitions = repository.Get();
      definitions.Should().BeOfType<SiteDefinitions>();
      var sites = definitions.Sites.ToList();
      sites.Count.Should().BeGreaterThan(0);
    }
  }
}
