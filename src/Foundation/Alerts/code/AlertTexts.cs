using Sitecore.Data;
using Sitecore.Foundation.SitecoreExtensions.Repositories;

namespace Sitecore.Foundation.Alerts
{
  public class AlertTexts
  {
    public static string InvalidDataSourceTemplate(ID templateId) => string.Format(DictionaryRepository.Get("/Alerts/Invalid data source", "Data source isn't set or have wrong template. Template {0} is required"), templateId);

    public static string InvalidDataSourceTemplateFriendlyMessage => DictionaryRepository.Get("/Alerts/Friendly/Invalid data source template", "There was a problem with the associated content item, please associate a correct content item with the component");

    public static string InvalidDataSource => DictionaryRepository.Get("/Alerts/Invalid data source", "Data source isn't set or have wrong template");


  }
}