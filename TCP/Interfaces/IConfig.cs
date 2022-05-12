namespace TCP.Interfaces
{
  public interface IConfig
  {
    Dictionary<Type, IFragment> CreateFragments();
    void CreateFragments<T>(Dictionary<Type, IFragment> fragmentsMap) where T : IFragment, new();
  }
}
