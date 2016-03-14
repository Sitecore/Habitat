using System.Collections.Generic;

namespace Sitecore.Feature.Demo.Models.Repository
{
  public interface ICampaignRepository
  {
    Campaign GetCurrent();
    IEnumerable<Campaign> GetHistoric();
  }
}