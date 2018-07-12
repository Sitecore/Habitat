#region using



#endregion

namespace Sitecore.Foundation.Indexing.Infrastructure.Fields
{
  using System.Collections.Generic;
  using Sitecore;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.ComputedFields;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data.Items;

  public class AllTemplatesComputedField : IComputedIndexField
  {
    public string FieldName { get; set; }
    public string ReturnType { get; set; }

    public object ComputeFieldValue(IIndexable indexable)
    {
      var indexItem = indexable as SitecoreIndexableItem;
      if (indexItem == null)
      {
        return null;
      }
      var item = indexItem.Item;

      var templates = new List<string>();
      this.GetAllTemplates(item.Template, templates);
      return templates;
    }

    public void GetAllTemplates(TemplateItem baseTemplate, List<string> list)
    {
      var str = IdHelper.NormalizeGuid(baseTemplate.ID);
      list.Add(str);
      if (baseTemplate.ID == TemplateIDs.StandardTemplate)
      {
        return;
      }
      foreach (var item in baseTemplate.BaseTemplates)
      {
        this.GetAllTemplates(item, list);
      }
    }
  }
}