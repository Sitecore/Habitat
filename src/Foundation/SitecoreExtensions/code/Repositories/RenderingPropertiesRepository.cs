namespace Sitecore.Foundation.SitecoreExtensions.Repositories
{
  using System;
  using Sitecore.Analytics.Data.Items;
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
        if (parameters != null)
        {
          try
          {
            Sitecore.Reflection.ReflectionUtil.SetProperties(obj, parameters);
          }
          catch (Exception e)
          {
              Sitecore.Diagnostics.Log.Error(e.Message, this); 
          }
        }
      }

      return (T)obj;
    }
  }
}