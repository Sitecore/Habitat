﻿namespace Sitecore.Foundation.Multisite.Dialogs
{
  using System;
  using Sitecore.Configuration;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Web.UI.HtmlControls;
  using Sitecore.Web.UI.Pages;
  using Sitecore.Web.UI.Sheer;
  using Sitecore.Web.UI.WebControls;
  using Sitecore.Web.UI.XmlControls;

  public class DatasourceSettingsPage : DialogForm
  {
    private const string DialogRootSettingName = "Foundation.Multisite.DatasourceDialogRoot";

    protected DataContext DataContext;
    /// <summary>
    /// The dialog.
    /// 
    /// </summary>
    protected XmlControl Dialog;
    /// <summary>
    /// The items.
    /// 
    /// </summary>
    protected Border Items;
    /// <summary>
    /// The treeview.
    /// 
    /// </summary>
    protected TreeviewEx Treeview;
    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object)e, "e");
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      if (this.DataContext != null)
      {
        this.DataContext.GetFromQueryString();
        this.DataContext.Root = this.Root;
        this.DataContext.Filter = this.GetFilter();
      }
    }

    protected string Root
    {
      get
      {
        return Settings.GetSetting(DialogRootSettingName, "/sitecore/layout/renderings/feature");
      }
    }

    protected void OK_Click()
    {
      Item selectionItem = this.Treeview.GetSelectionItem();
      if (selectionItem == null)
      {
        SheerResponse.Alert("Select an item.");
      }
      else
      {
        this.SetDialogResult(selectionItem);
        SheerResponse.CloseWindow();
      }
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Assert.ArgumentNotNull(sender, "sender");
      Assert.ArgumentNotNull((object)args, "args");
      this.OK_Click();
    }

    protected virtual void SetDialogResult(Item selectedItem)
    {
      Assert.ArgumentNotNull((object)selectedItem, "selectedItem");
      SheerResponse.SetDialogValue(selectedItem.ID.ToString());
    }

    protected string GetFilter()
    {
      return string.Format("(contains(@@templatekey, 'folder') or contains(@Datasource Location, '" + DatasourceConfigurationService.SiteDatasourcePrefix + "'))");
    }
  }
}