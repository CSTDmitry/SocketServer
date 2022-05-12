using TCP.Fragments.Externals;

namespace TCP.Fragments
{
  internal class Router : AbstractFragment
  {
    private delegate void Packet(int socket, ByteBuffer data);
    private Dictionary<int, Packet> PacketsList;
    private Receiver Receiver;

    public override void Initialize()
    {
      PacketsList = new Dictionary<int, Packet>();
      Receiver = TCPManager.GetFragment<Receiver>();

      PacketsList.Add((int)ClientRoutes.TestRoute, Receiver.HandleTestRoute);
    }

    public void HandleData(int socket, byte[] data)
    {
      ByteBuffer buffer = TCPManager.GetFragment<ByteBuffer>();

      try
      {
        if (PacketsList.TryGetValue(buffer.ReadInteger(), out Packet packet))
        {
          packet.Invoke(socket, buffer);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
  }
}
