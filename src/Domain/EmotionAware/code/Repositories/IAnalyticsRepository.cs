namespace Habitat.EmotionAware.Repositories
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    public interface IAnalyticsRepository
    {
        Item GetGoalsAndEventsFolder();
        IEnumerable<Item> GetGoalsByCategoryFolder(string categoryFolderName);
    }
}