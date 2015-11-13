namespace Habitat.Framework.Indexing.Models
{
  using Sitecore.Data.Items;

  public interface ISearchSettings
  {
    Item Root { get; set; }
  }
}