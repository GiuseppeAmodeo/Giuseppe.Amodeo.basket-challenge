using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GUIMenuManager : MonoBehaviour
{
    [SerializeField]
    private LoadLevel loadLevelScript;

    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private TMP_Text diffText;

    private int time = 0;
    private int minTime = 2;
    private int maxTime = 10;

    private int diff = 0;

    private void Reset()
    {
        this.loadLevelScript = base.GetComponent<LoadLevel>();
    }

    private void Start()
    {
        time = minTime;
    }

    public void OnButtonSinglePlayerPressed()
    {
        GameManager.CurrentMatchType = MatchType.SinglePlayer;
        GameManager.MatchTime = time;
        this.loadLevelScript.LoadNextLevel();
    }

    public void OnButtonPlayerVsAIPressed()
    {
        GameManager.CurrentDiffAI = (DiffAI)diff;
        GameManager.CurrentMatchType = MatchType.PlayerVsAI;
        GameManager.MatchTime = time;
        this.loadLevelScript.LoadNextLevel();
    }

    public void OnButtonQuitPressed()
    {
        Application.Quit();
    }

    public void OnButtonIncreaseTime()
    {
        AddTime(+1);
    }

    public void OnButtonDecreaseTime()
    {
        AddTime(-1);
    }

    public void OnButtonIncreaseDiff()
    {
        UpdateDiff(+1);
    }

    public void OnButtonDecreaseDiff()
    {
        UpdateDiff(-1);
    }

    private void UpdateDiff(int amount)
    {
        this.diff = Mathf.Clamp(this.diff + amount, 0, Enum.GetValues(typeof(DiffAI)).Length - 1);

        if (this.diffText != null)
        {
            this.diffText.text = ((DiffAI)diff).ToString();
        }
    }



    private void AddTime(int amount)
    {
        this.time = Mathf.Clamp(this.time + amount, this.minTime, this.maxTime);

        if (this.timerText != null)
        {
            this.timerText.text = time.ToString() + " min";
        }
    }
}
