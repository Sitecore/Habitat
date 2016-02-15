namespace Sitecore.Foundation.SitecoreExtensions.Repositories
{
  using System;
  using System.Linq;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Mvc.Presentation;

  public class RenderingPropertiesRepository : IRenderingPropertiesRepository
  {
    public T Get<T>()
    {
      var obj = Sitecore.Reflection.ReflectionUtil.CreateObject(typeof(T));
      var currentContext = RenderingContext.Current.Rendering;
      var parameters = currentContext?.Properties["Parameters"];
      if (parameters != null)
      {
        parameters = this.FilterEmptyParametrs(parameters);
        try
        {
          Sitecore.Reflection.ReflectionUtil.SetProperties(obj, parameters);
        }
        catch (Exception e)
        {
          Sitecore.Diagnostics.Log.Error(e.Message, this); 
        }
      }

      return (T)obj;
    }
    protected virtual string FilterEmptyParametrs(string parameters)
    {
      var parametersList = parameters.Split(new[]
      {
        '&'
      }, StringSplitOptions.RemoveEmptyEntries);

      return string.Join("&", parametersList.Where(x => x.Contains("=")));
    }
  }
}