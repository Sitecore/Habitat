using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Habitat.Framework.SitecoreExtensions.HtmlHelpers
{
  public static class ValidationHelpers
  {
    public static MvcHtmlString ValidationErrorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string error)
    {
      if (HasError(htmlHelper, ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression)))
        return new MvcHtmlString(error);
      else
        return null;
    }

    private static bool HasError(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression)
    {
      string modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
      FormContext formContext = htmlHelper.ViewContext.FormContext;
      if (formContext == null)
        return false;

      if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName))
        return false;

      ModelState modelState = htmlHelper.ViewData.ModelState[modelName];
      if (modelState == null)
        return false;

      ModelErrorCollection modelErrors = modelState.Errors;
      if (modelErrors == null)
        return false;

      return (modelErrors.Count > 0);
    }
  }
}