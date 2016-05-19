namespace Sitecore.Feature.Metadata.Tests
{
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Metadata.Models;
  using Sitecore.Feature.Metadata.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class MetadataRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetKeywords_ContextItem_ShouldReturnKeywordsModel(Db db, string contextItemName, string keyword1ItemName, string keyword2ItemName)
    {
      var contextItemId = ID.NewID;
      var keyword1Id = ID.NewID;
      var keyword2Id = ID.NewID;
      db.Add(new DbItem(contextItemName, contextItemId, Templates.PageMetadata.ID)
             {
               new DbField(Templates.PageMetadata.Fields.Keywords)
               {
                 {"en", $"{keyword1Id}|{keyword2Id}"}
               }
             });
      db.Add(new DbItem(keyword1ItemName, keyword1Id, Templates.Keyword.ID)
             {
               new DbField(Templates.Keyword.Fields.Keyword)
               {
                 {"en", keyword1ItemName}
               }
             });
      db.Add(new DbItem(keyword2ItemName, keyword2Id, Templates.Keyword.ID)
             {
               new DbField(Templates.Keyword.Fields.Keyword)
               {
                 {"en", keyword2ItemName}
               }
             });

      var contextItem = db.GetItem(contextItemId);
      var keywordsModel = MetadataRepository.GetKeywords(contextItem);
      keywordsModel.Should().BeOfType<MetaKeywordsModel>();
      keywordsModel.Keywords.Count().Should().Be(2);
    }


    [Theory]
    [AutoDbData]
    public void GetKeywords_ContextItemWithWrongTemplate_ShouldReturnNull([Content] Item contextItem)
    {
      MetadataRepository.GetKeywords(contextItem).Should().BeNull();
    }


    [Theory]
    [AutoDbData]
    public void Get_ContextItemIsMatchingTemplate_ShouldReturnSelf(Db db, string contextItemName)
    {
      var contextItemId = ID.NewID;

      db.Add(new DbItem(contextItemName, contextItemId, Templates.SiteMetadata.ID));

      var contextItem = db.GetItem(contextItemId);
      var keywordsModel = MetadataRepository.Get(contextItem);
      keywordsModel.ID.Should().Be(contextItemId);
    }

    [Theory]
    [AutoDbData]
    public void Get_ContextItemParentIsMatchingTemplate_ShouldReturnParent(Db db)
    {
      var contextItemId = ID.NewID;

      db.Add(new DbItem("context", contextItemId, Templates.SiteMetadata.ID));
      var contextItem = db.GetItem(contextItemId);
      var keywordsModel = MetadataRepository.Get(contextItem.Add("child", new TemplateID(ID.NewID)));

      keywordsModel.ID.Should().Be(contextItemId);
    }
  }
}