

namespace Sitecore.Feature.Language.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Language.Infrastructure.Factories;
  using Sitecore.Feature.Language.Tests.Extensions;
  using Xunit;

  public class LanguageFactoryTests
  {
    [Theory]
    [AutoDbData]
    public void Create_ChouldCreateLanguageModel(Db db, [Content]DbItem item)
    {
      var contentItem = db.GetItem(item.ID);
      Sitecore.Context.Item = contentItem;
      var language = LanguageFactory.Create(Context.Language);
      language.Should().NotBeNull();
      language.TwoLetterCode.Should().BeEquivalentTo(Context.Language.Name);
    }
  }
}
