namespace Sitecore.Foundation.Testing.Specflow.Infrastructure
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