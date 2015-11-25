namespace Habitat.Person.Models
{
    using Habitat.Framework.SitecoreExtensions.Model;
    using Sitecore.Mvc.Presentation;

    public class Person : RenderingModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public Image Picture { get; set; }
    }
}