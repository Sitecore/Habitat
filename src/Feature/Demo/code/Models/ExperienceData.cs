namespace Sitecore.Feature.Demo.Models
{
  using System.Web.Mvc;
  using Services;
  using Foundation.SitecoreExtensions.Services;
  using Sitecore.Feature.Demo.Models.Repository;

  public class ExperienceData
  {
    private IContactProfileProvider contactProfileProvider;
    private IProfileProvider profileProvider;

    public ExperienceData(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;

      Visits = new VisitsRepository(contactProfileProvider, profileProvider).Get();
      PersonalInfo = new PersonalInfoRepository(contactProfileProvider).Get();
      OnsiteBehavior = new OnsiteBehaviorRepository(profileProvider).Get();
      Referral = new ReferralRepository().Get();
    }

    public Visits Visits { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
    public OnsiteBehavior OnsiteBehavior { get; set; }
    public Referral Referral { get; set; }
  }
}