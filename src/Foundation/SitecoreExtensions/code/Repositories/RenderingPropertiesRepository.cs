namespace Sitecore.Foundation.SitecoreExtensions.Repositories
{
    using System;
    using System.Linq;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Reflection;

    [Service(typeof(IRenderingPropertiesRepository))]
    public class RenderingPropertiesRepository : IRenderingPropertiesRepository
    {
        public T Get<T>(Rendering rendering)
        {
            var obj = ReflectionUtil.CreateObject(typeof(T));
            var currentContext = rendering;
            var parameters = currentContext?.Properties["Parameters"];
            if (parameters == null)
                return (T)obj;

            parameters = this.FilterEmptyParametrs(parameters);
            try
            {
                ReflectionUtil.SetProperties(obj, parameters);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e, this);
            }

            return (T)obj;
        }

        protected virtual string FilterEmptyParametrs(string parameters)
        {
            var parametersList = parameters.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            return string.Join("&", parametersList.Where(x => x.Contains("=")));
        }
    }
}