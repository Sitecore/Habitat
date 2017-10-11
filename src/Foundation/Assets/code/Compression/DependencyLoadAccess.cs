namespace Sitecore.Foundation.Assets.Compression
{
    using System.Web;
    using System.Web.Mvc;

    public static class DependencyLoadAccess
    {
        #region
        public static SitecoreDependencyLoader GetLoader(this ViewContext vc)
        {
            bool isNew;
            return SitecoreDependencyLoader.TryCreate(vc.HttpContext, out isNew);
        }

        public static SitecoreDependencyLoader GetLoader(this ControllerContext cc)
        {
            bool isNew;
            return SitecoreDependencyLoader.TryCreate(cc.HttpContext, out isNew);
        }

        public static SitecoreDependencyLoader GetLoader(this HttpContextBase http)
        {
            bool isNew;
            return SitecoreDependencyLoader.TryCreate(http, out isNew);
        }
        #endregion
    }
}