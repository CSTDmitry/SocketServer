using System.Net;
using System.Net.Sockets;
using TCP.Fragments;

namespace TCP.Factories
{
  internal class SocketFactory : AbstractFragment
  {
    public override void Initialize()
    {
    }

    public ConnectedSocket CreateSocket(TcpClient temp)
    {
      ConnectedSocket socket = new ConnectedSocket();
      socket.ClientSocket = temp;
      socket.Id = ((IPEndPoint)temp.Client.RemoteEndPoint).Port;
      socket.Initialize();

      return socket;
    }
  }
}
