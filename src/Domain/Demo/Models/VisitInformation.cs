using System;
using System.Collections.Generic;
using System.Linq;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Framework.SitecoreExtensions.Repositories;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Automation.Data;
using Sitecore.Analytics.Data.Items;
using Sitecore.Analytics.Tracking;
using Sitecore.CES.DeviceDetection;
using Sitecore.Common;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using Convert = System.Convert;

namespace Habitat.Demo.Models
{
  public class VisitInformation
  {
    private DeviceInformation _deviceInformation;
    private bool _isDeviceLookupDone;
    public string PageCount => Convert.ToString(Tracker.Current.Interaction.PageCount);
    public string EngagementValue => Convert.ToString(Tracker.Current.Interaction.Value);
    public bool HasCampaign
    {
      get
      {
        if (!Tracker.Current.Interaction.CampaignId.HasValue)
          return false;

        var campaign = Context.Database.GetItem(Tracker.Current.Interaction.CampaignId.Value.ToID());
        return campaign != null;
      }
    }

    public string Campaign
    {
      get
      {
        if (!Tracker.Current.Interaction.CampaignId.HasValue)
          return null;

        var campaign = Context.Database.GetItem(Tracker.Current.Interaction.CampaignId.Value.ToID());
        return campaign?.Name;
      }
    }
    public bool HasGeoIp => Tracker.Current.Interaction.GeoData.Latitude.HasValue;
    public string City => Tracker.Current.Interaction.HasGeoIpData ? Tracker.Current.Interaction.GeoData.City : null;
    public string PostalCode => Tracker.Current.Interaction.HasGeoIpData ? Tracker.Current.Interaction.GeoData.PostalCode : null;
    public DeviceInformation Device => _isDeviceLookupDone ? _deviceInformation : (_deviceInformation = GetDeviceInformation());
    private DeviceInformation GetDeviceInformation()
    {
      _isDeviceLookupDone = true;
      if (!DeviceDetectionManager.IsEnabled || String.IsNullOrEmpty(Tracker.Current.Interaction.UserAgent))
        return null;
      return DeviceDetectionManager.GetDeviceInformation(Tracker.Current.Interaction.UserAgent);
    }

    public IEnumerable<PatternMatch> PatternMatches => LoadPatterns();

    public IEnumerable<PageLink> PagesViewed => LoadPages();

    public IEnumerable<string> GoalsList => LoadGoals();

    public IEnumerable<string> EngagementStates => LoadEngagementStates();

    public IEnumerable<PatternMatch> LoadPatterns()
    {
      if (!Tracker.IsActive)
        return Enumerable.Empty<PatternMatch>();

      var patternMatches = new List<PatternMatch>();
      foreach (var visibleProfile in GetSiteProfiles())
      {
        var matchingPattern = GetMatchingPatternForContact(visibleProfile);
        if (matchingPattern == null)
          continue;
        patternMatches.Add(CreatePatternMatch(matchingPattern, visibleProfile));
      }
      return patternMatches;
    }

    private static PatternMatch CreatePatternMatch(PatternCardItem matchingPattern, ProfileItem visibleProfile)
    {
      var image = matchingPattern.Image.MediaItem;
      var src = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
      var patternMatch = new PatternMatch(visibleProfile.NameField, matchingPattern.Name, src);
      return patternMatch;
    }

    private PatternCardItem GetMatchingPatternForContact(ProfileItem visibleProfile)
    {
      var userPattern = Tracker.Current.Interaction.Profiles[visibleProfile.Name];
      if (userPattern?.PatternId == null)
        return null;
      var matchingPattern = Context.Database.GetItem(userPattern.PatternId.Value.ToID());
      if (matchingPattern == null)
        return null;
      return new PatternCardItem(matchingPattern);
    }

    private IEnumerable<ProfileItem> GetSiteProfiles()
    {
      var settingsItem = Sitecore.Context.Site.GetContextItem(Templates.ProfilingSettings.ID);
      if (settingsItem == null)
        return Enumerable.Empty<ProfileItem>();
      MultilistField profiles = settingsItem.Fields[Templates.ProfilingSettings.Fields.SiteProfiles];
      return profiles.GetItems().Select(i => new ProfileItem(i));
    }

    public IEnumerable<PageLink> LoadPages()
    {
      return Tracker.Current.Interaction.GetPages().Select(CreatePageLink).Reverse();
    }

    private PageLink CreatePageLink(IPageContext page)
    {
      return new PageLink(CleanPageName(page), page.Url.Path, false);
    }

    public IEnumerable<string> LoadGoals()
    {
      var goals = new List<string>();

      var conversions = Tracker.Current.Interaction.GetPages().SelectMany(page => page.PageEvents.Where(pe => pe.IsGoal)).Reverse().ToArray();

      goals.AddRange(conversions.Select(goal => $"{goal.Name} ({goal.Value})"));

      return goals;
    }

    public IEnumerable<string> LoadEngagementStates()
    {
      var states = new List<string>();
      try
      {
        var automationStateManager = AutomationStateManager.Create(Tracker.Current.Contact);
        var engagementstates = automationStateManager.GetAutomationStates().ToArray();

        states.AddRange(engagementstates.Select(context => $"{context.PlanItem.DisplayName}: {context.StateItem.DisplayName}"));
      }
      catch (Exception ex)
      {
        Log.Error("VisitInformation: Could not load engagement states", ex);
      }
      return states;
    }

    private string CleanPageName(IPageContext p)
    {
      var pageName = p.Url.Path.Replace("/en", "/").Replace("//", "/").Remove(0, 1).Replace(".aspx", "");
      if (pageName == string.Empty || pageName == "en")
        pageName = "Home";
      if (pageName.Contains("/"))
        pageName = "..." + pageName.Substring(pageName.LastIndexOf("/", StringComparison.Ordinal));
      return pageName.Length < 27 ? $"{pageName} ({(p.Duration/1000.0).ToString("f2")}s)" : $"{pageName.Substring(0, 26)}... ({(p.Duration/1000.0).ToString("f2")}s)";
    }
  }
}