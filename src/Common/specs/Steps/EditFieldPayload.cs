namespace Common.Specflow.Steps
{
  using Common.Specflow.UtfService;

  public class EditFieldPayload
  {
    public string ItemIdOrPath { get; set; }
    public string FieldName { get; set; }
    public string FieldValue { get; set; }
    public Database Database { get; set; }
  }
}