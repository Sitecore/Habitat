namespace Sitecore.Foundation.Personalization.Rules.RulesMacro
{
    using System.Xml.Linq;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Marketing.Definitions.Profiles;
    using Sitecore.Rules.RuleMacros;
    using Sitecore.Shell.Applications.Dialogs.ItemLister;
    using Sitecore.Text;
    using Sitecore.Web.UI.Sheer;
    using System.Linq;

    public class ProfileCardMacro : IRuleMacro
    {
        public void Execute(XElement element, string name, UrlString parameters, string value)
        {
            Assert.ArgumentNotNull(element, "element");
            Assert.ArgumentNotNull(name, "name");
            Assert.ArgumentNotNull(parameters, "parameters");
            Assert.ArgumentNotNull(value, "value");

            if (string.IsNullOrEmpty(value)) return;

            var item = Client.ContentDatabase.GetItem(value);
            var path = XElement.Parse(element.ToString()).FirstAttribute.Value;

            if (string.IsNullOrEmpty(path)) return;
            
            var filterItem = Client.ContentDatabase.GetItem(path);
            if (filterItem == null) return;

            var selectItemOptions = this.GetSelectItemOptions(filterItem, item);

            SheerResponse.ShowModalDialog(selectItemOptions.ToUrlString().ToString(), "1200px", "700px", string.Empty, true);
        }

        private SelectItemOptions GetSelectItemOptions(Item filterItem, Item rootItem)
        {
            var selectItemOptions = new SelectItemOptions
            {
                FilterItem = filterItem,
                Root = Client.ContentDatabase.GetItem(WellKnownIdentifiers.ProfilesContainerId),
            };

            selectItemOptions.SelectedItem = rootItem ?? selectItemOptions.Root?.Children.FirstOrDefault();
            selectItemOptions.IncludeTemplatesForSelection = SelectItemOptions.GetTemplateList(Templates.ProfileCard.Id.ToString());
            selectItemOptions.Title = "Select Profile Card";
            selectItemOptions.Text = "Select the profile card to use in this rule.";
            selectItemOptions.Icon = "Business/16x16/chart.png";
            selectItemOptions.ShowRoot = false;

            return selectItemOptions;
        }
    }
}