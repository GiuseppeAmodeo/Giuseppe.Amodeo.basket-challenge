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

    private int time = 0;
    private int minTime = 2;
    private int maxTime = 10;

    private void Reset()
    {
        this.loadLevelScript = base.GetComponent<LoadLevel>();
    }

    private void Start()
    {
        time = minTime;
    }

    public void OnButtonPlayPressed()
    {
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

    private void AddTime(int amount)
    {
        time = Mathf.Clamp(time + amount, minTime, maxTime);

        if (timerText != null)
        {
            timerText.text = time.ToString() + " min";
        }
    }
}
