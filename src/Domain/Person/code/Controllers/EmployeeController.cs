namespace Habitat.Person.Controllers
{
    using System.Web.Mvc;
    using Habitat.Person.Repositories;
    using Sitecore.Mvc.Presentation;

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController() : this(new EmployeeRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public ActionResult EmployeeTeaser()
        {
            var item = _employeeRepository.Get();
            return this.View("EmployeeTeaser", item);
        }

        public ActionResult ContactTeaser()
        {
            var item = _employeeRepository.Get();
            return this.View("ContactTeaser", item);
        }

        public ActionResult EmployeeDetails()
        {
            var item = _employeeRepository.Get();
            return this.View("EmployeeDetails", item);
        }
    }
}