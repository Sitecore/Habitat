using Sitecore.Data;
using Sitecore.Foundation.SitecoreExtensions.Repositories;

namespace Sitecore.Foundation.Alerts
{
  public class AlertTexts
  {
    public static string InvalidDataSourceTemplate(ID templateId)
      => string.Format(DictionaryRepository.Get("/Alerts/Invalid data source", "Data source isn't set or have wrong template. Template {0} is required"), templateId);

    public static string InvalidDataSource => DictionaryRepository.Get("/Alerts/Invalid data source", "Data source isn't set or have wrong template");
  }
}