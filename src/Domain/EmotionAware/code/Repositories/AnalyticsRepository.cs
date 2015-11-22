

namespace Habitat.EmotionAware.Repositories
{
    using Habitat.Framework.SitecoreExtensions.Extensions;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class AnalyticsRepository : IAnalyticsRepository
    {

        public Item GetGoalsAndEventsFolder()
        {

            Item templateGoalsAndEventsFolder = Sitecore.Context.ContentDatabase.GetItem(EmotionAware.Templates.GoalsAndEventsFolder.ID);

            return templateGoalsAndEventsFolder.GetReferrersAsItems().FirstOrDefault();

        }


        public IEnumerable<Item> GetGoalsByCategoryFolder(string categoryFolderName)
        {
            Item goalsAndEventsFolder = this.GetGoalsAndEventsFolder();

            Item categoryFolder = goalsAndEventsFolder.GetDescendantsOrSelfOfTemplate(EmotionAware.Templates.GoalCategory.ID).FirstOrDefault(item => item.Name.Equals(categoryFolderName));

            return categoryFolder?.GetChildren().Where(item=>item.IsDerived(EmotionAware.Templates.Goal.ID));

        }

    }
}