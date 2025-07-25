using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GUIScore : MonoBehaviour
{
    public TMP_Text Score;

    private Pawn pawn;

    [SerializeField]
    private List<GUIPawnScoreInfo> guiPawnScoreInfo;

    private void Reset()
    {
        RectTransform[] componentsInChildren = GetComponentsInChildren<RectTransform>();
        this.Score = componentsInChildren.First((RectTransform hR) => hR.name.Contains("Score") && hR.gameObject != base.gameObject).GetComponent<TMP_Text>();

        GameObject gameObject = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+2")).gameObject;
        GameObject gameObject2 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+3")).gameObject;
        GameObject gameObject3 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+4")).gameObject;
        GameObject gameObject4 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+6")).gameObject;
        GameObject gameObject5 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+8")).gameObject;

        GameObject gameObject6 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+4 double")).gameObject;
        GameObject gameObject7 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+6 double")).gameObject;
        GameObject gameObject8 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+8 double")).gameObject;
        GameObject gameObject9 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+12 double")).gameObject;
        GameObject gameObject10 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+16 double")).gameObject;

        this.guiPawnScoreInfo = new List<GUIPawnScoreInfo>
        {
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject,
                ScoreType = ScoreType.SimpleScore,
                IsPowerActive=false
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject2,
                ScoreType = ScoreType.PerfectScore,
                IsPowerActive=false
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject3,
                ScoreType = ScoreType.MinBackBoardScore,
                IsPowerActive=false
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject4,
                ScoreType = ScoreType.MidBackBoardScore,
                IsPowerActive=false
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject5,
                ScoreType = ScoreType.MaxBackBoardScore,
                IsPowerActive=false
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject6,
                ScoreType = ScoreType.SimpleScore,
                IsPowerActive=true
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject7,
                ScoreType = ScoreType.PerfectScore,
                IsPowerActive=true
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject8,
                ScoreType = ScoreType.MinBackBoardScore,
                IsPowerActive=true
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject9,
                ScoreType = ScoreType.MidBackBoardScore,
                IsPowerActive=true
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject10,
                ScoreType = ScoreType.MaxBackBoardScore,
                IsPowerActive=true
            }
        };
    }

    private void Awake()
    {
        for (int i = 0; i < this.guiPawnScoreInfo.Count; i++)
        {
            this.guiPawnScoreInfo[i].ScoreGameObject.SetActive(false);
        }
    }

    public void Init(Pawn pawn)
    {
        this.pawn = pawn;
        this.pawn.ScoreChanged += this.OnPawnScoreChanged;
    }

    private void OnDestroy()
    {
        if (this.pawn != null)
        {
            this.pawn.ScoreChanged -= this.OnPawnScoreChanged;
        }
    }

    private void OnPawnScoreChanged(int score, ScoreType scoreType, bool isPowerActive)
    {
        this.Score.text = score.ToString();
        this.guiPawnScoreInfo.Find((GUIPawnScoreInfo hS) => hS.ScoreType == scoreType && hS.IsPowerActive == isPowerActive).ScoreGameObject.SetActive(true);
    }

}
