namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
  using System.Text.RegularExpressions;

  public static class StringExtensions
  {
    public static string Humanize(this string input)
    {
      return Regex.Replace(input, "(\\B[A-Z])", " $1");
    }

    public static string ToCssUrlValue(this string url)
    {
      return string.IsNullOrWhiteSpace(url) ? "none" : $"url('{url}')";
    }
  }
}