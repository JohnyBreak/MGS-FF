using Infrastructure.ServiceLocator;
using LevelManagement;
using UnityEngine;

public class CoreBootstrap : MonoBehaviour
{
    private async void Start()
    {
        var assetProvider = new AssetProvider();
        ServiceLocator.Register(assetProvider);
        ILevelLoader levelLoader = new LevelLoaderFacade(new LevelLoader(), new LevelInitializationService());
        ServiceLocator.Register(levelLoader);
        
        var pauseCanvasPrefab = await assetProvider.LoadAssetAsync<GameObject>("PauseCanvas");
        var instantiatedPauseCanvas = Instantiate(pauseCanvasPrefab);
        var pauseCanvas = instantiatedPauseCanvas.GetComponent<PauseCanvas>();
        ServiceLocator.Register(pauseCanvas);
        pauseCanvas.Init();
        
        await levelLoader.LoadLevelAsync("Playground 1");
        
        var playerFactory = new PlayerFactory(assetProvider);
        await playerFactory.SpawnPlayer(Vector3.zero);
        
        ServiceLocator.Get<LoaderCanvas>().Toggle(false);
    }
}
