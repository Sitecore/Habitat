namespace Sitecore.Foundation.Dictionary.Models
{
    using Sitecore.Data.Items;
    using Sitecore.Sites;

    public class Dictionary
  {
    public Item Root { get; set; }
    public bool AutoCreate { get; set; }
    public SiteContext Site { get; set; }
  }
}