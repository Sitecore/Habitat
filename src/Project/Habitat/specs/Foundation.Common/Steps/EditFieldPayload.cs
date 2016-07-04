
using Sitecore.Foundation.Common.Specflow.UtfService;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  

  public class EditFieldPayload
  {
    public string ItemIdOrPath { get; set; }
    public string FieldName { get; set; }
    public string FieldValue { get; set; }
  }
}