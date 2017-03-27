namespace Sitecore.Foundation.Multisite.Providers
{
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public interface IFieldDatasourceProvider
    {
        Item[] GetDatasourceLocation(Item contextItem, string name);
    }
}
