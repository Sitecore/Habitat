namespace Sitecore.Feature.Language.Tests
{
  using FluentAssertions;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Language.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class LanguageFactoryTests
  {
    [Theory]
    [AutoDbData]
    public void Create_ShouldCreateLanguageModel(Db db, [Content] DbItem item)
    {
      var contentItem = db.GetItem(item.ID);
      Context.Item = contentItem;
      var language = LanguageFactory.Create(Context.Language);
      language.Should().NotBeNull();
      language.TwoLetterCode.Should().BeEquivalentTo(Context.Language.Name);
    }
  }
}