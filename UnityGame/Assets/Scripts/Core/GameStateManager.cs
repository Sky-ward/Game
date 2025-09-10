using UnityEngine;

public enum GameState
{
    Hub,
    Exploring,
    Combat,
    Reward,
    Shop,
    Boss,
    Result,
    Pause
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Hub;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetState(GameState state)
    {
        State = state;
        Debug.Log($"GameState -> {state}");
    }
}
