using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GUIGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject guiRewardPanel;

    [SerializeField]
    private LoadLevel loadLevelScript;

    [SerializeField]
    private TMP_Text textScore;

    private void Reset()
    {
        this.loadLevelScript = base.GetComponent<LoadLevel>();
        RectTransform[] componentsInChildren = base.GetComponentsInChildren<RectTransform>();
        this.guiRewardPanel = componentsInChildren.First((RectTransform hR) => hR.name.Contains("Panel_Reward")).gameObject;
        this.textScore = componentsInChildren.First((RectTransform hR) => hR.name.Contains("TextScore")).GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        this.guiRewardPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.CurrentMatch.Ended += OnCurrentMatchEnded;
    }

    private void OnDestroy()
    {
        GameManager.CurrentMatch.Ended -= OnCurrentMatchEnded;
    }

    private void OnCurrentMatchEnded()
    {
        Time.timeScale = 0f;
        InputManager.Instance.IsInputEnabled = false;
        this.guiRewardPanel.SetActive(true);
        this.textScore.text = GameManager.CurrentMatch.PawnLocalPlayer.Score.ToString();
    }


    public void OnButtonDonePressed()
    {
        InputManager.Instance.IsInputEnabled = true;
        this.guiRewardPanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.CurrentMatch.Begin();
        GameManager.CurrentMatch.PawnLocalPlayer.ResetScore();
    }

    public void OnQuitButtonPressed()
    {
        this.loadLevelScript.LoadNextLevel();
        Time.timeScale = 1f;
    }
}
