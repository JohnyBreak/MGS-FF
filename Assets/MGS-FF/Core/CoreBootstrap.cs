using Infrastructure.ServiceLocator;
using UnityEngine;

public class CoreBootstrap : MonoBehaviour
{
    private async void Start()
    {
        var assetProvider = new AssetProvider();
        ServiceLocator.Register(assetProvider);

        var pauseCanvasPrefab = await assetProvider.LoadAssetAsync<GameObject>("PauseCanvas");
        var instantiatedPauseCanvas = Instantiate(pauseCanvasPrefab);
        var pauseCanvas = instantiatedPauseCanvas.GetComponent<PauseCanvas>();
        ServiceLocator.Register(pauseCanvas);
        pauseCanvas.Init();

        var playerFactory = new PlayerFactory(assetProvider);
        await playerFactory.SpawnPlayer(Vector3.zero);
        
        ServiceLocator.Get<LoaderCanvas>().Toggle(false);
    }
}
