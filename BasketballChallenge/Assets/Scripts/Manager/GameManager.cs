using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static Match CurrentMatch { get; private set; }

    public static MatchType CurrentMatchType = MatchType.PlayerVsAI;

    public static int MatchTime = 2;

    public static DiffAI CurrentDiffAI = DiffAI.Easy;

    [HideInInspector]
    [SerializeField]
    private Match[] matchPrefabsByType = new Match[Enum.GetValues(typeof(MatchType)).Length];

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(base.gameObject);
        }

        GameManager.instance = this;

        GameManager.CurrentMatch = Instantiate<Match>(this.matchPrefabsByType[(int)GameManager.CurrentMatchType]);

    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

}
