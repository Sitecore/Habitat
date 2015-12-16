namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
  using System.Text.RegularExpressions;

  public static class StringExtensions
  {
    public static string Humanize(this string input)
    {
      return Regex.Replace(input, "(\\B[A-Z])", " $1");
    }
  }
}