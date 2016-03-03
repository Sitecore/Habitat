namespace Sitecore.Feature.Demo.Models.Factory
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Model.Definitions;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Common;
  using Sitecore.Data;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Marketing.Definitions;
  using Sitecore.Marketing.Definitions.Campaigns;
  using Sitecore.Marketing.Taxonomy;
  using Sitecore.Marketing.Taxonomy.Extensions;
  using Sitecore.Marketing.Taxonomy.Model;
  using Sitecore.Marketing.Taxonomy.Model.Channel;

  public class ReferralFactory
  {
    private IContactProfileProvider contactProfileProvider;
    private IProfileProvider profileProvider;

    public ReferralFactory(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;
    }

    public Referral Create()
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
      return null;
    }

    private Device GetDevice()
    {
      return null;
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
      var keyBehaviourCache = Tracker.Current.Contact.GetKeyBehaviorCache();
      foreach (var cachedCampaign in keyBehaviourCache.Campaigns)
      {
        var campaign = GetCampaignDefinition(cachedCampaign.Id.ToID());
        if (campaign == null)
          continue;

        yield return new Campaign()
        {
          Title = campaign.Name ?? "(Unknown)",
          IsActive = false,
          Date = cachedCampaign.DateTime,
          Channel = GetChannel(campaign)
        };
      }
    }

    private Campaign GetActiveCampaign()
    {
      if (!Tracker.Current.Interaction.CampaignId.HasValue)
      {
        return null;
      }
      var campaignId = Tracker.Current.Interaction.CampaignId.Value.ToID();

      var campaign = GetCampaignDefinition(campaignId);
      if (campaign == null)
        return null;

      return new Campaign()
             {
               Title = campaign.Name ?? "(Unknown)",
               IsActive = true,
               Date = null,
               Channel = GetChannel(campaign)
      };
    }

    private string GetChannel(ICampaignActivityDefinition campaign)
    {
      if (campaign.ChannelUri == null)
        return null;
      var channelTaxonomyManager = TaxonomyManager.Provider.GetChannelManager();
      var channel = channelTaxonomyManager.GetChannel(campaign.ChannelUri, Sitecore.Context.Language.CultureInfo);
      return channel == null ? null : channelTaxonomyManager.GetFullName(channel.Uri, "/");
    }

    private static ICampaignActivityDefinition GetCampaignDefinition(ID campaignId)
    {
      var campaigns = DefinitionManagerFactory.Default.GetDefinitionManager<ICampaignActivityDefinition>();
      var campaign = campaigns.Get(campaignId, Sitecore.Context.Language.CultureInfo);
      return campaign;
    }
  }
}