using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameState currentState;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public bool IsState(GameState state) => currentState == state;
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
}

public enum GameState
{
    MAIN_MENU,
    GAME_PLAY,
    SETTING,
    WIN,
    LOST
}