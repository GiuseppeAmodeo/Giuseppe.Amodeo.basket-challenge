using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    public event Action Begun;

    public event Action Ended;

    public event Action<float> CurrentTimeChanged;

    public PawnLocalPlayer PawnLocalPlayer { get; private set; }

    public float CurrentTime { get; private set; }

    [SerializeField]
    private PawnLocalPlayer pawnLocalPlayerPrefab;

    [SerializeField]
    private GameSettings matchSettings;

    private int minutes = 60;

    //private float duration = float.PositiveInfinity;
    protected virtual void Awake()
    {
        this.PawnLocalPlayer = Instantiate<PawnLocalPlayer>(this.pawnLocalPlayerPrefab);
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
                base.StartCoroutine(this.WaitForPawnStopShooting());
            }
        }
    }

    private IEnumerator WaitForPawnStopShooting()
    {
        while (this.PawnLocalPlayer.IsShooting)
        {
            yield return null;
        }

        this.End();
        yield break;
    }

    public virtual void Begin()
    {
        this.CurrentTime = this.matchSettings.MatchTime * minutes;

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
