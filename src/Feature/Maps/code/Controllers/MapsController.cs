namespace Sitecore.Feature.Maps.Controllers
{
  using System;
  using System.Linq;
  using System.Web.Mvc;
  using Data;
  using Models;
  using Repositories;
  using Mvc.Controllers;

  public class MapsController : SitecoreController
  {
    private readonly IMapPointRepository mapPointRepository;

    public MapsController() : this(new MapPointRepository())
    {
    }

    public MapsController(IMapPointRepository mapPointRepository)
    {
      this.mapPointRepository = mapPointRepository;
    }     

    [HttpPost]
    public JsonResult GetMapPoints(Guid itemId)
    {
      var item = Context.Database.GetItem(new ID(itemId));
      var points = mapPointRepository.GetAll(item);

      return Json(points);
    }
  }
}