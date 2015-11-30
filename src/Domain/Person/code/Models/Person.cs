namespace Habitat.Person.Models
{
    using System.Web;
    using Sitecore.Mvc.Presentation;

    public class Person : RenderingModel
    {
        public HtmlString Name { get; set; }
        public HtmlString Title { get; set; }
        public HtmlString Picture { get; set; }
    }
}