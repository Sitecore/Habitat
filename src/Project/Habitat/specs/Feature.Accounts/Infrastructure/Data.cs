using Newtonsoft.Json;
using Sitecore.Feature.Accounts.Specflow.Steps;

namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  

  public class Data
  {
    
    [JsonProperty("dataSet")]
    public  Dataset Dataset { get; set; }
  }
}