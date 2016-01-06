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
        Foundation.MultiSite.Templates.Site.ID,
          Templates.LanguageSettings.ID
      };

      db.Add(new DbTemplate(Foundation.MultiSite.Templates.Site.ID));
      db.Add(new DbTemplate(Templates.LanguageSettings.ID));
      db.Add(template);
      db.Add(new DbItem(rootName, rootId, template.ID) {item});

      var contextItem = db.GetItem(item.ID);
      Sitecore.Context.Item = contextItem;
      var supportedLanguages = LanguageRepository.GetSupportedLanguages();
    }
  }
}
