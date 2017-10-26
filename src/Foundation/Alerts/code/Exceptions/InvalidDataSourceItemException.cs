namespace Sitecore.Foundation.Alerts.Exceptions
{
  using System;
  using System.Runtime.Serialization;

  [Serializable]
  public class InvalidDataSourceItemException : Exception
  {
    public InvalidDataSourceItemException() : base("Data source isn't set or have wrong template")
    {
    }

    public InvalidDataSourceItemException(string message) : base(message)
    {
    }

    public InvalidDataSourceItemException(string message, Exception inner) : base(message, inner)
    {
    }

    protected InvalidDataSourceItemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}