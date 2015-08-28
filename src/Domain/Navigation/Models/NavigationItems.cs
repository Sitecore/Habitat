namespace Habitat.Navigation.Models
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Sitecore.Mvc.Presentation;

    public class NavigationItems : RenderingModel
    {
        public IList<NavigationItem> Items { get; set; }
    }

    public class NavigationItem
    {
        public Item Item { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public NavigationItems Children { get; set; }
    }
}