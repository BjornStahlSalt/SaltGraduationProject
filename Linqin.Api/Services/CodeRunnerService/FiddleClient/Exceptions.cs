namespace Fiddle.Exceptions;

public class FiddleClientError : Exception
{
  public FiddleClientError(string message) : base(message)
  {
  }
}