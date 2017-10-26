namespace Sitecore.Foundation.FieldEditor.Infrastructure
{
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.FieldEditor.Services;
    using Sitecore.Shell.Applications.WebEdit;
    using Sitecore.Shell.Applications.WebEdit.Commands;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;

    public class EditAllFieldsCommand : FieldEditorCommand
    {
        private const string Parameter_Uri = "uri";
        private const string Parameter_Fields = "fields";

        [ExcludeFromCodeCoverage]
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));
            if (context.Items.Length < 1)
            {
                return;
            }
            var args = new ClientPipelineArgs(context.Parameters);
            var item = context.Items[0];
            args.Parameters.Add(Parameter_Uri, item.Uri.ToString());

            var pipedFieldNames = GetFieldsToEditService.GetFieldsToEdit(item);

            args.Parameters[Parameter_Fields] = pipedFieldNames;
            Context.ClientPage.Start(this, "StartFieldEditor", args);
        }

        [ExcludeFromCodeCoverage]
        protected override PageEditFieldEditorOptions GetOptions(ClientPipelineArgs args, NameValueCollection form)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.ArgumentNotNull(form, nameof(form));
            Assert.IsNotNullOrEmpty(args.Parameters[Parameter_Uri], $"Field Editor command expects '{Parameter_Uri}' parameter");
            Assert.IsNotNullOrEmpty(args.Parameters[Parameter_Fields], $"Field Editor command expects '{Parameter_Fields}' parameter");

            var uri = ItemUri.Parse(args.Parameters[Parameter_Uri]);
            Assert.IsNotNull(uri, Parameter_Uri + " parameter must be a valid Uri");

            var pipedFields = args.Parameters[Parameter_Fields];

            var item = Database.GetItem(uri);
            Assert.IsNotNull(item, "item");

            var options = GetFieldsToEditService.GetFieldEditorOptions(form, pipedFields, item);

            return options;
        }

        public override CommandState QueryState(CommandContext context)
        {
            if (!Context.PageMode.IsExperienceEditor)
                return CommandState.Hidden;
            return context.Items.Length >= 1 && !string.IsNullOrEmpty(GetFieldsToEditService.GetFieldsToEdit(context.Items[0])) ? CommandState.Enabled : CommandState.Hidden;
        }
    }
}