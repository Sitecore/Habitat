namespace Sitecore.Foundation.LocalDatasource.Infrastructure.Indexing
{
  using System.Linq;
  using System.Text;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.ComputedFields;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.LocalDatasource.Extensions;

  public class LocalDatasourceContentField : IComputedIndexField
  {
    public virtual string FieldName { get; set; }
    public virtual string ReturnType { get; set; }

    public virtual object ComputeFieldValue(IIndexable indexable)
    {
      var item = (Item)(indexable as SitecoreIndexableItem);
      if (item == null)
      {
        return null;
      }

      if (!this.ShouldIndexItem(item))
      {
        return null;
      }

      var dataSources = item.GetLocalDatasourceDependencies();

      var result = new StringBuilder();
      foreach (var dataSource in dataSources)
      {
        dataSource.Fields.ReadAll();
        foreach (var field in dataSource.Fields.Where(this.ShouldIndexField))
        {
          result.AppendLine(field.Value);
        }
      }

      return result.ToString();
    }

    private bool ShouldIndexItem(Item item)
    {
      return HasLayout(item) && !item.Paths.LongID.Contains(ItemIDs.TemplateRoot.ToString());
    }

    private static bool HasLayout(Item item)
    {
      return item.Visualization?.Layout != null;
    }

    private bool ShouldIndexField(Field field)
    {
      return !field.Name.StartsWith("__") && this.IsTextField(field) && !string.IsNullOrEmpty(field.Value);
    }

    private bool IsTextField(Field field)
    {
      return IndexOperationsHelper.IsTextField((SitecoreItemDataField)field);
    }
  }
}