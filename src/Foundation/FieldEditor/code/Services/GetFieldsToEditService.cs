using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.FieldEditor.Services
{
  using System.Collections.Specialized;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Shell.Applications.WebEdit;

  public class GetFieldsToEditService
  {
    public static string GetFieldsToEdit(Item item)
    {
      var customFields = item.Template.Fields.Where(x => !x.Name.StartsWith("__"));
      if (!customFields.Any())
      {
          return String.Empty;
      }
      var pipedFieldNames = String.Join("|", customFields.Select(f => f.Name));
      return pipedFieldNames;
    }

    public static PageEditFieldEditorOptions GetFieldEditorOptions(NameValueCollection form, string pipedFields, Item item)
    {
      var fields = pipedFields.Split('|').Where(fieldName => item.Fields[fieldName] != null).Select(fieldName => new FieldDescriptor(item, fieldName)).ToList();
      var options = new PageEditFieldEditorOptions(form, fields)
                    {
                      PreserveSections = true,
                      ShowSections = true
                    };
      return options;
    }
  }
}