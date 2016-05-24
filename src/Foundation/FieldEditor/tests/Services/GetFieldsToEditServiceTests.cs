namespace Sitecore.Foundation.FieldEditor.Tests.Services
{
  using System;
  using System.Collections.Specialized;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.FieldEditor.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class GetFieldsToEditServiceTests
  {
    [Theory]
    [AutoDbData]
    public void GetFieldsToEdit_ItemHasNoCustomFields_ReturnEmptyString(Db db, DbItem item)
    {
      db.Add(item);
      var testItem = db.GetItem(item.ID);

      GetFieldsToEditService.GetFieldsToEdit(testItem).Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void GetFieldsToEdit_ItemHasFields_ReturnFields(Db db, DbItem item, DbField field1, DbField field2)
    {
      item.Add(field1);
      item.Add(field2);
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      var expectedResult = field1.Name + "|" + field2.Name;

      GetFieldsToEditService.GetFieldsToEdit(testItem).Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [AutoDbData]
    public void GetFieldEditorOptions_HasFields_DescriptorForEachField([Content]string[] fieldNames, NameValueCollection form, Db db, DbItem item)
    {
      //Arrange
      foreach (var field in fieldNames)
      {
        item.Add(new DbField(field));
      }
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      var pipedFieldNames = string.Join("|", fieldNames);
      //Act
      var result = GetFieldsToEditService.GetFieldEditorOptions(form, pipedFieldNames, testItem);
      result.Fields.Count.Should().Be(fieldNames.Length);
      //Assert      
    }
  }
}