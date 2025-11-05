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
        
        if (needHideLoader)
        {
            ServiceLocator.Get<LoaderCanvas>().Toggle(false);
        }
        
        SceneLoadedEvent?.Invoke(sceneName);
    }
    
    public async UniTaskVoid LoadAdditiveSceneAsync(string sceneName, bool needShowLoader = false, bool needHideLoader = false)
    {
        if (isLoading) return;
        
        if (needShowLoader)
        {
            ServiceLocator.Get<LoaderCanvas>().Toggle(true);    
        }
        
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
        
        if (needHideLoader)
        {
            ServiceLocator.Get<LoaderCanvas>().Toggle(false);
        }
        
        SceneLoadedEvent?.Invoke(sceneName);
    }
    
    public async UniTaskVoid UnLoadSceneAsync(string sceneName)
    {
        await SceneManager.UnloadSceneAsync(sceneName);
        SceneUnloadedEvent?.Invoke(sceneName);
    }
    
    public void ReloadActiveScene()
    {
        LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    
    public string GetActiveSceneName() => SceneManager.GetActiveScene().name;

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

    private void OnDestroy()
    {
        ServiceLocator.Unregister<SceneLoader>();
    }

    
}
