using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Commands
{
  using System.Collections.Specialized;
  using Sitecore.Data;
  using Sitecore.Presentation;
  using Sitecore.Shell.Framework.Commands;
  using Sitecore.Text;
  using Sitecore.Web.UI.Sheer;

  public class CreateRenderingSettings : Command
  {
    public override void Execute(CommandContext context)
    {
      var item = context.Items[0];
      NameValueCollection parameters = new NameValueCollection();
      parameters.Add("item", item.ID.ToString());
      Context.ClientPage.Start((object)this, "Run", parameters);
    }

    public void Run(ClientPipelineArgs args)
    {
      var itemId = ID.Parse(args.Parameters["item"]);
      if (args.IsPostBack)
      {
        if (args.HasResult)
        {
          var contextItem = Sitecore.Context.ContentDatabase.GetItem(itemId);
          if (contextItem == null)
          {
            return;
          }

          var renderingItemId = args.Result;
          var renderingItem = Sitecore.Context.ContentDatabase.GetItem(renderingItemId);
          if (renderingItem != null)
          {
            contextItem.Add(renderingItem.Name, new TemplateID(Templates.DatasourceConfiguration.ID));
          }
        }
      }
      else
      {
        UrlString urlString = new UrlString(Context.Site.XmlControlPage);
        urlString["xmlcontrol"] = "DatasourceSettings";
        var dialogOptions = new ModalDialogOptions(urlString.ToString());
        dialogOptions.Response = true;
        SheerResponse.ShowModalDialog(dialogOptions);
        args.WaitForPostBack();
      }
    }
  }
}