using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Multisite.Repositories
{
  using Sitecore.Feature.Multisite.Models;

  public interface IMultisiteRepository
  {
    SiteDefinitions GetSiteDefinitions();
  }
}
