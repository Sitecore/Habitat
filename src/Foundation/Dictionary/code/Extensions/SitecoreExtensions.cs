namespace Sitecore.Foundation.Dictionary.Extensions
{
  using System.Web;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Helpers;
  using Quantus;
  using Quantus.Sitecore.Services;
  using Sitecore.Data;

  public static class SitecoreExtensions
  {
    public static string Dictionary(this SitecoreHelper helper, string relativePath, string defaultValue = "")
    {
      return DictionaryPhraseRepository.Current.Get(relativePath, defaultValue);
    }

    public static HtmlString DictionaryField(this SitecoreHelper helper, string relativePath, string defaultValue = "")
    {
      var item = DictionaryPhraseRepository.Current.GetItem(relativePath, defaultValue);
      if (item == null)
        return new HtmlString(defaultValue);
      return helper.Field(Templates.DictionaryEntry.Fields.Phrase, item);
    }

    public static string DictionaryPlural(this SitecoreHelper helper, string relativePath, decimal quantity, string defaultValue = "")
    {
      var category = PluralService.GetPluralCategory(Context.Language.Name, quantity);
      var fieldId = GetPluralFieldId(category);

      return DictionaryPhraseRepository.Current.GetPlural(relativePath, fieldId, defaultValue);
    }

    public static HtmlString DictionaryPluralField(this SitecoreHelper helper, string relativePath, decimal quantity, string defaultValue = "")
    {
      var item = DictionaryPhraseRepository.Current.GetPluralItem(relativePath, defaultValue);
      if (item == null)
        return new HtmlString(defaultValue);

      var type = PluralService.GetPluralCategory(Context.Language.Name, quantity);
      var fieldId = GetPluralFieldId(type);

      return helper.Field(fieldId, item);
    }

    private static ID GetPluralFieldId(PluralCategory category)
    {
      switch (category)
      {
        case PluralCategory.Zero:
            return Templates.DictionaryPluralEntry.Fields.PhraseZero;
        case PluralCategory.One:
            return Templates.DictionaryPluralEntry.Fields.PhraseOne;
        case PluralCategory.Two:
            return Templates.DictionaryPluralEntry.Fields.PhraseTwo;
        case PluralCategory.Few:
            return Templates.DictionaryPluralEntry.Fields.PhraseFew;
        case PluralCategory.Many:
            return Templates.DictionaryPluralEntry.Fields.PhraseMany;
        case PluralCategory.Other:
        default:
            return Templates.DictionaryPluralEntry.Fields.PhraseOther;
      }
    }
  }
}