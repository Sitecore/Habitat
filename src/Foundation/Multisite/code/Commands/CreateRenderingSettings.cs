namespace Sitecore.Foundation.Multisite.Commands
{
    using System;
    using System.Collections.Specialized;
  using System.Text.RegularExpressions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Shell.Framework.Commands;
  using Sitecore.Text;
  using Sitecore.Web.UI.Sheer;

  public class CreateRenderingSettings : Command
  {
    private const string DatasourceLocationFieldName = "Datasource Location";

    public override void Execute(CommandContext context)
    {
      var parameters = new NameValueCollection();
      var parentId = context.Parameters["parentID"];
      if (String.IsNullOrEmpty(parentId))
      {
        var item = context.Items[0];
        parentId = item.ID.ToString();
      }
      parameters.Add("item", parentId);
      Context.ClientPage.Start(this, "Run", parameters);
    }

    public void Run(ClientPipelineArgs args)
    {
      if (!args.IsPostBack)
      {
        ShowDatasourceSettingsDialog();
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult)
        {
          return;
        }
        var itemId = ID.Parse(args.Parameters["item"]);
        CreateDatasourceConfigurationItem(itemId, args.Result);
      }
    }

    private static void CreateDatasourceConfigurationItem(ID contextItemId, string renderingItemId)
    {
      var contextItem = Context.ContentDatabase.GetItem(contextItemId);
      if (contextItem == null)
      {
        return;
      }

      var renderingItem = Context.ContentDatabase.GetItem(renderingItemId);
      if (renderingItem == null)
      {
        return;
      }

      var datasourceConfigurationName = GetDatasourceConfigurationName(renderingItem);

      contextItem.Add(datasourceConfigurationName, new TemplateID(Templates.DatasourceConfiguration.ID));
    }

    private static string GetDatasourceConfigurationName(Item renderingItem)
    {
      var datasourceLocationValue = renderingItem[DatasourceLocationFieldName];
      var datasourceConfigurationName = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocationValue);
      if (string.IsNullOrEmpty(datasourceConfigurationName))
      {
        datasourceConfigurationName = renderingItem.Name;
      }
      return datasourceConfigurationName;
    }

    private static void ShowDatasourceSettingsDialog()
    {
      var urlString = new UrlString(Context.Site.XmlControlPage)
                      {
                        ["xmlcontrol"] = "DatasourceSettings"
                      };
      var dialogOptions = new ModalDialogOptions(urlString.ToString())
                          {
                            Response = true
                          };
      SheerResponse.ShowModalDialog(dialogOptions);
    }
  }
}