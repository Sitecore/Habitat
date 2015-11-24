namespace Habitat.Person.Models
{
    using Sitecore.Mvc.Presentation;

    public class Employee : RenderingModel
    {
        public Person Person { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedInLink { get; set; }
        public string BlogLink { get; set; } 
    }
}