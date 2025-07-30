using UnityEngine;
public class GUIMenuManager : MonoBehaviour
{
    private enum GameState
    {
        MainMenu,
        Gameplay,
        Reward
    }

    private GameState currentGameState;

    [Header("UI References")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject rewardUI;

    // Start is called before the first frame update
    void Start()
    {
        GoTo(GameState.MainMenu);
    }

    public void StartGame() => GoTo(GameState.Gameplay);
    public void BackToMain() => GoTo(GameState.MainMenu);
    public void ShowReward() => GoTo(GameState.Reward);
    public void QuitGame() => Application.Quit();

    private void GoTo(GameState newState)
    {
        currentGameState = newState;

        if (this.mainMenuUI != null)
            this.mainMenuUI.SetActive(this.currentGameState == GameState.MainMenu);

        if (this.gameplayUI != null)
            this.gameplayUI.SetActive(this.currentGameState == GameState.Gameplay);

        if (this.rewardUI != null)
            this.rewardUI.SetActive(this.currentGameState == GameState.Reward);
    }
}
