namespace Habitat.Person.Models
{
    using System.Web;
    using Sitecore.Mvc.Presentation;

    public class Quote : RenderingModel
    {
        public Person Person { get; set; }
        public HtmlString Company { get; set; }
        public HtmlString Quotation { get; set; }
    }
}