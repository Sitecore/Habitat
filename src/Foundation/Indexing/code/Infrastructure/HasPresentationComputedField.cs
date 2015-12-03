#region using



#endregion

namespace Sitecore.Foundation.Indexing.Infrastructure
{
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.ComputedFields;
  using Sitecore.Data.Items;

  public class HasPresentationComputedField : IComputedIndexField
  {
    public string FieldName { get; set; }

    public string ReturnType { get; set; }

    public object ComputeFieldValue(IIndexable indexable)
    {
      Item i = indexable as SitecoreIndexableItem;
      if (i?.Visualization.Layout != null)
      {
        return true;
      }
      return null;
    }
  }
}