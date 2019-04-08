﻿namespace Sitecore.Feature.Demo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data.Items;
    using Sitecore.Analytics.Patterns;
    using Sitecore.Cintel.Reporting.Contact.ProfilePatternMatch.Processors;
    using Sitecore.Data.Fields;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Resources.Media;

    [Service(typeof(IProfileProvider))]
    public class ProfileProvider : IProfileProvider
    {
        public IEnumerable<ProfileItem> GetSiteProfiles()
        {
            var settingsItem = Context.Site.GetContextItem(Templates.ProfilingSettings.ID);
            if (settingsItem == null)
            {
                return Enumerable.Empty<ProfileItem>();
            }
            MultilistField profiles = settingsItem.Fields[Templates.ProfilingSettings.Fields.SiteProfiles];
            return profiles.GetItems().Select(i => new ProfileItem(i));
        }

        public bool HasMatchingPattern(ProfileItem currentProfile, ProfilingTypes type)
        {
            if (type == ProfilingTypes.Historic)
            {
                var userPattern = Tracker.Current.Contact.BehaviorProfiles[currentProfile.ID];
                return userPattern != null && userPattern.NumberOfTimesScored != 0;
            }
            else
            {
                var userPattern = Tracker.Current.Interaction.Profiles[currentProfile.Name];
                return userPattern != null && userPattern.Count != 0;
            }
        }

        public IEnumerable<PatternMatch> GetPatternsWithGravityShare(ProfileItem visibleProfile, ProfilingTypes type)
        {
            Assert.ArgumentNotNull(visibleProfile, nameof(visibleProfile));

            var userPattern = type == ProfilingTypes.Historic ? this.GetHistoricMatchedPattern(visibleProfile) : this.GetCurrentlyMatchedPattern(visibleProfile);

            var patterns = PopulateProfilePatternMatchesWithXdbData.GetPatternsWithGravityShare(visibleProfile, userPattern);
            return patterns.Select(patternKeyValuePair => CreatePatternMatch(visibleProfile, patternKeyValuePair)).OrderByDescending(pm => pm.MatchPercentage);
        }

        private Pattern GetCurrentlyMatchedPattern(ProfileItem visibleProfile)
        {
            return visibleProfile.PatternSpace.CreatePattern(Tracker.Current.Interaction.Profiles[visibleProfile.Name]);
        }

        private Pattern GetHistoricMatchedPattern(ProfileItem profile)
        {
            var behaviorProfile = Tracker.Current.Contact.BehaviorProfiles[profile.ID];
            if (behaviorProfile == null)
                return null;
            IProfileData profileData = new BehaviorProfileDecorator(profile, behaviorProfile);
            return profile.PatternSpace.CreatePattern(profileData);
        }

        private static PatternMatch CreatePatternMatch(ProfileItem visibleProfile, KeyValuePair<PatternCardItem, double> patternKeyValuePair)
        {
            return new PatternMatch(visibleProfile.NameField, patternKeyValuePair.Key.NameField, GetPatternImageUrl(patternKeyValuePair), patternKeyValuePair.Value);
        }

        private static string GetPatternImageUrl(KeyValuePair<PatternCardItem, double> patternKeyValuePair)
        {
            return patternKeyValuePair.Key.Image?.MediaItem == null ? string.Empty : patternKeyValuePair.Key.Image.ImageUrl(new MediaUrlOptions
                                                                                                                            {
                                                                                                                                Width = 50,
                                                                                                                                MaxWidth = 50
                                                                                                                            });
        }
    }
}