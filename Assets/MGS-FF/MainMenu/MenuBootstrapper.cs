using UnityEngine;

public class MenuBootstrapper : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private LoaderCanvas _loaderCanvas;
    [SerializeField] private MenuBehaviour _menuBehaviour;
    
    private void Start()
    {
        ServiceLocator.Register(_sceneLoader);
        ServiceLocator.Register(_loaderCanvas);
        _loaderCanvas.Init();
        _sceneLoader.Init();
        _menuBehaviour.Init();
    }
}
