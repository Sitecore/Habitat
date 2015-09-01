namespace Habitat.Framework.Assets.Models
{
    internal class AssetRequirement
    {
        public AssetRequirement(AssetType type, string file, ScriptLocation location = ScriptLocation.Body, string inline = null, string addOnceToken = null)
        {
            this.Type = type;
            this.File = file;
            this.Location = location;
            this.Inline = inline;
            this.AddOnceToken = addOnceToken;
        }

        public ScriptLocation Location { get; set; }

        public string File { get; set; }

        public string Inline { get; set; }

        public string AddOnceToken { get; set; }

        public AssetType Type { get; set; }

        public long GetDataLength()
        {
            var total = 0L;

            if (this.File != null)
            {
                total += this.File.Length;
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