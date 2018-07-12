namespace Sitecore.Feature.Accounts.Tests.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit2;

  public class RightKeysAttribute: CustomizeAttribute
  {
    private readonly object[] keys;

    public RightKeysAttribute(params object[] keys)
    {
      this.keys = keys;
    }

    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
      if (parameter == null)
      {
        throw new ArgumentNullException("parameter");
      }

      Type type = parameter.ParameterType;

      Type[] genericArguments = type.GetGenericArguments();
      if ((int)genericArguments.Length != 2)
      {
        throw new InvalidOperationException("Wrong number of generic parameters");
      }
      if (type.GetGenericTypeDefinition() != typeof(IDictionary<,>))
      {
        throw new InvalidOperationException("Parameter isn't a dictionary");
      }

      var customizationType = typeof(RightKeysCustomization<,>).MakeGenericType(genericArguments);

      return Activator.CreateInstance(customizationType, this.keys) as ICustomization;
    }
  }
}
