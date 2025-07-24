using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Backboard : MonoBehaviour
{
    public ScoreType CurrentBackboardScore { get; private set; }

    private ScoreType DefaultBackboardScore = ScoreType.SimpleScore;

    [SerializeField]
    private List<GUIBackboardScoreInfo> guiBackboardScoreInfo;

    [Header("Probability")]
    [SerializeField, Range(0, 100)]
    private int commonWeight = 70;  //+4
    [SerializeField, Range(0, 100)]
    private int rareWeight = 25;    //+6
    [SerializeField, Range(0, 100)]
    private int veryRareWeight = 5;     //+8

    private int layerBall;

    private GUIBackboardScoreInfo currentGUIScoreInfo;


    private void Reset()
    {
        RectTransform[] componentsInChildren = base.GetComponentsInChildren<RectTransform>();
        GameObject gameObjectScore4 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+4")).gameObject;
        GameObject gameObjectScore6 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+6")).gameObject;
        GameObject gameObjectScore8 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("+8")).gameObject;

        GameObject gameObjectFrameScore4 = gameObjectScore4.GetComponentsInChildren<SpriteRenderer>().First((SpriteRenderer sp) => sp.name.Contains("Frame")).gameObject;
        GameObject gameObjectFrameScore6 = gameObjectScore6.GetComponentsInChildren<SpriteRenderer>().First((SpriteRenderer sp) => sp.name.Contains("Frame")).gameObject;
        GameObject gameObjectFrameScore8 = gameObjectScore8.GetComponentsInChildren<SpriteRenderer>().First((SpriteRenderer sp) => sp.name.Contains("Frame")).gameObject;


        this.guiBackboardScoreInfo = new List<GUIBackboardScoreInfo>
        {
            new GUIBackboardScoreInfo
            {
                Score = gameObjectScore4,
                Frame = gameObjectFrameScore4,
                ScoreType = ScoreType.MinBackBoardScore
            },
            new GUIBackboardScoreInfo
            {
                Score = gameObjectScore6,
                Frame = gameObjectFrameScore6,
                ScoreType = ScoreType.MidBackBoardScore
            },
              new GUIBackboardScoreInfo
            {
                Score = gameObjectScore8,
                Frame = gameObjectFrameScore8,
                ScoreType = ScoreType.MaxBackBoardScore
            }
        };
    }

    private void Awake()
    {
        this.layerBall = LayerMask.NameToLayer("Ball");
        CurrentBackboardScore = this.DefaultBackboardScore;

        for (int i = 0; i < this.guiBackboardScoreInfo.Count; i++)
        {
            this.guiBackboardScoreInfo[i].Frame.SetActive(false);
            this.guiBackboardScoreInfo[i].Score.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == this.layerBall && this.currentGUIScoreInfo != null && this.currentGUIScoreInfo.Score.activeSelf)
        {
            this.currentGUIScoreInfo.Frame.SetActive(true);
        }
    }


    public void StartBlink()
    {
        int total = commonWeight + rareWeight + veryRareWeight; 
        int roll = Random.Range(0, total);

        if (roll < commonWeight)
        {
            this.CurrentBackboardScore = ScoreType.MinBackBoardScore; // +4
        }
        else if (roll < commonWeight + rareWeight)
        {
            this.CurrentBackboardScore = ScoreType.MidBackBoardScore; // +6
        }
        else
        {
            this.CurrentBackboardScore = ScoreType.MaxBackBoardScore; // +8
        }

        this.currentGUIScoreInfo = this.guiBackboardScoreInfo.Find((GUIBackboardScoreInfo hs) => hs.ScoreType == this.CurrentBackboardScore);
        this.currentGUIScoreInfo.Score.SetActive(true);
    }

    public void StopBlink()
    {
        this.CurrentBackboardScore = this.DefaultBackboardScore;
        this.currentGUIScoreInfo.Score.SetActive(false);
    }
}
