using TCP.Interfaces;
using TCP.Fragments;
using TCP.Fragments.Externals;
using TCP.Factories;

namespace TCP.Config
{
  public class FragmentsConfig : AbstractConfig
  {
    public override Dictionary<Type, IFragment> CreateFragments()
    {
      var fragmentsMap = new Dictionary<Type, IFragment>();

      CreateFragments<ByteBuffer>(fragmentsMap);
      CreateFragments<SocketFactory>(fragmentsMap);
      CreateFragments<TCPListener>(fragmentsMap);
      CreateFragments<Router>(fragmentsMap);
      CreateFragments<Sender>(fragmentsMap);
      CreateFragments<Receiver>(fragmentsMap);

      return fragmentsMap;
    }
  }
}