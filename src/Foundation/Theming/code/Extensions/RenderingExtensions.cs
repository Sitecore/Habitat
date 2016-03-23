namespace Sitecore.Foundation.Theming.Extensions
{
  using System;
  using Sitecore.Data;
  using Sitecore.Mvc.Presentation;

  public static class RenderingExtensions
  {
    public static string GetBackgroundClass([NotNull] this Rendering rendering)
    {
      var id = MainUtil.GetID(rendering.Parameters[Constants.BackgroundLayoutParameters.Background] ?? "", null);
      if (ID.IsNullOrEmpty(id))
        return "";
      var item = rendering.RenderingItem.Database.GetItem(id);
      return item?[Templates.Style.Fields.Class] ?? "";
    }

    public static string GetContainerClass([NotNull] this Rendering rendering)
    {
      return rendering.IsContainerFluid() ? "container-fluid" : "container";
    }
    public static bool IsContainerFluid([NotNull] this Rendering rendering)
    {
      return MainUtil.GetBool(rendering.Parameters[Constants.HasContainerLayoutParameters.IsFluid], false);
    }
  }
}