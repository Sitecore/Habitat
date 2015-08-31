using System;
using System.Collections.Generic;
using System.Linq;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Navigation.Models;
using Sitecore;
using Sitecore.Data.Items;

namespace Habitat.Navigation.Repositories
{
    public interface INavigationRepository
    {
        Item GetNavigationRoot(Item contextItem);
        NavigationItems GetBreadcrumb();
        NavigationItems GetPrimaryMenu();
        NavigationItem GetSecondaryMenuItem();
    }

    public class NavigationRepository : INavigationRepository
    {
        public Item ContextItem { get; }
        public Item NavigationRoot { get; }

        public NavigationRepository(Item contextItem)
        {
            ContextItem = contextItem;
            NavigationRoot = GetNavigationRoot(ContextItem);
            if (NavigationRoot == null)
                throw new InvalidOperationException($"Cannot determine navigation root from '{ContextItem.Paths.FullPath}'");
        }
        
        public Item GetNavigationRoot(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Templates.NavigationRoot.ID) ??
                   Context.Site.GetContextItem(Templates.NavigationRoot.ID);
        }

        public NavigationItems GetBreadcrumb()
        {
            var items = new NavigationItems
            {
                Items = GetNavigationHierarchy().Reverse().ToList()
            };

            for (var i = 0; i < items.Items.Count - 1; i++)
                items.Items[i].Level = i;

            return items;
        }

        public NavigationItems GetPrimaryMenu()
        {
            var navItems = GetChildNavigationItems(NavigationRoot, 0, 0);

            if (MainUtil.GetBool(NavigationRoot[Templates.NavigationRoot.Fields.IncludeRootInPrimaryMenu], false))
            {
                if (NavigationRoot.IsDerived(Templates.Navigable.ID))
                {
                    var navigationItem = CreateNavigationItem(NavigationRoot, 0, 0);
                    //Root navigation item is only active when we are actually on the root item
                    navigationItem.IsActive = ContextItem.ID == NavigationRoot.ID;
                    navItems.Items.Insert(0, navigationItem);
                }
            }
            return navItems;
        }

        public NavigationItem GetSecondaryMenuItem()
        {
            var rootItem = GetSecondaryMenuRoot();
            return rootItem == null ? null : CreateNavigationItem(rootItem, 0, 2);
        }

        private Item GetSecondaryMenuRoot()
        {
            return FindActivePrimaryMenuItem();
        }

        private Item FindActivePrimaryMenuItem()
        {
            var primaryMenuItems = GetPrimaryMenu();
            //Find the active primary menu item
            var activePrimaryMenuItem =
                primaryMenuItems.Items.FirstOrDefault(i => i.Item.ID != NavigationRoot.ID && i.IsActive);
            return activePrimaryMenuItem?.Item;
        }

        private IEnumerable<NavigationItem> GetNavigationHierarchy()
        {
            var item = ContextItem;
            while (item != null)
            {
                if (item.IsDerived(Templates.Navigable.ID))
                    yield return CreateNavigationItem(item, 0);

                item = item.Parent;
            }
        }

        private NavigationItem CreateNavigationItem(Item item, int level, int maxLevel = -1)
        {
            return new NavigationItem
            {
                Item = item,
                IsActive = IsItemActive(item),
                Children = GetChildNavigationItems(item, level + 1, maxLevel)
            };
        }

        private NavigationItems GetChildNavigationItems(Item parentItem, int level, int maxLevel)
        {
            if (level > maxLevel || !parentItem.HasChildren)
                return null;
            var childItems =
                parentItem.Children.Where(i => MainUtil.GetBool(i[Templates.Navigable.Fields.ShowInNavigation], true))
                    .Select(i => CreateNavigationItem(i, level, maxLevel));
            return new NavigationItems
            {
                Items = childItems.ToList()
            };
        }

        private bool IsItemActive(Item item)
        {
            return ContextItem.ID == item.ID || ContextItem.Axes.GetAncestors().Any(a => a.ID == item.ID);
        }
    }
}