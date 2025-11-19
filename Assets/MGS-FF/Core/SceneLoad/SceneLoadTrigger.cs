using UnityEngine;
using Infrastructure.ServiceLocator;
using LevelManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private string[] _scenesToLoad;
    [SerializeField] private string[] _scenesToUnload;

    private ILevelLoader _levelLoader;

    private void Start()
    {
        _levelLoader = ServiceLocator.Get<ILevelLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            return;
        }

        LoadLevels();
        UnLoadLevels();
    }

    private void LoadLevels()
    {
        if (_levelLoader == null)
        {
            Debug.LogError($"[SceneLoadTrigger] at {transform.position} _sceneLoader == null");
            return;
        }

        foreach (var sceneName in _scenesToLoad)
        {
            if (_levelLoader.IsLoaded(sceneName))
            {
                continue;
            }

            _levelLoader.LoadLevelAsync(sceneName);
        }
    }

    private void UnLoadLevels()
    {
        if (_levelLoader == null)
        {
            Debug.LogError($"[SceneLoadTrigger] at {transform.position} _sceneLoader == null");
            return;
        }
        foreach (var sceneName in _scenesToUnload)
        {
            if (!_levelLoader.IsLoaded(sceneName))
            {
                continue;
            }

            _levelLoader.UnLoadSceneAsync(sceneName);
        }
    }
}