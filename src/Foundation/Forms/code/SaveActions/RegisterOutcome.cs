namespace Sitecore.Foundation.Forms.SaveActions
{
  using System.Drawing;
  using Sitecore.Analytics.Outcome;
  using Sitecore.Data;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.WFFM.Abstractions.Actions;
  using Sitecore.WFFM.Actions.Base;

  public class RegisterOutcome : WffmSaveAction
  {
    private readonly ITrackerService trackerService;

    public RegisterOutcome(ITrackerService trackerService)
    {
      this.trackerService = trackerService;
    }

    public RegisterOutcome() : this(new TrackerService())
    {
    }

    public string Outcome { get; set; }

    public override void Execute(ID formId, AdaptedResultList adaptedFields, ActionCallContext actionCallContext = null, params object[] data)
    {
      if (string.IsNullOrEmpty(this.Outcome) || !ID.IsID(this.Outcome))
      {
        Log.Warn("Can't register an outcome. Outcome isn't set",this);
        return;
      }

      var outcomeItem = Context.Database.GetItem(new ID(this.Outcome));
      if (outcomeItem == null || outcomeItem.TemplateID != Constants.OutcomeTemplateId)
      {
        Log.Warn("Can't register an outcome. Wrong outcome definition", this);
        return;
      }

      this.trackerService.TrackOutcome(new ID(this.Outcome));
    }
  }
}