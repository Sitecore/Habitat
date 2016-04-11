namespace Sitecore.Foundation.Installer
{
  using System;
  using System.Runtime.Serialization;

  public class ProcessException : Exception
  {
    public ProcessException()
    {
    }
    public ProcessException(string message) : base(message)
    {
    }
    public ProcessException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}