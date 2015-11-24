namespace Habitat.Person.Models
{
    using Habitat.Framework.SitecoreExtensions.Model;
    using Sitecore.Data.Items;
    using Sitecore.Mvc.Presentation;

    public class Person : RenderingModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public Image Image { get; set; }
        public Item Item { get; set; }
    }
}