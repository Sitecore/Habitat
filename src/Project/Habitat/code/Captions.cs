namespace Sitecore.Habitat.Website
{
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public static class Captions
  {
    public static string OpenVisitDetailsPanel => DictionaryRepository.Get("/Website/Captions/OpenVisitDetailsPanel", "Open visit details panel");
    public static string CloseVisitDetailsPanel => DictionaryRepository.Get("/Website/Captions/CloseVisitDetailsPanel", "Close visit details panel");
    public static string RefreshVisitDetailsPanel => DictionaryRepository.Get("/Website/Captions/RefreshVisitDetailsPanel", "Refresh visit details panel");
    public static string EndVisit => DictionaryRepository.Get("/Website/Captions/EndVisit", "End the current visit and submits it to the Sitecore Experience Database");
  }
}