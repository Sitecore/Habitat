namespace Sitecore.Foundation.Dictionary.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.Foundation.Dictionary.Models;
    using Sitecore.Foundation.Dictionary.Services;
    using Sitecore.Foundation.Testing.Attributes;
    using Xunit;

    public class CreateDictionaryEntryServiceTests
    {
        [Theory]
        [AutoDbData]
        public void CreateDictionaryEntry_Call_CreateItems(Db db, [Content] DictionaryEntryTemplate entryTemplate, [Content] DictionaryPluralEntryTemplate pluralEntryTemplate, [Content] DbItem rootItem, IEnumerable<string> pathParts, IEnumerable<string> pluralPathParts, string defaultValue)
        {
            //Arrange
            var dictionary = new Dictionary
            {
                Root = db.GetItem(rootItem.ID)
            };
            db.Add(new DbTemplate(Templates.DictionaryFolder.ID));
            var path = string.Join("/", pathParts.Select(ItemUtil.ProposeValidItemName));
            var pluralPath = string.Join("/", pluralPathParts.Select(ItemUtil.ProposeValidItemName));

            //Act
            var phraseItem = CreateDictionaryEntryService.CreateDictionaryEntry(dictionary, path, false, defaultValue);
            var pluralPhraseItem = CreateDictionaryEntryService.CreateDictionaryEntry(dictionary, pluralPath, true, defaultValue);

            //Assert
            phraseItem.Should().NotBeNull();
            phraseItem.TemplateID.Should().Be(Templates.DictionaryEntry.ID);
            phraseItem.Paths.FullPath.Should().Be($"{rootItem.FullPath}/{path}");
            phraseItem[Templates.DictionaryEntry.Fields.Phrase].Should().Be(defaultValue);

            pluralPhraseItem.Should().NotBeNull();
            pluralPhraseItem.TemplateID.Should().Be(Templates.DictionaryPluralEntry.ID);
            pluralPhraseItem.Paths.FullPath.Should().Be($"{rootItem.FullPath}/{pluralPath}");
            pluralPhraseItem[Templates.DictionaryPluralEntry.Fields.PhraseOther].Should().Be(defaultValue);
        }

        public class DictionaryEntryTemplate : DbTemplate
        {
            public DictionaryEntryTemplate() : base(Templates.DictionaryEntry.ID)
            {
                this.Add(Templates.DictionaryEntry.Fields.Phrase);
            }
        }

        public class DictionaryPluralEntryTemplate : DbTemplate
        {
            public DictionaryPluralEntryTemplate() : base(Templates.DictionaryPluralEntry.ID)
            {
                this.Add(Templates.DictionaryPluralEntry.Fields.PhraseOther);
            }
        }
    }
}