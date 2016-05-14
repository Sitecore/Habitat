namespace Sitecore.Feature.Demo.Models
{
  using Sitecore.Feature.Demo.Models.Repository;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public class ExperienceData
  {
    private IContactProfileProvider contactProfileProvider;
    private IProfileProvider profileProvider;

    public ExperienceData(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;

      this.Visits = new VisitsRepository(contactProfileProvider).Get();
      this.PersonalInfo = new PersonalInfoRepository(contactProfileProvider).Get();
      this.OnsiteBehavior = new OnsiteBehaviorRepository(profileProvider).Get();
      this.Referral = new ReferralRepository().Get();
    }

    public Visits Visits { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
    public OnsiteBehavior OnsiteBehavior { get; set; }
    public Referral Referral { get; set; }
  }
}