using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour, IInitable
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _exitButton;
    
    public void Init()
    {
        _gameButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
        
        _gameButton.onClick.AddListener(Resume);
        _exitButton.onClick.AddListener(BackToMenu);
        
        gameObject.SetActive(false);
    }
    
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
    private void Resume()
    {
        // game state change to gameplay
        gameObject.SetActive(false);
    }
    
    private void BackToMenu()
    {
        ServiceLocator.Get<SceneLoader>().LoadSceneAsync("Menu").Forget();
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Unregister<PauseCanvas>();
        _gameButton.onClick.RemoveListener(Resume);
        _exitButton.onClick.RemoveListener(BackToMenu);
    }
}
