namespace Habitat.Office.Controllers
{
  using System;
  using System.Linq;
  using System.Web.Mvc;
  using Habitat.Office.Models;
  using Habitat.Office.Repositories;
  using Sitecore.Data;
  using Sitecore.Mvc.Controllers;
  using Sitecore.Mvc.Presentation;

  public class OfficeController : SitecoreController
  {
    private readonly IOfficeRepository officeRepository;

    public OfficeController() : this(new OfficeRepository())
    {
    }

    public OfficeController(IOfficeRepository officeRepository)
    {
      this.officeRepository = officeRepository;
    }

    public ActionResult OfficeList()
    {
      var offices = officeRepository.GetAll(RenderingContext.Current.Rendering.Item);
      return View(offices);
    }  

    public ActionResult OfficeMaps()
    {
      return View();
    }

    [HttpPost]
    public JsonResult GetOfficeCoordinates(Guid itemId)
    {
      var item = Sitecore.Context.Database.GetItem(new ID(itemId));
      var offices = officeRepository.GetAll(item).Select(i=> new Office(i));

      return Json(offices);
    }
  }
}