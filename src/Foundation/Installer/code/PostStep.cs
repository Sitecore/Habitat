namespace Sitecore.Foundation.Installer
{
  using System;
  using System.Collections.Specialized;
  using Sitecore.Diagnostics;
  using Sitecore.Install.Framework;

  public class PostStep : IPostStep
  {
    public void Run(ITaskOutput output, NameValueCollection metaData)
    {
      var getPostStepActionList =
        metaData["Attributes"].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (var postStepAction in getPostStepActionList)
      {
        try
        {
          var postStepActionType = Type.GetType(postStepAction);
          if (postStepActionType == null)
          {
            throw new Exception("type was null");
          }

          Log.Info(postStepAction + " post step action was started", this);

          var activator = (IPostStepAction)Activator.CreateInstance(postStepActionType);
          activator.Run();
        }
        catch (Exception ex)
        {
          Log.Error(postStepAction + " post step action has failed", ex, this);
        }
      }
    }
  }
}