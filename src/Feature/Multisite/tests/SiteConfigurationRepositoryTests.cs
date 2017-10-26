namespace Sitecore.Feature.Multisite.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.FakeDb;
    using Sitecore.Feature.Multisite.Models;
    using Sitecore.Feature.Multisite.Repositories;
    using Sitecore.Foundation.Multisite;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Web;
    using Xunit;

    public class SiteConfigurationRepositoryTests
    {
        [Theory]
        [AutoDbData]
        public void Constructor_InstanceOfISiteDefinitionInterface_InstanceShouldBeCreated(ISiteDefinitionsProvider provider, SiteConfigurationRepository multisiteRepository)
        {
            Action action = () => new SiteConfigurationRepository(provider);
            action.ShouldNotThrow();
        }

        [Theory]
        [AutoDbData]
        public void GetSiteDefinitions_ShouldReturnSiteDefinitiosModel([Frozen] ISiteDefinitionsProvider siteDefinitionProvider, [Greedy] SiteConfigurationRepository repository, string name)
        {
            var id = ID.NewID;
            var db = new Db
            {
                new DbItem(name, id, Multisite.Templates.SiteConfiguration.ID)
                {
                    new SharedDbField(Multisite.Templates.SiteConfiguration.Fields.ShowInMenu) {Value = "1"}
                }
            };
            var item = db.GetItem(id);

            var siteSettings = new StringDictionary()
                         {
                           {"name", name},
                         };

            siteDefinitionProvider.SiteDefinitions.Returns(new List<SiteDefinition> {new SiteDefinition {Item = item, Site = new SiteInfo(siteSettings) } });
            var definitions = repository.Get();
            definitions.Should().BeOfType<SiteConfigurations>();
            var sites = definitions.Items.ToList();
            sites.Count.Should().BeGreaterThan(0);
        }
    }
}