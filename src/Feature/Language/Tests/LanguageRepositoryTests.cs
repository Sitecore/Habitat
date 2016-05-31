namespace Sitecore.Feature.Language.Tests
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Language.Infrastructure.Repositories;
  using Sitecore.Feature.Language.Models;
  using Sitecore.Feature.Language.Tests.Extensions;
  using Sitecore.Foundation.Multisite;
  using Xunit;

  public class LanguageRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetAll_ShouldReturnAllLanguages(Db db, [Content] DbItem item)
    {
      var contextItem = db.GetItem(item.ID);
      Context.Item = contextItem;
      var languages = LanguageRepository.GetAll();
      languages.Should().BeAssignableTo<IEnumerable<Language>>();
      languages.Count().Should().Be(db.Database.GetLanguages().Count);
    }

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
    [AutoLanguageDbData]
    public void GetSupportedLanguages_OneSelected_ShouldReturnSelected(Db db, [Content] DbTemplate template, DbItem item, string rootName)
    {
      template.BaseIDs = new[]
      {
        Templates.Site.ID, Feature.Language.Templates.LanguageSettings.ID
      };

      var languageItem = new DbItem("en");
      db.Add(languageItem);

      var siteRootItem = new DbItem(rootName, ID.NewID, template.ID)
      {
        new DbField(Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages)
        {
          {
            "en", languageItem.ID.ToString()
          }
        }
      };

      siteRootItem.Add(item);
      db.Add(siteRootItem);
      var contextItem = db.GetItem(item.ID);
      Context.Item = contextItem;
      var supportedLanguages = LanguageRepository.GetSupportedLanguages();
      supportedLanguages.Count().Should().BeGreaterThan(0);
    }

    [Theory]
    [AutoLanguageDbData]
    public void GetSupportedLanguages_NoneSelected_ShouldReturnEmptyList(Db db, [Content] DbTemplate template, DbItem item, string rootName)
    {
      template.BaseIDs = new[]
      {
        Templates.Site.ID, Feature.Language.Templates.LanguageSettings.ID
      };

      var languageItem = new DbItem("en");
      db.Add(languageItem);

      var siteRootItem = new DbItem(rootName, ID.NewID, template.ID)
      {
        new DbField(Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages)
        {
          {
            "en", ""
          }
        }
      };

      siteRootItem.Add(item);
      db.Add(siteRootItem);
      var contextItem = db.GetItem(item.ID);
      Context.Item = contextItem;
      var supportedLanguages = LanguageRepository.GetSupportedLanguages();
      supportedLanguages.Count().Should().Be(0);
    }
  }
}