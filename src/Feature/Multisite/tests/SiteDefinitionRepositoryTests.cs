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
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Feature.MultiSite.Repositories;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Publishing.Explanations;
  using Sitecore.SecurityModel;
  using Xunit;

  public class SiteDefinitionRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Constructor_InstanceOfISiteDefinitionInterface_InstanceShouldBeCreated(ISiteDefinitionsProvider provider, SiteDefinitionsRepository multisiteRepository)
    {
      Action action = () => new SiteDefinitionsRepository(provider);
      action.ShouldNotThrow();
    }

    [Theory]
    [AutoDbData]
    public void GetSiteDefinitions_ShouldReturnSiteDefinitiosModel([Frozen]ISiteDefinitionsProvider siteDefinitionProvider, [Greedy] SiteDefinitionsRepository repository, string name)
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

      siteDefinitionProvider.SiteDefinitions.Returns(new List<Foundation.MultiSite.SiteDefinition> { new Foundation.MultiSite.SiteDefinition { Item = item } });
      var definitions = repository.Get();
      definitions.Should().BeOfType<SiteDefinitions>();
      var sites = definitions.Sites.ToList();
      sites.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [AutoDbData]
    public void SiteDefinitionRepository_Call_UpdateSiteItem([Content]SiteTemplate siteTemplate, [Content]Item rootItem, string name, string hostName, bool showInMenu, Foundation.MultiSite.SiteDefinition siteDefinition, [Frozen]ISiteDefinitionsProvider sideDefinitionsProvider, [Greedy]SiteDefinitionsRepository siteDefinitionRepository)
    {
      //Arrange
      var siteItem = rootItem.Add("Site", new TemplateID(siteTemplate.ID));
      siteDefinition.Name = name;
      siteDefinition.Item = siteItem;
      sideDefinitionsProvider.SiteDefinitions.Returns(new[] { siteDefinition });

      //Act
      using (new SecurityDisabler())
      {
        siteDefinitionRepository.ConfigureHostName(name, hostName, showInMenu);
      }

      //Assert
      siteItem[Templates.Site.Fields.HostName].Should().Be(hostName);
      siteItem[MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu].Should().Be(showInMenu? "1":"0");
    }

    public class SiteTemplate : DbTemplate
    {
      public SiteTemplate()
      {
        this.Add(Templates.Site.Fields.HostName);
        this.Add(MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu);
      }
    }
  }
}
