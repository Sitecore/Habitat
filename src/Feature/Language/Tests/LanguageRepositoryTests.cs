namespace Sitecore.Feature.Language.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Language.Infrastructure.Repositories;
  using Sitecore.Feature.Language.Models;
  using Sitecore.Feature.Language.Tests.Extensions;
  using Xunit;

  public class LanguageRepositoryTests
  {
    [Theory]
    [AutoDbData]

    public void GetAll_ShouldReturnAllLanguages(Db db, [Content]DbItem item)
    {
      var contextItem = db.GetItem(item.ID);
      Sitecore.Context.Item = contextItem;
      var languages = LanguageRepository.GetAll();
      languages.Should().BeAssignableTo<IEnumerable<Language>>();
      languages.Count().Should().Be(db.Database.GetLanguages().Count);
    }

    [Theory]
    [AutoDbData]
    public void GetActive_ShouldReturnLanguageModelForContextLanguage(Db db, [Content]DbItem item)
    {
      var contextItem = db.GetItem(item.ID);
      Sitecore.Context.Item = contextItem;
      var activeLanguage = LanguageRepository.GetActive();
      activeLanguage.TwoLetterCode.Should().BeEquivalentTo(Context.Language.Name);
    }

    [Theory]
    [AutoDbData]
    public void GetSupportedLanguages_ShouldReturlListOfSupportedLanguages(Db db, DbItem item , string rootName)
    {
      var contextItemId = ID.NewID;
      var rootId = ID.NewID;
      var template = new DbTemplate();
      template.BaseIDs = new[]
      {
        Foundation.Multisite.Templates.Site.ID,
          Templates.LanguageSettings.ID
      };

      var languageItem = new DbItem("en");
      db.Add(languageItem);
      db.Add(new DbTemplate(Foundation.Multisite.Templates.Site.ID));
      db.Add(new DbTemplate(Templates.LanguageSettings.ID) {Fields = { { Templates.LanguageSettings.Fields.SupportedLanguages, languageItem.ID.ToString()} }});
      db.Add(template);

      var rootItem = new DbItem(rootName, rootId, template.ID){ new DbField(Templates.LanguageSettings.Fields.SupportedLanguages) { {"en", languageItem.ID.ToString()} } };

      rootItem.Add(item);
      db.Add(rootItem);
      var contextItem = db.GetItem(item.ID);
      Sitecore.Context.Item = contextItem;
      var supportedLanguages = LanguageRepository.GetSupportedLanguages();
      supportedLanguages.Count().Should().BeGreaterThan(0);
    }
  }
}
