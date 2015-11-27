namespace Habitat.EmotionAware.Services
{
    using Habitat.EmotionAware.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Habitat.Framework.ProjectOxfordAI.Enums;
    using Habitat.SitecoreExtensions.Helpers;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data.Items;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Model.Entities;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class EmotionAnalyticsService : IEmotionAnalyticsService
    {

        private readonly IAnalyticsRepository analyticsRepository;
        private readonly List<Item> emotionGoals;

        public EmotionAnalyticsService() : this(new AnalyticsRepository())
        {
            //this.emotionGoals = this.analyticsRepository.GetGoalsByCategoryFolder("Emotions").ToList();
        }

        public EmotionAnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            this.analyticsRepository = analyticsRepository;
            
        }

        private bool IsTracking()
        {
            if (!Tracker.IsActive)
            {
                Tracker.StartTracking();
            }

            if (Tracker.Current != null && Tracker.Current.Interaction != null && Tracker.Current.Interaction.CurrentPage != null)
            {
                return true;
            }

            Log.Warn("Tracker.Current == null || Tracker.Current.Interaction.CurrentPage == null", typeof(EmotionAnalyticsService));
            return false;
        }

        public ITagValue RegisterEmotionOnCurrentContact(Emotions emotion)
        {

            if (!this.IsTracking())
                return null;
          
            //Register the emotion on the contact
            if (Sitecore.Analytics.Tracker.Current.Contact.Tags.Find("Emotion") != null)
               return Sitecore.Analytics.Tracker.Current.Contact.Tags.Set("Emotion", emotion.ToString());
            else
               return Sitecore.Analytics.Tracker.Current.Contact.Tags.Add("Emotion", emotion.ToString());

        }

        public PageEventData RegisterGoal(string goalName, string pageUrl)
        {

            if (!this.IsTracking())
                return null;

            IPageContext pageContext = Sitecore.Analytics.Tracker.Current.Interaction.GetPages().LastOrDefault(page => page.Url.Path.Equals(pageUrl));

            if (pageContext == null)
            {
                Log.Warn($"Page {pageUrl} does not exist", typeof(EmotionAnalyticsService));
                return null;
            }

            Item goalItem = this.emotionGoals.FirstOrDefault(goal => goal.Name.Equals(goalName));

            if (goalItem == null)
            {
                Log.Warn($"Goal {goalName} does not exist", typeof(EmotionAnalyticsService));
                return null;
            }

            PageEventItem pageEvent = new PageEventItem(goalItem);

            return pageContext.Register(pageEvent);
            
        }
    }
}