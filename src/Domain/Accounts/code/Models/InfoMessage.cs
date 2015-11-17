namespace Habitat.Accounts.Models
{
  public class InfoMessage
  {
    public InfoMessage()
    {
    }

    public InfoMessage(string message)
    {
      this.Message = message;
    }

    public string Message { get; set; }
  }
}