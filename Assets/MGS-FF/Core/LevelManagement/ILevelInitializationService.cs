using Cysharp.Threading.Tasks;

namespace LevelManagement
{
    public interface ILevelInitializationService
    {
        UniTask Initialize(LevelInitializationEntryPoint initializationEntryPoint);
    }
}