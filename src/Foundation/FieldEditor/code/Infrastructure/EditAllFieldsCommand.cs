namespace Sitecore.Foundation.FieldEditor.Infrastructure
{
  using System.Collections.Specialized;
  using System.Linq;
  using Sitecore.Data;
  using Sitecore.Diagnostics;
  using Sitecore.Shell.Applications.WebEdit;
  using Sitecore.Shell.Applications.WebEdit.Commands;
  using Sitecore.Shell.Framework.Commands;
  using Sitecore.Web.UI.Sheer;

  public class EditAllFieldsCommand : FieldEditorCommand
  {
    private const string Parameter_Uri = "uri";
    private const string Parameter_Fields = "fields";

    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull(context, nameof(context));
      if (context.Items.Length < 1)
        return;
      var args = new ClientPipelineArgs(context.Parameters);
      var item = context.Items[0];
      args.Parameters.Add(Parameter_Uri, item.Uri.ToString());

      var customFields = item.Template.Fields.Where(x => !x.Name.StartsWith("__"));

      var pipedFieldNames = string.Concat(customFields.Select(f => f.Name), "|").Trim('|');

      args.Parameters[Parameter_Fields] = pipedFieldNames;
      Context.ClientPage.Start(this, "StartFieldEditor", args);
    }

    protected override PageEditFieldEditorOptions GetOptions(ClientPipelineArgs args, NameValueCollection form)
    {
      Assert.ArgumentNotNull(args, nameof(args));
      Assert.ArgumentNotNull(form, nameof(form));
      Assert.IsNotNullOrEmpty(args.Parameters[Parameter_Uri], $"Field Editor command expects '{Parameter_Uri}' parameter");
      Assert.IsNotNullOrEmpty(args.Parameters[Parameter_Fields], $"Field Editor command expects '{Parameter_Fields}' parameter");

      var uri = ItemUri.Parse(args.Parameters[Parameter_Uri]);
      Assert.IsNotNull(uri, Parameter_Uri + " parameter must be a valid Uri");

      var fieldsParameter = args.Parameters[Parameter_Fields];

      var item = Database.GetItem(uri);
      Assert.IsNotNull(item, "item");

      var fields = fieldsParameter.Split('|').Where(fieldName => item.Fields[fieldName] != null).Select(fieldName => new FieldDescriptor(item, fieldName)).ToList();

      var options = new PageEditFieldEditorOptions(form, fields)
                    {
                      PreserveSections = true,
                      ShowSections = true
                    };

      return options;
    }

    public override CommandState QueryState(CommandContext context)
    {
      return Context.PageMode.IsExperienceEditor ? CommandState.Enabled : CommandState.Hidden;
    }
  }
}