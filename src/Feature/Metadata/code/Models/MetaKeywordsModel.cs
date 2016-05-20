namespace Sitecore.Feature.Metadata.Models
{
  using System.Collections.Generic;
  using System.Text;

  public class MetaKeywordsModel
  {
    public IEnumerable<string> Keywords { get; set; }

    public override string ToString()
    {
      var stringBuilder = new StringBuilder();
      var result = string.Join(",", this.Keywords);
      return result;
    }
  }
}