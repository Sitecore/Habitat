namespace Sitecore.Foundation.FieldEditor.Tests.Services
{
    using System.Collections.Specialized;
    using FluentAssertions;
    using Sitecore.ExperienceEditor.Abstractions;
    using Sitecore.FakeDb;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.Foundation.FieldEditor.Services;
    using Sitecore.Foundation.Testing.Attributes;
    using Xunit;

    public class GetFieldsToEditServiceTests
  {
    [Theory]
    [AutoDbData]
    public void GetFieldsToEdit_ItemHasNoCustomFields_ReturnEmptyString(Db db, DbItem item, BaseItemContentService itemContentService)
    {
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      var service = new GetFieldsToEditService(itemContentService);

      service.GetFieldsToEdit(testItem).Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void GetFieldsToEdit_ItemHasFields_ReturnFields(Db db, DbItem item, DbField field1, DbField field2, BaseItemContentService itemContentService)
    {
      item.Add(field1);
      item.Add(field2);
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      var expectedResult = new [] {field1.Name + "|" + field2.Name, field2.Name + "|" + field1.Name};

      var service = new GetFieldsToEditService(itemContentService);
      service.GetFieldsToEdit(testItem).Should().BeOneOf(expectedResult);
    }

    [Theory]
    [AutoDbData]
    public void GetFieldEditorOptions_HasFields_DescriptorForEachField([Content]string[] fieldNames, NameValueCollection form, Db db, DbItem item, BaseItemContentService itemContentService)
    {
      //Arrange
      foreach (var field in fieldNames)
      {
        item.Add(new DbField(field));
      }
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      var pipedFieldNames = string.Join("|", fieldNames);
      var service = new GetFieldsToEditService(itemContentService);

      //Act
      var result = service.GetFieldEditorOptions(form, pipedFieldNames, testItem);
      
      //Assert      
      result.Fields.Count.Should().Be(fieldNames.Length);
    }
  }
}