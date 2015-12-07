namespace Habitat.Maps.Controllers
{
  using System;
  using System.Linq;
  using System.Web.Mvc;
  using Habitat.Maps.Models;
  using Habitat.Maps.Repositories;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Mvc.Controllers;
  using Sitecore.Mvc.Presentation;

  public class MapsController : SitecoreController
  {
    private readonly IMapRepository mapRepository;

    public MapsController() : this(new MapRepository())
    {
    }

    public MapsController(IMapRepository mapRepository)
    {
      this.mapRepository = mapRepository;
    }

    public ActionResult MapPointsList()
    {
      var offices = mapRepository.GetAll(RenderingContext.Current.Rendering.Item);
      return View(offices);
    }  

    public ActionResult MapPoints()
    {
      return View();
    }

    [HttpPost]
    public JsonResult GetMapPoints(Guid itemId)
    {
      var item = Context.Database.GetItem(new ID(itemId));
      var offices = mapRepository.GetAll(item).Select(i=> new MapPoint(i));

      return Json(offices);
    }
  }
}