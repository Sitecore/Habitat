namespace Sitecore.Foundation.SitecoreExtensions.FieldEditor
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Applications.WebEdit;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;

    /// <summary>
    /// The field editor all.
    /// </summary>
    public class FieldEditorAll : Shell.Applications.WebEdit.Commands.FieldEditorCommand
    {
        /// <summary>
        /// The name of the parameter in <c>ClientPipelineArgs</c> containing 
        /// Sitecore item identification information.
        /// </summary>
        private const string URI = "uri";

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length >= 1)
            {
                ClientPipelineArgs args = new ClientPipelineArgs(context.Parameters);
                args.Parameters.Add(URI, context.Items[0].Uri.ToString());

                var allFields = string.Empty;

                // Loop through all fields on the template
                // Filter the standard Sitecore fields available on every template, starting with "__"
                foreach (var field in context.Items[0].Template.Fields.Where(x => !x.Name.StartsWith("__")))
                {
                    // name|name2|...
                    allFields = string.Concat(allFields, field.Name, "|");
                }

                // Remove the last '|' of the allFields string
                if (allFields.EndsWith("|"))
                {
                    allFields = allFields.TrimEnd(allFields[allFields.Length - 1]);
                }

                args.Parameters["fields"] = allFields;
                Context.ClientPage.Start(this, "StartFieldEditor", args);
            }
        }

        /// <summary>
        /// Gets the configured Field Editor options. Options determine both the look of Field Editor and the actual fields available for editing.
        /// </summary>
        /// <param name="args">
        /// The pipeline arguments. Current item URI is accessible as 'uri' parameter
        /// </param>
        /// <param name="form">
        /// The form values.
        /// </param>
        /// <returns>
        /// The options.
        /// </returns>
        protected override PageEditFieldEditorOptions GetOptions(ClientPipelineArgs args, NameValueCollection form)
        {
            Assert.IsNotNull(args, "args");
            Assert.IsNotNull(form, "form");
            Assert.IsNotNullOrEmpty(args.Parameters[URI], URI);
            ItemUri uri = ItemUri.Parse(args.Parameters[URI]);
            Assert.IsNotNull(uri, URI);

            Assert.IsNotNullOrEmpty(args.Parameters["fields"], "Field Editor command expects 'fields' parameter");
            string fieldsParameter = args.Parameters["fields"];

            Item item = Database.GetItem(uri);
            Assert.IsNotNull(item, "item");

            List<FieldDescriptor> fields = new List<FieldDescriptor>();
            foreach (string fieldName in fieldsParameter.Split('|'))
            {
                if (item.Fields[fieldName] != null)
                {
                    fields.Add(new FieldDescriptor(item, fieldName));
                }
            }

            // Field editor options.
            PageEditFieldEditorOptions options = new PageEditFieldEditorOptions(form, fields)
            {
                PreserveSections = true, 
                ShowSections = true, 
            };

            return options;
        }

        /// <summary>
        /// Determine if the command button should be displayed or hidden.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="CommandState"/>.
        /// </returns>
        public override CommandState QueryState(CommandContext context)
        {
            return Context.PageMode.IsExperienceEditor ? CommandState.Enabled : CommandState.Hidden;
        }
    }
}