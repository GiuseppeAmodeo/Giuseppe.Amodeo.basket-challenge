using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameFlowManager : MonoBehaviour
{
    private enum GameState
    {
        MainMenu,
        Gameplay,
        Reward
    }

    private GameState currentGameState;

    [Header("UI References")]
    [SerializeField]
    private GameObject mainMenuUI;

    [SerializeField]
    private GameObject gameplayUI;

    [SerializeField]
    private GameObject rewardUI;

    [Header("UI Buttons")]
    [SerializeField]
    private Button btnStart;
    
    [SerializeField]
    private Button btnPlay;
    
    [SerializeField]
    private Button btnBackToMain;
    
    [SerializeField]
    private Button btnShowReward;
    
    [SerializeField]
    private Button btnQuit;

    private void Awake()
    {
        btnStart.onClick.AddListener(StartGame);
        btnBackToMain.onClick.AddListener(BackToMain);
        btnPlay.onClick.AddListener(StartGame);
        btnShowReward.onClick.AddListener(ShowReward);
        btnQuit.onClick.AddListener(QuitGame);
    }

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

        if (mainMenuUI != null)
            mainMenuUI.SetActive(currentGameState == GameState.MainMenu);

        if (gameplayUI != null)
            gameplayUI.SetActive(currentGameState == GameState.Gameplay);

        if (rewardUI != null)
            rewardUI.SetActive(currentGameState == GameState.Reward);
    }
}
