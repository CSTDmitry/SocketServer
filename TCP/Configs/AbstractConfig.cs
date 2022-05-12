using TCP.Interfaces;

namespace TCP.Config
{
  public abstract class AbstractConfig : IConfig
  {
    public abstract Dictionary<Type, IFragment> CreateFragments();

    public void CreateFragments<T>(Dictionary<Type, IFragment> fragmentsMap) where T : IFragment, new()
    {
      fragmentsMap[typeof(T)] = new T();
    }
  }
}