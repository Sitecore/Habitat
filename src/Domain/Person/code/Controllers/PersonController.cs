namespace Habitat.Person.Controllers
{
    using System.Web.Mvc;
    using Habitat.Person.Repositories;
    using Sitecore.Mvc.Presentation;

    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController() : this(new PersonRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public PersonController(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }

        public ActionResult PersonPhoto()
        {
            var item = this._personRepository.Get();
            return this.View("PersonPhoto", item);
        }
    }
}