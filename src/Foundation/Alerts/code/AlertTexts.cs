using Sitecore.Data;
using Sitecore.Foundation.Dictionary.Repositories;

namespace Sitecore.Foundation.Alerts
{
  public class AlertTexts
  {
    public static string InvalidDataSourceTemplate(ID templateId)
      => string.Format(DictionaryPhraseRepository.Current.Get("/Alerts/Invalid data source", "Data source isn't set or have wrong template. Template {0} is required"), templateId);

    public static string InvalidDataSourceTemplateFriendlyMessage => DictionaryPhraseRepository.Current.Get("/Alerts/Friendly/Invalid data source template", "There was a problem with the associated content item, please associate a correct content item with the component");

    public static string InvalidDataSource => DictionaryPhraseRepository.Current.Get("/Alerts/Invalid data source", "Data source isn't set or have wrong template");


  }
}