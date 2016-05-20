namespace Sitecore.Foundation.Installer
{
  using System.Collections.Specialized;

  public interface IPostStepAction
  {
    void Run(NameValueCollection collection);
  }
}