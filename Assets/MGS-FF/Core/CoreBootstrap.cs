using System.Collections.Generic;
using Collectables;
using Collectables.UI;
using Infrastructure.ServiceLocator;
using LevelManagement;
using UnityEngine;

public class CoreBootstrap : MonoBehaviour
{
    [SerializeField] private CollectableFloatingTextCanvas _canvas;
    
    private TestCollectableContainer _container = new ();
    
    private async void Start()
    {
        var assetProvider = new AssetProvider();
        ServiceLocator.Register(assetProvider);
        
        var resolver = new CollectableResolver(new List<ICollector>()
        {
            new PistolAmmoCollector(_container)
        });

        CollectableService collectableService = new CollectableService(
            assetProvider,
            resolver,
            _canvas);
        
        ILevelLoader levelLoader = new LevelLoaderFacade(
            new LevelLoader(), 
            new LevelInitializationService(collectableService));
        
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
