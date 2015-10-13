using System.Collections.Generic;
using Sitecore.Mvc.Presentation;

namespace Habitat.Navigation.Models
{
    public class NavigationItems : RenderingModel
    {
        public IList<NavigationItem> Items { get; set; }
    }
}