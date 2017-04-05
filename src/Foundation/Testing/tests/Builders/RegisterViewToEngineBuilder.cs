namespace Sitecore.Foundation.Testing.Builders
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using System.Web.Mvc;
  using Ploeh.AutoFixture.Kernel;
  using Sitecore.Foundation.Testing.Attributes;

  public class RegisterViewToEngineBuilder : AttributeRelay<RegisterViewAttribute>
  {
    protected override object Resolve(ISpecimenContext context, RegisterViewAttribute attribute, ParameterInfo parameterInfo)
    {
      var specimen = context.Resolve(parameterInfo.ParameterType);

      if (specimen is IView)
      {
        this.SetView(attribute.Name, specimen as IView);
      }

      return specimen;
    }

    private void SetView(string viewName, IView view)
    {
      var localViewEngine = ViewEngines.Engines.OfType<ViewEngineMock>().SingleOrDefault() ?? new ViewEngineMock();
      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(localViewEngine);

      localViewEngine.Views[viewName] = view;
    }

    public class ViewEngineMock : IViewEngine
    {
      public Dictionary<string, IView> Views { get; } = new Dictionary<string, IView>();

      public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
      {
        if (!this.Views.ContainsKey(partialViewName))
        {
          throw new InvalidOperationException($"Can't fined registered view with name {partialViewName}");
        }

        return new ViewEngineResult(this.Views[partialViewName], this);
      }

      public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
      {
        if (!this.Views.ContainsKey(viewName))
        {
          throw new InvalidOperationException($"Can't fined registered view with name {viewName}");
        }

        return new ViewEngineResult(this.Views[viewName], this);
      }

      public void ReleaseView(ControllerContext controllerContext, IView view)
      {
      }
    }
  }
}