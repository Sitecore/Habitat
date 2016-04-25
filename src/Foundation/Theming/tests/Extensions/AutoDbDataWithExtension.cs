namespace Sitecore.Foundation.Theming.Tests.Extensions
{
  using System;
  using Ploeh.AutoFixture;

  public class AutoDbDataWithExtension: AutoDbDataAttribute
  {
    public AutoDbDataWithExtension(params Type[] customizationTypes)
    {
      foreach (var customizationType in customizationTypes)
      {
        var customization = Activator.CreateInstance(customizationType) as ICustomization;
        if (customization == null)
        {
          throw new InvalidOperationException("Wrong customization. Interface ICustmization isn't implemented");
        }

        this.Fixture.Customize(customization);
      }
    }

    public AutoDbDataWithExtension(Type customizationType)
    {
      var customization = Activator.CreateInstance(customizationType) as ICustomization;
      if (customization == null)
      {
        throw new InvalidOperationException("Wrong customization. Interface ICustmization isn't implemented");
      }

      this.Fixture.Customize(customization);
    }
  }
}
