namespace Habitat.Accounts.Models
{
  public class InfoMessage
  {
    public InfoMessage()
    {
    }

    public InfoMessage(string message): this(message, MessageType.Info)
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
      Info,Success,Warning,Error
    }
  }
}