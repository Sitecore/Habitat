using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Feature.FAQ.Repositories
{
    public static class GroupMemberRepository
    {
        public static IEnumerable<Item> Get(Item item)
        {
            return item.GetMultiListValues(Templates.FaqGroup.Fields.GroupMember);
        }
    }
}