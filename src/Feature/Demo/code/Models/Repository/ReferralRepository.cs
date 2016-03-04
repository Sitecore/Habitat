namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Model.Definitions;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Common;
  using Sitecore.Data;
  using Sitecore.Marketing.Definitions;
  using Sitecore.Marketing.Definitions.Campaigns;
  using Sitecore.Marketing.Taxonomy;
  using Sitecore.Marketing.Taxonomy.Extensions;
  using Sitecore.Marketing.Taxonomy.Model;
  using Sitecore.Marketing.Taxonomy.Model.Channel;

  public class ReferralRepository
  {
    private readonly LocationRepository locationRepository = new LocationRepository();
    private readonly DeviceRepository deviceRepository = new DeviceRepository();
    private readonly CampaignRepository campaignRepository = new CampaignRepository();

    public Referral Get()
    {
      var campaigns = CreateCampaigns().ToArray();
      return new Referral()
             {
               Campaigns = campaigns,
               Device = GetDevice(),
               TotalNoOfCampaigns = campaigns.Length,
               Location = GetLocation(),
               ReferringSite = GetReferringSite()
             };
    }

    private string GetReferringSite()
    {
      return Tracker.Current.Interaction.ReferringSite;
    }

    private Location GetLocation()
    {
      return locationRepository.GetCurrent();
    }

    private Device GetDevice()
    {
      return deviceRepository.GetCurrent();
    }

    private IEnumerable<Campaign> CreateCampaigns()
    {
      var activeCampaign = GetActiveCampaign();
      if (activeCampaign != null)
        yield return activeCampaign;

      foreach (var campaign in GetHistoricCampaigns())
      {
        yield return campaign;
      }
    }

    private IEnumerable<Campaign> GetHistoricCampaigns()
    {
      return campaignRepository.GetHistoric();
    }

    private Campaign GetActiveCampaign()
    {
      return campaignRepository.GetCurrent();
    }
  }
}