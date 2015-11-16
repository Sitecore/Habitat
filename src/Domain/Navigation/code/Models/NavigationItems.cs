namespace Habitat.Navigation.Models
{
  using System.Collections.Generic;
  using Sitecore.Mvc.Presentation;

  public class NavigationItems : RenderingModel
  {
    public IList<NavigationItem> Items { get; set; }
  }
}