namespace Sitecore.Feature.Metadata.Repositories
{
  using System.Linq;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Feature.Metadata.Models;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public static class MetadataRepository
  {
    public static Item Get(Item contextItem)
    {
      return contextItem.GetAncestorOrSelfOfTemplate(Templates.SiteMetadata.ID) ?? Context.Site.GetContextItem(Templates.SiteMetadata.ID);
    }

    public static MetaKeywordsModel GetKeywords(Item item)
    {
      if (item.IsDerived(Templates.PageMetadata.ID))
      {
        var keywordsField = item.Fields[Templates.PageMetadata.Fields.Keywords];
        if (keywordsField == null)
        {
          return null;
        }

        var keywordMultilist = new MultilistField(keywordsField);
        var keywords = keywordMultilist.GetItems().Select(keywrdItem => keywrdItem[Templates.Keyword.Fields.Keyword]);
        var metaKeywordModel = new MetaKeywordsModel
                               {
                                 Keywords = keywords.ToList()
                               };

        return metaKeywordModel;
      }

      return null;
    }
  }
}