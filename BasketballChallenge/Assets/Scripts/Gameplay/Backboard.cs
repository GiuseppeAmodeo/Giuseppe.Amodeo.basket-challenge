using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backboard : MonoBehaviour
{
    public ScoreType CurrentBackboardScore { get; private set; }

    private ScoreType DefaultBackboardScore = ScoreType.SimpleScore;

    private int layerBall;

    // Start is called before the first frame update
    private void Awake()
    {
        this.layerBall = LayerMask.NameToLayer("Ball");
        CurrentBackboardScore = this.DefaultBackboardScore;
    }

  
}
