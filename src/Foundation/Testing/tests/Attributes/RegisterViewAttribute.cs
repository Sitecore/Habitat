namespace Sitecore.Foundation.Testing.Attributes
{
  using System;

  public class RegisterViewAttribute : Attribute
  {
    public string Name { get; private set; }

    public RegisterViewAttribute(string name)
    {
      this.Name = name;
    }
  }
}