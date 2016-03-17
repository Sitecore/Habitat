namespace Sitecore.Feature.Demo.Models
{
  using System.Collections;
  using System.Collections.Generic;

  public class Referral
  {
    public string ReferringSite { get; set; }
    public int TotalNoOfCampaigns { get; set; }
    public IEnumerable<Campaign> Campaigns { get; set; }
  }
}