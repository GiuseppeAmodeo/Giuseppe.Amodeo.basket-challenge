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

        GameObject gameObject  = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+2")).gameObject;
        GameObject gameObject2 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+3")).gameObject;
        GameObject gameObject3 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+4")).gameObject;
        GameObject gameObject4 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+6")).gameObject;
        GameObject gameObject5 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+8")).gameObject;

        this.guiPawnScoreInfo = new List<GUIPawnScoreInfo>
        {
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject,
                ScoreType = ScoreType.SimpleScore
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject2,
                ScoreType = ScoreType.PerfectScore
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject3,
                ScoreType = ScoreType.MinBackBoardScore
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject4,
                ScoreType = ScoreType.MidBackBoardScore
            },
            new GUIPawnScoreInfo
            {
                ScoreGameObject = gameObject5,
                ScoreType = ScoreType.MaxBackBoardScore
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
        this.pawn.ScoreChanged -= this.OnPawnScoreChanged;
    }

    private void OnPawnScoreChanged(int score, ScoreType scoreType)
    {
        this.Score.text = score.ToString();
        this.guiPawnScoreInfo.Find((GUIPawnScoreInfo hS) => hS.ScoreType == scoreType).ScoreGameObject.SetActive(true);
    }

}
