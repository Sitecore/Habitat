namespace Sitecore.Foundation.LocalDatasource.Extensions
{
  using System;
  using System.Linq;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public static class ItemExtensions
  {
    public static bool HasLocalDatasourceFolder([NotNull] this Item item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      return item.Children[Settings.LocalDatasourceFolderName] != null;
    }

    public static Item GetLocalDatasourceFolder([NotNull] this Item item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      return item.Children[Settings.LocalDatasourceFolderName];
    }

    public static Item[] GetLocalDatasourceDependencies(this Item item)
    {
      if (!item.HasLocalDatasourceFolder())
      {
        return new Item[]
        {
        };
      }

      var itemLinks = Globals.LinkDatabase.GetReferences(item).Where(r => (r.SourceFieldID == FieldIDs.LayoutField || r.SourceFieldID == FieldIDs.FinalLayoutField) && r.TargetDatabaseName == item.Database.Name);
      return itemLinks.Select(l => l.GetTargetItem()).Where(i => i != null && i.IsLocalDatasourceItem(item)).Distinct().ToArray();
    }

    public static bool IsLocalDatasourceItem([NotNull] this Item dataSourceItem, Item ofItem)
    {
      if (dataSourceItem == null)
      {
        throw new ArgumentNullException(nameof(dataSourceItem));
      }
      var datasourceFolder = ofItem.GetLocalDatasourceFolder();
      return datasourceFolder != null && dataSourceItem.Axes.IsDescendantOf(datasourceFolder);
    }

    public static bool IsLocalDatasourceItem([NotNull] this Item dataSourceItem)
    {
      if (dataSourceItem == null)
      {
        throw new ArgumentNullException(nameof(dataSourceItem));
      }

      if (MainUtil.IsID(Settings.LocalDatasourceFolderTemplate))
      {
        return dataSourceItem.Parent?.TemplateID.Equals(ID.Parse(Settings.LocalDatasourceFolderTemplate)) ?? false;
      }
      return dataSourceItem.Parent?.TemplateName.Equals(Settings.LocalDatasourceFolderTemplate, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }

    public static Item GetParentLocalDatasourceFolder([NotNull] this Item dataSourceItem)
    {
      if (dataSourceItem == null)
      {
        throw new ArgumentNullException(nameof(dataSourceItem));
      }

      var template = dataSourceItem.Database.GetTemplate(Settings.LocalDatasourceFolderTemplate);
      if (template == null)
      {
        Log.Warn($"Cannot find the local datasource folder template '{Settings.LocalDatasourceFolderTemplate}'", dataSourceItem);
        return null;
      }
      return dataSourceItem.Axes.GetAncestors().LastOrDefault(i => i.IsDerived(template.ID));
    }
  }
}