using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backboard : MonoBehaviour
{
    public ScoreType CurrentBackboardScore { get; private set; }

    private ScoreType DefaultBackboardScore = ScoreType.SimpleScore;

    private void Awake()
    {
        CurrentBackboardScore = this.DefaultBackboardScore;
    }
}
