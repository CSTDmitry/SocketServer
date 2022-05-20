using TCP.Fragments.Externals;

namespace TCP.Fragments
{
  internal class Receiver : AbstractFragment
  {
    public override void Initialize()
    {
    }

    public void HandleGetAccess(int port, ByteBuffer buffer)
    {
      string id = buffer.ReadString();

      TCPManager.AddAllowedSocket(port, id);
      buffer.ClearBuffer();
    }

    public void HandlePosition(int socket, ByteBuffer buffer)
    {
      string id = buffer.ReadString();
      float x = buffer.ReadFloat();
      float y = buffer.ReadFloat();
      float z = buffer.ReadFloat();

      Console.WriteLine($"Position: ID: {id} x: {x} y: {y} z: {z}");
      buffer.ClearBuffer();
    }

    public void HandleDisconnect(int socket, ByteBuffer buffer)
    {
      string id = buffer.ReadString();

      TCPManager.Disconnect(id);
      buffer.ClearBuffer();
    }
  }
}
