using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour, IInitable
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _exitButton;
    
    public void Init()
    {
        _gameButton.onClick.AddListener(GoToGame);
        _exitButton.onClick.AddListener(Exit);
    }

    private void GoToGame()
    {
        ServiceLocator.Get<SceneLoader>().LoadSceneAsync("Playground").Forget();
    }
    
    private void Exit()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    private void OnDestroy()
    {
        _gameButton.onClick.RemoveListener(GoToGame);
        _exitButton.onClick.RemoveListener(Exit);
    }
}
