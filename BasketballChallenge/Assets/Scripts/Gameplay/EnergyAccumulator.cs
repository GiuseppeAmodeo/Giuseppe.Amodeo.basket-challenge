using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyAccumulator : MonoBehaviour
{
    public event Action<float> ThresholdChanged;

    public event Action<float> ValueChanged;

    public float CoolDownSpeed = 0.05f;
    public float ThresholdValue = 0.7f;
    public float AccumulationCoefficient = 0.1f;

    [SerializeField]
    private Pawn pawn;

    private bool ballEnteredBasket;
    private float value;

    // Start is called before the first frame update
    private void Start()
    {
        this.pawn.Ball.EnteredBasket += this.OnBallEnteredBasket;
        this.pawn.Ball.TouchedFloor += this.OnBallTouchedFloor;

        if (this.ThresholdChanged!=null)
        {
            this.ThresholdChanged(this.ThresholdValue);
        }
    }

    private void OnDestroy()
    {
        this.pawn.Ball.EnteredBasket -= this.OnBallEnteredBasket;
        this.pawn.Ball.TouchedFloor -= this.OnBallTouchedFloor;
    }

    // Update is called once per frame
    void Update()
    {
        this.value = Mathf.Clamp01(this.value - this.CoolDownSpeed * Time.deltaTime);

        if(Mathf.Approximately(this.value, 0f))
        {
            this.EndFireBall();
        }

        if (this.ValueChanged != null)
        {
            this.ValueChanged(this.value);
        }
    }

    private void OnBallEnteredBasket(ScoreType scoreType)
    {
        this.ballEnteredBasket = true;

        if (!this.pawn.Ball.IsPowerActive)
        {
            this.value = Mathf.Clamp01(this.value + (float)scoreType * this.AccumulationCoefficient);

            if (this.value >= this.ThresholdValue)
            {
                this.BeginFireBall();
            }

            if (this.ValueChanged != null)
            {
                this.ValueChanged(this.value);
            }
        }
    }

    private void OnBallTouchedFloor()
    {
        if(!this.ballEnteredBasket)
        {
            this.EndFireBall();
        }

        this.ballEnteredBasket = false;
    }

    private void BeginFireBall()
    {
        this.pawn.Ball.IsPowerActive = true;
    }

    private void EndFireBall()
    {
        this.value = 0f;
        this.pawn.Ball.IsPowerActive = false;

        if (this.ValueChanged != null)
        {
            this.ValueChanged(this.value);
        }
    }

 
}
