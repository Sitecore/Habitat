namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using Sitecore.Foundation.Common.Specflow.Service_References.UtfService;

  public class EditFieldPayload
  {
    public string ItemIdOrPath { get; set; }
    public string FieldName { get; set; }
    public string FieldValue { get; set; }
    public Database Database { get; set; }
  }
}