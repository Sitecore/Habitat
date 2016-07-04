namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Foundation.Common.Specflow.Steps;

  public class CleanupPool : List<TestCleanupAction>, IDisposable
  {
    public void Dispose()
    {
      this.ForEach(CleanupExecute);
    }


    private static void CleanupExecute(TestCleanupAction payload)
    {
      if (payload.ActionType == ActionType.RemoveUser)
      {
        ContextExtensions.HelperService.DeleteUser(payload.GetPayload<string>());
        return;
      }
      if (payload.ActionType == ActionType.CleanFieldValue)
      {
        var fieldPayload = payload.GetPayload<EditFieldPayload>();
        ContextExtensions.UtfService.EditItem(fieldPayload.ItemIdOrPath, fieldPayload.FieldName, fieldPayload.FieldValue);
        return;
      }

      if (payload.ActionType == ActionType.DeleteItem)
      {
        var fieldPayload = payload.GetPayload<EditFieldPayload>();
        ContextExtensions.UtfService.DeleteItem(fieldPayload.ItemIdOrPath, false);
        return;
      }

      throw new NotSupportedException($"Action type '{payload.ActionType}' is not supported");
    }
  }
}