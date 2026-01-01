using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    private GameState _currentState = GameState.None;
    public UnityAction onInitialize, onStartGame, onGameEnd, onWinGame, onGameOver;

    private void UpdateGameStates()
    {
        switch(_currentState)
        {
            case GameState.Initialize:
                onInitialize?.Invoke();
                break;
            case GameState.StartGame:
                onStartGame?.Invoke();
                break;
            case GameState.GameEnd:
                onGameEnd?.Invoke();
                break;
            case GameState.WinGame:
                onWinGame?.Invoke();
                break;
            case GameState.GameOver:
                onGameOver?.Invoke();
                break;
            case GameState.None:
            default:
                break;
        }
    }

    public void SetState(GameState state)
    {
        _currentState = state;
        UpdateGameStates();
    }
}

public enum GameState
{
    Initialize,
    StartGame,
    GameEnd,
    WinGame,
    GameOver,
    None
}