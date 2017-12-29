namespace Sitecore.Feature.Demo.Models
{
    using System.Linq;
    using Sitecore.Analytics.Model;
    using Sitecore.Data.Items;
    using Sitecore.ExperienceExplorer.Business.Utilities.Extensions;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Text;
    using static Configuration.Factory;

    public class DemoContent
    {
        private WhoIsInformation _geoData;

        public DemoContent(Item item)
        {
            this.Item = item;
        }

        public Item Item { get; set; }

        public string HtmlContent
        {
            get
            {
                var content = this.Item[Templates.DemoContent.Fields.HTMLContent];
                return this.ReplaceTokens(content);
            }
        }

        public string Referrer => this.Item[Templates.DemoContent.Fields.Referrer];
        public string IpAddress => this.Item[Templates.DemoContent.Fields.IPAddress];

        public WhoIsInformation GeoData => this._geoData ?? (this._geoData = this.GetGeoData());

        private WhoIsInformation GetGeoData()
        {
            var lat = this.Item.GetFieldAsDouble(Templates.DemoContent.Fields.Latitude, 0);
            var lon = this.Item.GetFieldAsDouble(Templates.DemoContent.Fields.Longitude, 0);

            if (lat == 0 || lon == 0)
                return null;

            var geoData = new WhoIsInformation
            {
                Latitude = this.Item.GetFieldAsDouble(Templates.DemoContent.Fields.Latitude, 0),
                Longitude = this.Item.GetFieldAsDouble(Templates.DemoContent.Fields.Longitude, 0),
                AreaCode = this.Item[Templates.DemoContent.Fields.AreaCode],
                BusinessName = this.Item[Templates.DemoContent.Fields.BusinessName],
                City = this.Item[Templates.DemoContent.Fields.City],
                Country = this.Item[Templates.DemoContent.Fields.Country],
                Dns = this.Item[Templates.DemoContent.Fields.DNS],
                Isp = this.Item[Templates.DemoContent.Fields.ISP],
                MetroCode = this.Item[Templates.DemoContent.Fields.MetroCode],
                PostalCode = this.Item[Templates.DemoContent.Fields.PostalCode],
                Region = this.Item[Templates.DemoContent.Fields.Region],
                Url = this.Item[Templates.DemoContent.Fields.Url],
                IsUnknown = false
            };
            return geoData;
        }

    private string ReplaceTokens(string content)
        {
            var replacer = GetMasterVariablesReplacer();
            using (new ReplacerContextSwitcher(this.GetReplacementTokens()))
            {
                return replacer.Replace(content, this.Item);
            }
        }

        private string[] GetReplacementTokens()
        {
            return this.Item.Children.Where(i => i.IsDerived(Templates.Token.ID)).SelectMany(i => new[] {$"${i.Name}", i[Templates.Token.Fields.TokenValue]}).ToArray();
        }
    }
}