namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  public class TestCleanupAction
  {
    public ActionType ActionType { get; set; }
    public object Payload { get; set; }

    public T GetPayload<T>() where T:class
    {
      return Payload as T;
    }
  }
}