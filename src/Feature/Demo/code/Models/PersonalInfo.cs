namespace Sitecore.Feature.Demo.Models
{
  using System.Collections;
  using System.Collections.Generic;

  public class PersonalInfo
  {
    public string FullName { get; set; }
    public bool IsIdentified { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Properties { get; set; }
    public string PhotoUrl { get; set; }
    public Location Location { get; set; }
    public Device Device { get; set; }
  }
}