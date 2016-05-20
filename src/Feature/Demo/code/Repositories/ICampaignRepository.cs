﻿namespace Sitecore.Feature.Demo.Repositories
{
  using System.Collections.Generic;
  using Sitecore.Feature.Demo.Models;

  public interface ICampaignRepository
  {
    Campaign GetCurrent();
    IEnumerable<Campaign> GetHistoric();
  }
}