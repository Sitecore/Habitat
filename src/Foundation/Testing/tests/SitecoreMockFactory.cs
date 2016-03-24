namespace Sitecore.Foundation.Testing
{
  using System;
  using NSubstitute;

  public static class SitecoreMockFactory
  {
    public static object GetObject(string typeName)
    {
      var objectType = Type.GetType(typeName);
      return Substitute.For(new Type[] {objectType}, new object[0]);
    }
  }
}
