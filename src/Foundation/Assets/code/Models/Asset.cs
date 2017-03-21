namespace Sitecore.Foundation.Assets.Models
{
    using System;

    public class Asset
    {
        public Asset(AssetType type, ScriptLocation location, string content, AssetContentType contentType = AssetContentType.File, string site = null)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            this.Type = type;
            this.Content = content;
            this.ContentType = contentType;
            this.Location = location;
            if (this.ContentType == AssetContentType.Inline)
            {
                this.AddOnceToken = this.Content.GetHashCode().ToString();
            }
            this.Site = site;
        }


        public string Site { get; set; }

        public ScriptLocation Location { get; set; }

        public string Content { get; set; }
        public AssetContentType ContentType { get; }

        public string Inline { get; set; }

        public string AddOnceToken { get; set; }

        public AssetType Type { get; set; }

        public long GetDataLength()
        {
            var total = 0L;

            if (this.Content != null)
            {
                total += this.Content.Length;
            }

            if (this.Inline != null)
            {
                total += this.Inline.Length;
            }

            if (this.AddOnceToken != null)
            {
                total += this.AddOnceToken.Length;
            }

            return total;
        }
    }
}