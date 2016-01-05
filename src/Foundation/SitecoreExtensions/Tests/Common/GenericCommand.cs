namespace Sitecore.Foundation.SitecoreExtensions.Tests.Common
{
  using Ploeh.AutoFixture.Kernel;

  public abstract class GenericCommand<T> : ISpecimenCommand where T :class
  {
    public void Execute(object specimen, ISpecimenContext context)
    {

      var castedSpecimen = specimen as T;
      if (castedSpecimen == null)
      {
        return;
      }

      ExecuteAction(castedSpecimen, context);
    }

    protected abstract void ExecuteAction(T specimen, ISpecimenContext context);
  }
}