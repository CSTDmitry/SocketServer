using System.Net.Sockets;
using TCP.Fragments.Externals;

namespace TCP.Fragments
{
  public class ConnectedSocket : AbstractFragment
  {
    public int Id;
    public TcpClient ClientSocket;
    public NetworkStream Stream;
    private byte[] ReceiveBuffer;

    public override void Initialize()
    {
      ClientSocket.SendBufferSize = 4096;
      ClientSocket.ReceiveBufferSize = 4096;
      ReceiveBuffer = new byte[4096];
      Stream = ClientSocket.GetStream();
      Stream.BeginRead(ReceiveBuffer, 0, ClientSocket.ReceiveBufferSize, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
      try
      {
        if (Stream.EndRead(result) == 0)
        {
          Console.WriteLine("Connection lost");
          return;
        }
        else
        {
          TCPManager.GetFragment<ByteBuffer>().Write(ReceiveBuffer);
          TCPManager.GetFragment<Router>().HandleData(Id, ReceiveBuffer);
          
          Stream.BeginRead(ReceiveBuffer, 0, ClientSocket.ReceiveBufferSize, ReceiveCallback, null);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
  }
}
