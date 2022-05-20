using TCP.Interfaces;
using TCP.Fragments;
using TCP.Config;

namespace TCP
{
  public static class TCPManager
  {
    private static FragmentsBase FragmentsBase;
    public static Dictionary<int, ConnectedSocket> ConnectedSockets;
    public static Dictionary<string, ConnectedSocket> AllowedSockets;

    public static void Initialize()
    {
      FragmentsBase = new FragmentsBase(new FragmentsConfig());
      ConnectedSockets = new Dictionary<int, ConnectedSocket>();
      AllowedSockets = new Dictionary<string, ConnectedSocket>();
      FragmentsBase.Initialize();
    }

    internal static T GetFragment<T>() where T : IFragment => FragmentsBase.GetFragmentr<T>();

    public static void NewSocket(ConnectedSocket socket)
    {
      ConnectedSockets.Add(socket.Id, socket);
    }

    public static void AddAllowedSocket(int port, string id)
    {
      ConnectedSocket socket = ConnectedSockets[port];
      AllowedSockets.Add(id, socket);
      ConnectedSockets.Remove(port);
    }

    public static void Disconnect(string id)
    {
      ConnectedSocket socket = AllowedSockets[id];
      AllowedSockets.Remove(id);
    }
  }
}
