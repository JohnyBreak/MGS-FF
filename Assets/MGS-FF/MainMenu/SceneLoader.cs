using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IInitable
{
    public event Action<string> SceneLoadedEvent;
    public event Action<string> SceneUnloadedEvent;

    private LoaderCanvas _loaderCanvas;
    bool isLoading = false;
    
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        //ServiceLocator.Register(this);
    }

    public async UniTaskVoid LoadSceneAsync(string sceneName, bool needShowLoader = true, bool needHideLoader = true)
    {
        if (isLoading) return;
        
        if (needShowLoader)
        {
            ServiceLocator.Get<LoaderCanvas>().Toggle(true);    
        }
        
        await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        //await UniTask.Delay(5000, true);
        
        if (needHideLoader)
        {
            ServiceLocator.Get<LoaderCanvas>().Toggle(false);
        }
        
        SceneLoadedEvent?.Invoke(sceneName);
    }
    
    public void ReloadActiveScene()
    {
        LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    
    public string GetActiveSceneName() => SceneManager.GetActiveScene().name;
    
    private void OnDestroy()
    {
        ServiceLocator.Unregister<SceneLoader>();
    }
}
