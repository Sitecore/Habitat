namespace Sitecore.Foundation.Multisite.Providers
{
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public interface IDatasourceProvider
  {
    Item[] GetDatasources(string name, Item contextItem);

    Item GetDatasourceTemplate(string name, Item contextItem);

    Database Database { get; set; }
  }
}
