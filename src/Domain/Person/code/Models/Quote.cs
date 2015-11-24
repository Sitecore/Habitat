namespace Habitat.Person.Models
{
    using Sitecore.Mvc.Presentation;

    public class Quote : RenderingModel
    {
        public Person Person { get; set; }
        public string Company { get; set; }
        public string Quotation { get; set; }
    }
}