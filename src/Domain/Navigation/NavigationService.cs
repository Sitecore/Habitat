using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Navigation.Models;

namespace Habitat.Navigation
{
    public interface INavigationService
    {
        Item GetNavigationRoot(Item contextItem);
        NavigationItems GetBreadcrumb();
        NavigationItems GetPrimaryMenu();
        NavigationItem GetSecondaryMenuItem();
    }

    public class NavigationService : INavigationService
    {
        public NavigationService(Item contextItem)
        {
            this.ContextItem = contextItem;
            this.NavigationRoot = this.GetNavigationRoot(this.ContextItem);
            if (this.NavigationRoot == null)
                throw new InvalidOperationException($"Cannot determine navigation root from '{this.ContextItem.Paths.FullPath}'");
        }

        public Item ContextItem { get; }
        public Item NavigationRoot { get; }

        public Item GetNavigationRoot(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Templates.NavigationRoot.ID) ?? Context.Site.GetContextItem(Templates.NavigationRoot.ID);
        }

        public NavigationItems GetBreadcrumb()
        {
            var items = new NavigationItems
            {
                Items = this.GetNavigationHierarchy().Reverse().ToList(),
            };

            for (var i = 0; i < items.Items.Count - 1; i++)
                items.Items[i].Level = i;

            return items;
        }

        public NavigationItems GetPrimaryMenu()
        {
            var navItems = this.GetChildNavigationItems(this.NavigationRoot, 0, 0);

            if (MainUtil.GetBool(this.NavigationRoot[Templates.NavigationRoot.Fields.IncludeRootInPrimaryMenu], false))
            {
                if (this.NavigationRoot.IsDerived(Templates.Navigable.ID))
                {
                    var navigationItem = this.CreateNavigationItem(this.NavigationRoot, 0, 0);
                    //Root navigation item is only active when we are actually on the root item
                    navigationItem.IsActive = this.ContextItem.ID == this.NavigationRoot.ID;
                    navItems.Items.Insert(0, navigationItem);
                }
            }
            return navItems;
        }

        public NavigationItem GetSecondaryMenuItem()
        {
            var rootItem = this.GetSecondaryMenuRoot();
            return rootItem == null ? null : this.CreateNavigationItem(rootItem, 0, 2);
        }

        private Item GetSecondaryMenuRoot()
        {
            return this.FindActivePrimaryMenuItem();
        }

        private Item FindActivePrimaryMenuItem()
        {
            var primaryMenuItems = this.GetPrimaryMenu();
            //Find the active primary menu item
            var activePrimaryMenuItem = primaryMenuItems.Items.FirstOrDefault(i => i.Item.ID != this.NavigationRoot.ID && i.IsActive);
            return activePrimaryMenuItem?.Item;
        }

        private IEnumerable<NavigationItem> GetNavigationHierarchy()
        {
            var item = this.ContextItem;
            while (item != null)
            {
                if (item.IsDerived(Templates.Navigable.ID))
                    yield return this.CreateNavigationItem(item, 0);

                item = item.Parent;
            }
        }

        private NavigationItem CreateNavigationItem(Item item, int level, int maxLevel = -1)
        {
            return new NavigationItem()
            {
                Item = item,
                IsActive = this.IsItemActive(item),
                Children = this.GetChildNavigationItems(item, level + 1, maxLevel)
            };
        }

        private NavigationItems GetChildNavigationItems(Item parentItem, int level, int maxLevel)
        {
            if (level > maxLevel || !parentItem.HasChildren)
                return null;
            var childItems = parentItem.Children.Where(i => MainUtil.GetBool(i[Templates.Navigable.Fields.ShowInNavigation], true)).Select(i => this.CreateNavigationItem(i, level, maxLevel));
            return new NavigationItems()
            {
                Items = childItems.ToList()
            };
        }

        private bool IsItemActive(Item item)
        {
            return this.ContextItem.ID == item.ID || this.ContextItem.Axes.GetAncestors().Any(a => a.ID == item.ID);
        }
    }
}