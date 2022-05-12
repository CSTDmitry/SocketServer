using TCP.Fragments.Externals;

namespace TCP.Fragments
{
  public class Sender : AbstractFragment
  {
    public override void Initialize()
    {
    }

    private void SendDataTo(ConnectedSocket socket, byte[] data)
    {
      ByteBuffer buffer = TCPManager.GetFragment<ByteBuffer>();

      buffer.Write((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
      buffer.Write(data);

      socket.Stream.BeginWrite(buffer.GetArray, 0, buffer.GetArray.Length, null, null);
      buffer.ClearBuffer();
    }

    public void SocketHandShake(ConnectedSocket socket, string message)
    {
      ByteBuffer buffer = TCPManager.GetFragment<ByteBuffer>();

      buffer.Write((int)ServerRoutes.SocketHandShake);
      buffer.Write(message);

      SendDataTo(socket, buffer.GetArray);
      buffer.ClearBuffer();
    }
  }
}
