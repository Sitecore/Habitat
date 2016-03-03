namespace Sitecore.Feature.Demo.Models
{
  using System.Web.Mvc;
  using Services;
  using Foundation.SitecoreExtensions.Services;
  using Sitecore.Feature.Demo.Models.Factory;

  public class ExperienceData
  {
    private IContactProfileProvider contactProfileProvider;
    private IProfileProvider profileProvider;

    public ExperienceData(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;

      Visits = new VisitsFactory(contactProfileProvider, profileProvider).Create();
      PersonalInfo = new PersonalInfoFactory(contactProfileProvider, profileProvider).Create();
      OnsiteBehavior = new OnsiteBehaviorFactory(contactProfileProvider, profileProvider).Create();
      Referral = new ReferralFactory(contactProfileProvider, profileProvider).Create();
    }

    public Visits Visits { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
    public OnsiteBehavior OnsiteBehavior { get; set; }
    public Referral Referral { get; set; }
  }
}