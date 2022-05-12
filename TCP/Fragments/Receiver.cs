using TCP.Fragments.Externals;

namespace TCP.Fragments
{
  internal class Receiver : AbstractFragment
  {
    public override void Initialize()
    {
    }

    public void HandleTestRoute(int socket, ByteBuffer buffer)
    {
      string clientId = buffer.ReadString();
      string message = buffer.ReadString();

      Console.WriteLine($"Test route message from: {clientId} Message: {message}");
      buffer.ClearBuffer();
    }
  }
}
