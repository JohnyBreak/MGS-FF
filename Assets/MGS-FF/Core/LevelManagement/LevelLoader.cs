using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : ILevelLoader
    {
        public async UniTask LoadLevelAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
        }
        
        public bool IsLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }

        public async UniTask UnLoadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
        }
    }
}