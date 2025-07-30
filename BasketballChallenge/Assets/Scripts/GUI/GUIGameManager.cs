using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GUIGameManager : MonoBehaviour
{
    [SerializeField]
    private GUIReward guiRewardPanel;

    [SerializeField]
    private LoadLevel loadLevelScript;

    [SerializeField]
    private TMP_Text textScore;

    [SerializeField]
    private TMP_Text textTime;

    [SerializeField]
    private GUIScore guiScoreLocalPlayer;

    [SerializeField]
    private GUIScore guiScoreOpponent;

    [SerializeField]
    private GUIBar guiForceBar;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject panelMatchWon;

    [SerializeField]
    private GameObject panelMatchLost;

    [SerializeField]
    private GameObject panelMatchExtraTime;

    private void Reset()
    {
        this.loadLevelScript = base.GetComponent<LoadLevel>();
        RectTransform[] componentsInChildren = base.GetComponentsInChildren<RectTransform>(true);
        this.pauseMenu = componentsInChildren.First((RectTransform hR) => hR.name.Contains("PanelPause")).gameObject;
        this.guiRewardPanel = base.GetComponentInChildren<GUIReward>(true);
        this.panelMatchWon = componentsInChildren.First((RectTransform hR) => hR.name.Contains("Won")).gameObject;
        this.panelMatchLost = componentsInChildren.First((RectTransform hR) => hR.name.Contains("Lost")).gameObject;
        this.panelMatchExtraTime = componentsInChildren.First((RectTransform hR) => hR.name.Contains("ExtraTime")).gameObject;
        this.textTime = componentsInChildren.First((RectTransform hR) => hR.name.Contains("TextTime")).GetComponent<TMP_Text>();
        this.textScore = componentsInChildren.First((RectTransform hR) => hR.name.Contains("TextScore")).GetComponent<TMP_Text>();
        GUIBar[] componentsInChildren2 = base.GetComponentsInChildren<GUIBar>(true);
        this.guiForceBar = componentsInChildren2.FirstOrDefault((GUIBar hB) => hB.name.Contains("Force"));
        GUIScore[] componentsInChildren3 = base.GetComponentsInChildren<GUIScore>(true);
        this.guiScoreLocalPlayer = componentsInChildren3.FirstOrDefault((GUIScore hS) => hS.name.Contains("LocalPlayer"));
        this.guiScoreOpponent = componentsInChildren3.FirstOrDefault((GUIScore hS) => hS.name.Contains("Opponent"));
    }

    private void Awake()
    {
        this.pauseMenu.SetActive(false);
        this.guiRewardPanel.gameObject.SetActive(false);
        this.panelMatchWon.SetActive(false);
        this.panelMatchLost.SetActive(false);
        this.panelMatchExtraTime.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.CurrentMatch.Ended += this.OnCurrentMatchEnded;
        GameManager.CurrentMatch.CurrentTimeChanged += this.OnMatchCurrentTimeChanged;
        GameManager.CurrentMatch.ExtraTimeAdded += this.OnCurrentMatchExtraTimeAdded;
        GameManager.CurrentMatch.PawnLocalPlayer.ForceChanged += this.OnPawnLocalPlayerForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PerfectForceChanged += this.OnPawnLocalPlayerPerfectForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PawnSetup += this.OnPawnLocalPlayerSetup;

        this.guiScoreLocalPlayer.Init(GameManager.CurrentMatch.PawnLocalPlayer);

        if (GameManager.CurrentMatch.PawnOpponent != null)
        {
            this.guiScoreOpponent.Init(GameManager.CurrentMatch.PawnOpponent);
        }
        else
        {
            this.guiScoreOpponent.gameObject.SetActive(false);
        }

    }

    private void OnDestroy()
    {
        GameManager.CurrentMatch.Ended -= OnCurrentMatchEnded;
        GameManager.CurrentMatch.CurrentTimeChanged -= this.OnMatchCurrentTimeChanged;
        GameManager.CurrentMatch.ExtraTimeAdded -= this.OnCurrentMatchExtraTimeAdded;
        GameManager.CurrentMatch.PawnLocalPlayer.ForceChanged -= this.OnPawnLocalPlayerForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PerfectForceChanged -= this.OnPawnLocalPlayerPerfectForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PawnSetup -= this.OnPawnLocalPlayerSetup;
    }

    private void OnPawnLocalPlayerSetup()
    {
        this.guiForceBar.SetCurrentForce(0f);
    }

    private void OnCurrentMatchExtraTimeAdded()
    {
        this.panelMatchExtraTime.SetActive(true);
    }

    private void OnCurrentMatchEnded()
    {
        this.pauseMenu.SetActive(false);
        this.guiForceBar.gameObject.SetActive(false);

        if (GameManager.CurrentMatch.PawnOpponent != null)
        {
            base.StartCoroutine(this.WaitForBothPawnStopShooting());
        }
        else
        {
            this.guiRewardPanel.gameObject.SetActive(true);
            this.textScore.text = guiScoreLocalPlayer.Score.text;
            this.guiRewardPanel.SetRewardStars(GameManager.CurrentMatch.PawnLocalPlayer.Score);
        }
    }

    private IEnumerator WaitForBothPawnStopShooting()
    {
        while (GameManager.CurrentMatch.PawnLocalPlayer.IsShooting || GameManager.CurrentMatch.PawnOpponent.IsShooting)
        {
            yield return null;
        }
        if (GameManager.CurrentMatch.PawnLocalPlayer.Score > GameManager.CurrentMatch.PawnOpponent.Score)
        {
            this.panelMatchWon.SetActive(true);
        }
        else if (GameManager.CurrentMatch.PawnLocalPlayer.Score < GameManager.CurrentMatch.PawnOpponent.Score)
        {
            this.panelMatchLost.SetActive(true);
        }
        yield break;
    }

    private void OnMatchCurrentTimeChanged(float currentTime)
    {
        if (float.IsPositiveInfinity(currentTime))
        {
            this.textTime.text = string.Empty;
        }
        else
        {
            this.textTime.text = currentTime.ToString("00");
        }
    }

    private void OnPawnLocalPlayerForceChanged(float force)
    {
        this.guiForceBar.SetCurrentForce(force);
    }

    private void OnPawnLocalPlayerPerfectForceChanged(float perfectForce)
    {
        this.guiForceBar.SetPerfectForce(perfectForce);
    }

    public void OnButtonDonePressed()
    {
        this.pauseMenu.SetActive(false);
        InputManager.Instance.IsInputEnabled = true;
        Time.timeScale = 1f;
    }

    public void OnButtonRetryPressed()
    {
        this.loadLevelScript.ReloadCurrentLevel();
        Time.timeScale = 1f;
    }

    public void OnButtonQuitPressed()
    {
        this.loadLevelScript.LoadNextLevel();
        Time.timeScale = 1f;
    }

    public void SetPauseMenu()
    {
        this.pauseMenu.SetActive(!this.pauseMenu.activeSelf);
        InputManager.Instance.IsInputEnabled = !this.pauseMenu.activeSelf;
        if (this.pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

 
}
