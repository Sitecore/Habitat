using System;
using System.Collections.Generic;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using Sitecore.Web.UI.WebControls;
using Rendering = Sitecore.Mvc.Presentation.Rendering;

namespace Habitat.Framework.SitecoreExtensions.Extensions
{
    public static class MvcItemFieldRepository
    {
        private static Stack<string> _endFieldStack;

        private static Stack<string> EndFieldStack
        {
            get { return _endFieldStack ?? (_endFieldStack = new Stack<string>()); }
        }

        private static Item CurrentItem
        {
            get
            {
                var currentRendering = CurrentRendering;
                return currentRendering == null ? PageContext.Current.Item : currentRendering.Item;
            }
        }

        private static Rendering CurrentRendering
        {
            get { return RenderingContext.CurrentOrNull.ValueOrDefault(context => context.Rendering); }
        }

        private static string RenderField(Item item, string fieldName)
        {
            return FieldRenderer.Render(item, fieldName);
        }

        private static string RenderField(Item item, ID fieldId)
        {
            var field = item.Fields[fieldId];
            Assert.IsNotNull(field, "Field with id: " + fieldId + " is null on item " + item.Name);
            return FieldRenderer.Render(item, field.Name);
        }

        public static HtmlString MvcField(this Item item, ID fieldId)
        {
            Assert.IsNotNull(item, "Item cannot be null");
            Assert.IsNotNull(fieldId, "FieldId cannot be null");
            return new HtmlString(RenderField(item, fieldId));
        }

        public static HtmlString MvcField(this Item item, ID fieldId, object parameters)
        {
            return new HtmlString(BeginField(fieldId, item, parameters) + EndField().ToString());
        }

        private static HtmlString BeginField(ID fieldId, Item item, object parameters)
        {
            Assert.ArgumentNotNull(fieldId, "fieldName");
            var renderFieldArgs = new RenderFieldArgs
            {
                Item = item,
                FieldName = item.Fields[fieldId].Name
            };
            if (parameters != null)
            {
                TypeHelper.CopyProperties(parameters, renderFieldArgs);
                TypeHelper.CopyProperties(parameters, renderFieldArgs.Parameters);
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

        private static HtmlString EndField()
        {
            var endFieldStack = EndFieldStack;
            if (endFieldStack.Count == 0)
                throw new InvalidOperationException(
                    "There was a call to EndField with no corresponding call to BeginField");
            return new HtmlString(endFieldStack.Pop());
        }
    }
}