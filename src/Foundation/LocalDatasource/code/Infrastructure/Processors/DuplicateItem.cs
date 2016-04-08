namespace Sitecore.Foundation.LocalDatasource.Infrastructure.Processors
{
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.Web.UI.Sheer;

  public class DuplicateItem
  {
    private Item _itemToDuplicate;

    public void Execute(ClientPipelineArgs args)
    {
      var copy = this.Duplicate(args);
      if (copy == null)
      {
        return;
      }

      if (this._itemToDuplicate == null)
      {
        return;
      }

      //new ReferenceReplacementJob(this._itemToDuplicate, copy).StartAsync();
    }

    private Item Duplicate(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      var item = this.GetItemToDuplicate(args);
      this._itemToDuplicate = item;
      if (item == null)
      {
        SheerResponse.Alert("Item not found.");
        args.AbortPipeline();
      }
      else
      {
        var parent = item.Parent;
        if (parent == null)
        {
          SheerResponse.Alert("Cannot duplicate the root item.");
          args.AbortPipeline();
        }
        else if (parent.Access.CanCreate())
        {
          Log.Audit(this, "Duplicate item: {0}", AuditFormatter.FormatItem(item));
          return Context.Workflow.DuplicateItem(item, args.Parameters["name"]);
        }
        else
        {
          SheerResponse.Alert(Translate.Text("You do not have permission to duplicate \"{0}\".", item.DisplayName));
          args.AbortPipeline();
        }
      }
      return null;
    }

    private Item GetItemToDuplicate(ClientPipelineArgs args)
    {
      Language language;
      var database = Factory.GetDatabase(args.Parameters["database"]);
      Assert.IsNotNull(database, args.Parameters["database"]);
      var str = args.Parameters["id"];
      if (!Language.TryParse(args.Parameters["language"], out language))
      {
        language = Context.Language;
      }
      return database.GetItem(ID.Parse(str), language);
    }
  }
}