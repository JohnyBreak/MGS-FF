public class GameStateManager
{
    public enum GameState 
    {
        None = 0,
        Paused = 1,
        GamePlay = 2
    }

    private static GameStateManager _isnstance;
    public static GameStateManager Instance 
    {
        get 
        {
            if (_isnstance == null)
                _isnstance = new GameStateManager();

            return _isnstance;
        }
    }
    public GameState CurrentGameState { get; private set; }
    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler GameStateChangedEvent;

    private GameStateManager() 
    {

    }

    public void SetState(GameState newState) 
    {
        if (newState == CurrentGameState) return;

        CurrentGameState = newState;
        GameStateChangedEvent?.Invoke(newState);
    }
}
