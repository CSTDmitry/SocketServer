using System.Net;
using System.Net.Sockets;
using TCP.Factories;

namespace TCP.Fragments
{
  public class TCPListener : AbstractFragment
  {
    private TcpListener Socket;

    public override void Initialize()
    {
      Socket = new TcpListener(IPAddress.Any, 26950);
      Socket.Start();
      Socket.BeginAcceptTcpClient(new AsyncCallback(ConnectCallback), null);
    }

    private void ConnectCallback(IAsyncResult result)
    {
      TCPManager.NewSocket(
        TCPManager.GetFragment<SocketFactory>().CreateSocket(Socket.EndAcceptTcpClient(result))
      );

      Socket.BeginAcceptTcpClient(new AsyncCallback(ConnectCallback), null);
    }
  }
}
