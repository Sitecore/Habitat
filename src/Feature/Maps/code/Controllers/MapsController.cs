﻿namespace Sitecore.Feature.Maps.Controllers
{
  using System;
  using System.Linq;
  using System.Web.Mvc;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Feature.Maps.Models;
  using Sitecore.Feature.Maps.Repositories;
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

    public ActionResult MapPoint()
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