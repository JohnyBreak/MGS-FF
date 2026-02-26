using System;

namespace GameState
{
    public static class GameState
    {
        public enum State 
        {
            None = 0,
            Paused = 1,
            GamePlay = 2,
            Dialogue = 3
        }
    
        public static State CurrentState { get; private set; }
        public static event Action<State> GameStateChangedEvent;


        public static void SetState(State newState) 
        {
            if (newState == CurrentState) return;

            CurrentState = newState;
            GameStateChangedEvent?.Invoke(newState);
        }
    }
}

