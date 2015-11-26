using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Framework.SitecoreExtensions.Repositories
{
  using Sitecore.Mvc.Presentation;

  public class RenderingPropertiesRepository : IRenderingPropertiesRepository
  {
    public T Get<T>()
    {
      var obj = Sitecore.Reflection.ReflectionUtil.CreateObject(typeof(T));
      var currentContext = RenderingContext.Current.Rendering;
      if (currentContext != null)
      {
        var parameters = currentContext.Properties["Parameters"];
        Sitecore.Reflection.ReflectionUtil.SetProperties(obj, parameters);
      }

      return (T)obj;
    }
  }
}