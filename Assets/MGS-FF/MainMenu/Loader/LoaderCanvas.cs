using UnityEngine;

public class LoaderCanvas : MonoBehaviour, IInitable
{
    public void Init()
    {
        //ServiceLocator.Register(this);
        DontDestroyOnLoad(gameObject);
        Toggle(false);
    }

    public void Toggle(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<LoaderCanvas>();
    }
}
