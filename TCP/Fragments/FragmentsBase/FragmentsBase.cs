using TCP.Interfaces;

namespace TCP.Fragments
{
  public class FragmentsBase
  {
    private Dictionary<Type, IFragment> FragmentsMap;

    public FragmentsBase(IConfig config)
    {
      FragmentsMap = new Dictionary<Type, IFragment>();
      FragmentsMap = config.CreateFragments();
    }

    public void Initialize()
    {
      foreach (var fragment in FragmentsMap.Values)
      {
        fragment.Initialize();
      }
    }

    public T GetFragmentr<T>() where T : IFragment => (T)FragmentsMap[typeof(T)];
  }
}