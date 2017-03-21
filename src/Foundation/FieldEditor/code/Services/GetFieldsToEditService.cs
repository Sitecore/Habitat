namespace Sitecore.Foundation.FieldEditor.Services
{
    using System.Collections.Specialized;
    using System.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Shell.Applications.WebEdit;

    public class GetFieldsToEditService
    {
        public static string GetFieldsToEdit(Item item)
        {
            var editableFields = item.Template.Fields.Where(IsEditableField).ToArray();
            if (!editableFields.Any())
            {
                return string.Empty;
            }
            var pipedFieldNames = string.Join("|", editableFields.Select(f => f.Name));
            return pipedFieldNames;
        }

        private static bool IsEditableField(TemplateFieldItem x)
        {
            return !x.Name.StartsWith("__");
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