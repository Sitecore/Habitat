namespace Sitecore.Foundation.Assets.Models
{
  using System;

  internal class Asset
  {
    public Asset(AssetType type, ScriptLocation location, string file = null, string inline = null, string site = null)
    {
      this.Type = type;
      this.File = file;
      this.Location = location;
      this.Inline = inline;
      if (!string.IsNullOrEmpty(inline))
      {
        this.AddOnceToken = inline.GetHashCode().ToString();
      }
      this.Site = site;
    }


    public string Site { get; set; }

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