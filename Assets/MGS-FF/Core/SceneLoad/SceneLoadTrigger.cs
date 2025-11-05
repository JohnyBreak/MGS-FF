using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private string[] _scenesToLoad;
    [SerializeField] private string[] _scenesToUnload;

    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = ServiceLocator.Get<SceneLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            return;
        }

        LoadScenes();
        UnLoadScenes();
    }

    private void LoadScenes()
    {
        if (_sceneLoader == null)
        {
            Debug.LogError($"[SceneLoadTrigger] at {transform.position} _sceneLoader == null");
            return;
        }

        foreach (var sceneName in _scenesToLoad)
        {
            if (_sceneLoader.IsLoaded(sceneName))
            {
                continue;
            }

            _sceneLoader.LoadAdditiveSceneAsync(sceneName).Forget();
        }
    }

    private void UnLoadScenes()
    {
        if (_sceneLoader == null)
        {
            Debug.LogError($"[SceneLoadTrigger] at {transform.position} _sceneLoader == null");
            return;
        }
        foreach (var sceneName in _scenesToUnload)
        {
            if (!_sceneLoader.IsLoaded(sceneName))
            {
                continue;
            }

            _sceneLoader.UnLoadSceneAsync(sceneName).Forget();
        }
    }
}