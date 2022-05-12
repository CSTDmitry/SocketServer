using TCP.Interfaces;
using TCP.Fragments;
using TCP.Config;

namespace TCP
{
  public static class TCPManager
  {
    private static FragmentsBase FragmentsBase;
    public static Dictionary<int, ConnectedSocket> ConnectedSockets;

    public static void Initialize()
    {
      FragmentsBase = new FragmentsBase(new FragmentsConfig());
      ConnectedSockets = new Dictionary<int, ConnectedSocket>();
      FragmentsBase.Initialize();
    }

    internal static T GetFragment<T>() where T : IFragment => FragmentsBase.GetFragmentr<T>();

    private static void SocketHandShake(ConnectedSocket socket)
    {
      GetFragment<Sender>().SocketHandShake(socket, "Server hand shake");
    }

    public static void NewSocket(ConnectedSocket socket)
    {
      ConnectedSockets.Add(socket.Id, socket);
      SocketHandShake(socket);
    }
  }
}
