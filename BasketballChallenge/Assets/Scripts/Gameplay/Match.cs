using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    public event Action Begun;

    public event Action Draw;

    public event Action Ended;

    public event Action ExtraTimeAdded;

    public event Action<float> CurrentTimeChanged;

    public PawnLocalPlayer PawnLocalPlayer { get; private set; }

    public Pawn PawnOpponent { get; private set; }

    public float CurrentTime { get; private set; }

    [SerializeField]
    private PawnLocalPlayer pawnLocalPlayerPrefab;

    [SerializeField]
    private Pawn pawnOpponentPrefab;

    [SerializeField]
    private float extraTime = 10f;

    private int minutes = 60;

    //private float duration = float.PositiveInfinity;
    protected virtual void Awake()
    {
        this.PawnLocalPlayer = Instantiate<PawnLocalPlayer>(this.pawnLocalPlayerPrefab);

        if (this.pawnOpponentPrefab != null)
        {
            this.PawnOpponent = Instantiate<Pawn>(this.pawnOpponentPrefab);
        }
    }

    protected virtual void Start()
    {
        this.Begin();
    }

    protected virtual void Update()
    {
        if (this.CurrentTime > 0f)
        {
            this.CurrentTime -= Time.deltaTime;

            if (this.CurrentTimeChanged != null)
            {
                this.CurrentTimeChanged(this.CurrentTime);
            }

            if (this.CurrentTime <= 0f)
            {
                if (GameManager.CurrentMatch.PawnOpponent != null)
                {
                    if (GameManager.CurrentMatch.PawnLocalPlayer.Score == GameManager.CurrentMatch.PawnOpponent.Score)
                    {
                        this.AddExtraTime();
                    }
                }

                base.StartCoroutine(this.WaitForBothPawnStopShooting());
            }
        }
    }

    private void AddExtraTime()
    {
        this.CurrentTime += this.extraTime - this.CurrentTime;

        if (this.ExtraTimeAdded != null)
        {
            this.ExtraTimeAdded();
        }
    }

    private IEnumerator WaitForBothPawnStopShooting()
    {

        if (this.Draw != null)
        {
            this.Draw();
        }

        if (GameManager.CurrentMatch.PawnOpponent != null)
        {
            while (GameManager.CurrentMatch.PawnLocalPlayer.IsShooting || GameManager.CurrentMatch.PawnOpponent.IsShooting)
            {
                yield return null;
            }

            if (GameManager.CurrentMatch.PawnLocalPlayer.Score == GameManager.CurrentMatch.PawnOpponent.Score)
            {
                this.AddExtraTime();
            }
        }

        this.End();
        yield break;
    }

    public virtual void Begin()
    {
        this.CurrentTime = GameManager.MatchTime * minutes;

        if (this.Begun != null)
        {
            this.Begun();
        }
    }

    public virtual void End()
    {
        if (this.Ended != null)
        {
            this.Ended();
        }
    }
}
