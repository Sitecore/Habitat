namespace Sitecore.Feature.Maps.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data;
    using Repositories;

    public class MapsController : Mvc.Controllers.SitecoreController
    {
        private readonly IMapPointRepository mapPointRepository;

        public MapsController(IMapPointRepository mapPointRepository)
        {
            this.mapPointRepository = mapPointRepository;
        }

        [HttpPost]
        [Foundation.SitecoreExtensions.Attributes.SkipAnalyticsTracking]
        public JsonResult GetMapPoints(Guid itemId)
        {
            var item = Context.Database.GetItem(new ID(itemId));
            var points = this.mapPointRepository.GetAll(item);

            return this.Json(points);
        }
    }
}