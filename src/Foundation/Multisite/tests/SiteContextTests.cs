namespace Sitecore.Foundation.Multisite.Tests
{
    using FluentAssertions;
    using NSubstitute;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Multisite.Tests.Extensions;
    using Xunit;

    public class SiteContextTests
  {
    [Theory]
    [AutoDbData]
    public void GetSiteDefinition_ProviderReturnsDefinition_ShouldReturnDefinition(ISiteDefinitionsProvider provider, DbItem item, Db db, string siteName)
    {
      var siteDefinitionId = ID.NewID;
      db.Add(new DbItem(siteName, siteDefinitionId, Templates.Site.ID) { item });
      var definitionItem = db.GetItem(siteDefinitionId);

      var definition = new SiteDefinition();
      definition.Item = definitionItem;
      provider.GetContextSiteDefinition(Arg.Any<Item>()).Returns(definition);

      var siteContext = new SiteContext(provider);

      var contextItem = db.GetItem(item.ID);
      var siteDefinition = siteContext.GetSiteDefinition(contextItem);

      siteDefinition.Item.ID.Should().BeEquivalentTo(definitionItem.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetSiteDefinition_ProviderReturnsEmpty_ShouldReturnNull(ISiteDefinitionsProvider provider, DbItem item, Db db, string siteName)
    {
      db.Add(item);
      var contextItem = db.GetItem(item.ID);

      provider.GetContextSiteDefinition(Arg.Any<Item>()).Returns((SiteDefinition)null);

      var siteContext = new SiteContext(provider);
      siteContext.GetSiteDefinition(contextItem).Should().BeNull();
    }
  }
}
