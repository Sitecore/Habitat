namespace Sitecore.Foundation.Alerts.Models
{
  public class InfoMessage
  {
    public InfoMessage()
    {
    }

    public InfoMessage(string message) : this(message, MessageType.Info)
    {
    }

    public InfoMessage(string message, MessageType messageType)
    {
      this.Message = message;
      this.Type = messageType;
    }

    public string Message { get; set; }

    public MessageType Type { get; set; }

    public enum MessageType
    {
      Info,
      Success,
      Warning,
      Error
    }

    public static InfoMessage Error(string message) => new InfoMessage(message, MessageType.Error);
    public static InfoMessage Warning(string message) => new InfoMessage(message, MessageType.Warning);
    public static InfoMessage Success(string message) => new InfoMessage(message, MessageType.Success);
    public static InfoMessage Info(string message) => new InfoMessage(message, MessageType.Info);
  }
}