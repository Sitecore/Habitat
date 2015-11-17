namespace Habitat.Framework.SitecoreExtensions.Model
{
  public class Image
  {
    public string Source { get; set; }
    public string Title { get; set; }
    public string Extension { get; set; }
    public long FileSize { get; set; }
    public string MediaId { get; set; }
    public string AlternateText { get; set; }
    public string MediaPath { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int? VerticalSpace { get; set; }
    public int? HorisontalSpace { get; set; }
  }
}