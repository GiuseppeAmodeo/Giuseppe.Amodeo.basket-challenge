using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using TMPro.EditorUtilities;

public class GUIGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject guiRewardPanel;

    [SerializeField]
    private LoadLevel loadLevelScript;

    [SerializeField]
    private TMP_Text textScore;

    [SerializeField]
    private TMP_Text textTime;

    [SerializeField]
    private GUIScore guiScoreLocalPlayer;

    [SerializeField]
    private GUIBar guiForceBar;

    [SerializeField]
    private GUIBar guiEnergyBar;

    private EnergyAccumulator energyAccumulatorLocalPlayer;

    private void Reset()
    {
        this.loadLevelScript = base.GetComponent<LoadLevel>();
        RectTransform[] componentsInChildren = base.GetComponentsInChildren<RectTransform>();
        this.guiRewardPanel = componentsInChildren.First((RectTransform hR) => hR.name.Contains("Panel_Reward")).gameObject;
        this.textScore = componentsInChildren.First((RectTransform hR) => hR.name.Contains("TextScore")).GetComponent<TMP_Text>();
        this.textTime = componentsInChildren.First((RectTransform hR) => hR.name.Contains("TextTime")).GetComponent<TMP_Text>();
        GUIBar[] componentsInChildren2 = base.GetComponentsInChildren<GUIBar>();
        this.guiForceBar = componentsInChildren2.FirstOrDefault((GUIBar hB) => hB.name.Contains("Force"));
        this.guiEnergyBar = componentsInChildren2.FirstOrDefault((GUIBar hB) => hB.name.Contains("Energy"));

    }

    private void Awake()
    {
        this.guiRewardPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.CurrentMatch.Ended += this.OnCurrentMatchEnded;
        GameManager.CurrentMatch.CurrentTimeChanged += this.OnMatchCurrentTimeChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.ForceChanged += this.OnPawnLocalPlayerForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PerfectForceChanged += this.OnPawnLocalPlayerPerfectForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PawnSetup += this.OnPawnLocalPlayerSetup;

        this.energyAccumulatorLocalPlayer = GameManager.CurrentMatch.PawnLocalPlayer.GetComponent<EnergyAccumulator>();

        if (this.energyAccumulatorLocalPlayer!=null)
        {
            this.guiEnergyBar.ThresholdValueSlider.value = this.energyAccumulatorLocalPlayer.ThresholdValue;
            this.energyAccumulatorLocalPlayer.ThresholdChanged += this.OnEnergyAccumulatorLocalPlayerThresholdChanged;
            this.energyAccumulatorLocalPlayer.ValueChanged += this.OnEnergyAccumulatorLocalPlayerValueChanged;
        }

        this.guiScoreLocalPlayer.Init(GameManager.CurrentMatch.PawnLocalPlayer);

    }

    private void OnDestroy()
    {
        GameManager.CurrentMatch.Ended -= OnCurrentMatchEnded;
        GameManager.CurrentMatch.CurrentTimeChanged -= this.OnMatchCurrentTimeChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.ForceChanged -= this.OnPawnLocalPlayerForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PerfectForceChanged -= this.OnPawnLocalPlayerPerfectForceChanged;
        GameManager.CurrentMatch.PawnLocalPlayer.PawnSetup -= this.OnPawnLocalPlayerSetup;
        this.energyAccumulatorLocalPlayer.ThresholdChanged -= this.OnEnergyAccumulatorLocalPlayerThresholdChanged;
        this.energyAccumulatorLocalPlayer.ValueChanged -= this.OnEnergyAccumulatorLocalPlayerValueChanged;
    }

    private void OnPawnLocalPlayerSetup()
    {
        this.guiForceBar.SetCurrentForce(0f);
    }

    private void OnCurrentMatchEnded()
    {
        Time.timeScale = 0f;
        InputManager.Instance.IsInputEnabled = false;
        this.guiForceBar.gameObject.SetActive(false);
        this.guiRewardPanel.SetActive(true);
        this.textScore.text = guiScoreLocalPlayer.Score.text;
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

    private void OnEnergyAccumulatorLocalPlayerValueChanged(float value)
    {
        this.guiEnergyBar.CurrentValueSlider.value = value;
    }

    private void OnEnergyAccumulatorLocalPlayerThresholdChanged(float threshold)
    {
        this.guiEnergyBar.ThresholdValueSlider.value = threshold;
    }

    public void OnButtonDonePressed()
    {
        InputManager.Instance.IsInputEnabled = true;
        this.guiForceBar.gameObject.SetActive(true);
        this.guiRewardPanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.CurrentMatch.Begin();
        GameManager.CurrentMatch.PawnLocalPlayer.ResetScore();
        GameManager.CurrentMatch.PawnLocalPlayer.Setup();
    }

    public void OnQuitButtonPressed()
    {
        this.loadLevelScript.LoadNextLevel();
        Time.timeScale = 1f;
    }
}
