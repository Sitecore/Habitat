namespace Sitecore.Foundation.SitecoreExtensions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Mvc.Extensions;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Pipelines;
    using Sitecore.Pipelines.RenderField;
    using Sitecore.Web.UI.WebControls;

    public class FieldRendererService
    {
        [ThreadStatic]
        private static Stack<string> _endFieldStack;

        private static Stack<string> EndFieldStack => _endFieldStack ?? (_endFieldStack = new Stack<string>());

        private static Item CurrentItem
        {
            get
            {
                var currentRendering = CurrentRendering;
                return currentRendering == null ? PageContext.Current.Item : currentRendering.Item;
            }
        }

        private static Mvc.Presentation.Rendering CurrentRendering
        {
            get
            {
                return RenderingContext.CurrentOrNull.ValueOrDefault(context => context.Rendering);
            }
        }

        public static string RenderField(Item item, string fieldName)
        {
            return FieldRenderer.Render(item, fieldName);
        }

        public static string RenderField(Item item, ID fieldId)
        {
            var field = item.Fields[fieldId];
            Assert.IsNotNull(field, "Field with id: " + fieldId + " is null on item " + item.Name);
            return FieldRenderer.Render(item, field.Name);
        }

        public static HtmlString BeginField(ID fieldId, Item item, object parameters)
        {
            Assert.ArgumentNotNull(fieldId, nameof(fieldId));
            var renderFieldArgs = new RenderFieldArgs
                                  {
                                      Item = item,
                                      FieldName = item.Fields[fieldId].Name
                                  };
            if (parameters != null)
            {
                CopyProperties(parameters, renderFieldArgs);
                CopyProperties(parameters, renderFieldArgs.Parameters);
            }
            renderFieldArgs.Item = renderFieldArgs.Item ?? CurrentItem;

            if (renderFieldArgs.Item == null)
            {
                EndFieldStack.Push(string.Empty);
                return new HtmlString(string.Empty);
            }
            CorePipeline.Run("renderField", renderFieldArgs);
            var result1 = renderFieldArgs.Result;
            var str = result1.ValueOrDefault(result => result.FirstPart).OrEmpty();
            EndFieldStack.Push(result1.ValueOrDefault(result => result.LastPart).OrEmpty());
            return new HtmlString(str);
        }

        private static void CopyProperties(object source, object target)
        {
            var type = target.GetType();
            foreach (var info in source.GetType().GetProperties())
            {
                var property = type.GetProperty(info.Name);
                if ((property != null) && info.PropertyType.IsAssignableTo(property.PropertyType))
                {
                    property.SetValue(target, info.GetValue(source, null), null);
                }
            }
        }

        public static HtmlString EndField()
        {
            var endFieldStack = EndFieldStack;
            if (endFieldStack.Count == 0)
            {
                throw new InvalidOperationException("There was a call to EndField with no corresponding call to BeginField");
            }
            return new HtmlString(endFieldStack.Pop());
        }
    }
}