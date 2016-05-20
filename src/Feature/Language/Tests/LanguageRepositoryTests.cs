namespace Sitecore.Feature.Language.Tests
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Language.Models;
  using Sitecore.Feature.Language.Repositories;
  using Sitecore.Foundation.Multisite;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class LanguageRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetActive_ShouldReturnLanguageModelForContextLanguage(Db db, [Content] DbItem item)
    {
      var contextItem = db.GetItem(item.ID);
      Context.Item = contextItem;
      var activeLanguage = LanguageRepository.GetActive();
      activeLanguage.TwoLetterCode.Should().BeEquivalentTo(Context.Language.Name);
    }

    [Theory]
    [AutoDbData]
    public void GetSupportedLanguages_ShouldReturlListOfSupportedLanguages(Db db, DbItem item, string rootName)
    {
      var contextItemId = ID.NewID;
      var rootId = ID.NewID;
      var template = new DbTemplate();
      template.BaseIDs = new[] {Templates.Site.ID, Feature.Language.Templates.LanguageSettings.ID};

      var languageItem = new DbItem("en");
      db.Add(languageItem);
      db.Add(new DbTemplate(Templates.Site.ID));
      db.Add(new DbTemplate(Feature.Language.Templates.LanguageSettings.ID)
             {
               Fields = {
                          {Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages, languageItem.ID.ToString()}
                        }
             });
      db.Add(template);

      var rootItem = new DbItem(rootName, rootId, template.ID)
                     {
                       new DbField(Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages)
                       {
                         {"en", languageItem.ID.ToString()}
                       },
                       item
                     };

      db.Add(rootItem);
      var contextItem = db.GetItem(item.ID);
      Context.Item = contextItem;
      var supportedLanguages = LanguageRepository.GetSupportedLanguages();
      supportedLanguages.Count().Should().BeGreaterThan(0);
    }
  }
}