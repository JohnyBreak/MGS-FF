using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoaderFacade : ILevelLoader
    {
        private readonly ILevelLoader _loader;
        private readonly ILevelInitializationService _initializationService;

        public LevelLoaderFacade(ILevelLoader loader, ILevelInitializationService initializationService)
        {
            _loader = loader;
            _initializationService = initializationService;
        }

        public async UniTask LoadLevelAsync(string sceneName)
        {
            await _loader.LoadLevelAsync(sceneName);
            var scene = SceneManager.GetSceneByName(sceneName);

            var point = scene.GetComponentOnRootObject<LevelInitializationEntryPoint>();
            
            if (point == null)
            {
                return;
            }

            await _initializationService.Initialize(point);
        }

        public bool IsLoaded(string sceneName)
        {
            return _loader.IsLoaded(sceneName);
        }

        public async UniTask UnLoadSceneAsync(string sceneName)
        {
            await _loader.UnLoadSceneAsync(sceneName);
        }
    }
}