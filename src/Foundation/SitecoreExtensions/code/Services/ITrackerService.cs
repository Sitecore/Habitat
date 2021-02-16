namespace Sitecore.Foundation.SitecoreExtensions.Services
{
    using System;

    public interface ITrackerService
    {
        bool IsActive { get; }
        void IdentifyContact(string source, string identifier);
        void TrackOutcome(Guid outcomeDefinitionId);
        void TrackPageEvent(Guid pageEventId, string text = null, string data = null, string dataKey = null, int? value = null);
        void TrackGoal(Guid goalId, string text = null, string data = null, string dataKey = null, int? value = null);
    }
}