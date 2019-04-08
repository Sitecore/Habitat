﻿#region using



#endregion

namespace Sitecore.Foundation.Indexing.Infrastructure.Fields
{
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.ComputedFields;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class HasPresentationComputedField : IComputedIndexField
  {
    public string FieldName { get; set; }

    public string ReturnType { get; set; }

    public object ComputeFieldValue(IIndexable indexable)
    {
      Item i = indexable as SitecoreIndexableItem;
      if (i.HasLayout())
      {
        return true;
      }
      return null;
    }
  }
}