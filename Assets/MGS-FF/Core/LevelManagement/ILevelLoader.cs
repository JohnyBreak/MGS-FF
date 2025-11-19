using Cysharp.Threading.Tasks;

namespace LevelManagement
{
    public interface ILevelLoader
    {
        public UniTask LoadLevelAsync(string sceneName);
        bool IsLoaded(string sceneName);
        public UniTask UnLoadSceneAsync(string sceneName);
    }
}

