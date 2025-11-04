using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _exitButton;
    
    public void Awake()
    {
        ServiceLocator.Register(this);
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
        _gameButton.onClick.RemoveListener(Resume);
        _exitButton.onClick.RemoveListener(BackToMenu);
    }
}
