namespace Sitecore.Feature.Demo.Models
{
    using Sitecore.Feature.Demo.Repositories;
    using Sitecore.Feature.Demo.Services;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Services;

    [Service(Lifetime = Lifetime.Transient)]
    public class ExperienceData
    {
        public ExperienceData(VisitsRepository visitsRepository, PersonalInfoRepository personalInfoRepository, OnsiteBehaviorRepository onsiteBehaviorRepository, ReferralRepository referralRepository, ITrackerService trackerService)
        {
            this.Visits = visitsRepository.Get();
            this.PersonalInfo = personalInfoRepository.Get();
            this.OnsiteBehavior = onsiteBehaviorRepository.Get();
            this.Referral = referralRepository.Get();
            this.IsActive = trackerService.IsActive;
        }

        public Visits Visits { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public OnsiteBehavior OnsiteBehavior { get; set; }
        public Referral Referral { get; set; }
        public bool IsActive { get; set; }
    }
}