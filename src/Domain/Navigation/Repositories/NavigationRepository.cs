using System;
using System.Collections.Generic;
using System.Linq;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Navigation.Models;
using Sitecore;
using Sitecore.Data.Items;

namespace Habitat.Navigation.Repositories
{
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
            var navItems = GetChildNavigationItems(NavigationRoot, 0, 1);

            AddRootToPrimaryMenu(navItems);
            return navItems;
        }

        private void AddRootToPrimaryMenu(NavigationItems navItems)
        {
            if (!IncludeInNavigation(NavigationRoot))
                return;

            var navigationItem = CreateNavigationItem(NavigationRoot, 0, 0);
            //Root navigation item is only active when we are actually on the root item
            navigationItem.IsActive = ContextItem.ID == NavigationRoot.ID;
            navItems.Items.Insert(0, navigationItem);
        }

        private bool IncludeInNavigation(Item item)
        {
            return item.IsDerived(Templates.Navigable.ID) && MainUtil.GetBool(item[Templates.Navigable.Fields.ShowInNavigation], false);
        }

        public NavigationItem GetSecondaryMenuItem()
        {
            var rootItem = GetSecondaryMenuRoot();
            return rootItem == null ? null : CreateNavigationItem(rootItem, 0, 2);
        }

        public NavigationItems GetLinkMenuItems(Item menuRoot)
        {
            if (menuRoot == null)
                throw new ArgumentNullException(nameof(menuRoot));
            return GetChildNavigationItems(menuRoot, 0, 0);
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
                if (IncludeInNavigation(item))
                    yield return CreateNavigationItem(item, 0);

                item = item.Parent;
            }
        }

        private NavigationItem CreateNavigationItem(Item item, int level, int maxLevel = -1)
        {
            return new NavigationItem
            {
                Item = item,
                Url = (item.IsDerived(Templates.Link.ID) ? item.LinkFieldUrl(Templates.Link.Fields.Link) : item.Url()),
                Target = (item.IsDerived(Templates.Link.ID) ? item.LinkFieldTarget(Templates.Link.Fields.Link) : ""),
                IsActive = IsItemActive(item),
                IsCurrent = IsItemCurrent(item),
                Children = GetChildNavigationItems(item, level + 1, maxLevel)
            };
        }

        private NavigationItems GetChildNavigationItems(Item parentItem, int level, int maxLevel)
        {
            if (level > maxLevel || !parentItem.HasChildren)
                return null;
            var childItems = parentItem.Children.Where(IncludeInNavigation).Select(i => CreateNavigationItem(i, level, maxLevel));
            return new NavigationItems
            {
                Items = childItems.ToList()
            };
        }

        private bool IsItemActive(Item item)
        {
            return ContextItem.ID == item.ID || ContextItem.Axes.GetAncestors().Any(a => a.ID == item.ID);
        }
        private bool IsItemCurrent(Item item)
        {
            return ContextItem.ID == item.ID;
        }
    }
}