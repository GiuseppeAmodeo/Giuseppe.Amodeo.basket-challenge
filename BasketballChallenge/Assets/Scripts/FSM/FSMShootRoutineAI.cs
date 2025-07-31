using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMShootRoutineAI : FSM
{
    [SerializeField]
    private Pawn pawn;

    [SerializeField]
    private float shootDelay = 0.5f;

    [SerializeField]
    private float setupDelay = 0.5f;

    [SerializeField]
    private float minReactionTime = 1f;

    [SerializeField]
    private float maxReactionTime = 2f;

    [SerializeField]
    [Range(0f, 1f)]
    private float minShootForce;

    [SerializeField]
    [Range(0f, 1f)]
    private float maxShootForce = 1f;

    [SerializeField]
    [Range(0f, 1f)]
    private float minShootSpread;

    [SerializeField]
    [Range(0f, 1f)]
    private float maxShootSpread = 1f;

    [SerializeField]
    private DifficultySettings easyDiffSettings;

    [SerializeField]
    private DifficultySettings mediumDiffSettings;

    [SerializeField]
    private DifficultySettings hardDiffSettings;

    private FSMShootRoutineAI.StateIdle stateIdle;

    private FSMShootRoutineAI.StateShoot stateShoot;

    private FSMShootRoutineAI.StateWait stateWait;

    private void Reset()
    {
        this.pawn = base.GetComponent<Pawn>();  
    }

   private IEnumerator Start()
    {
        this.pawn.Ball.TouchedFloor += OnBallTouchedFloor;
        this.stateIdle = new FSMShootRoutineAI.StateIdle(this);
        this.stateShoot = new FSMShootRoutineAI.StateShoot(this);
        this.stateWait = new FSMShootRoutineAI.StateWait();
        this.stateIdle.Next = this.stateShoot;
        this.stateShoot.Next = this.stateWait;
        yield return null;

        base.SwitchState(this.stateIdle);
        UpdateDifficultyAI();

        yield break;
    }

    private void UpdateDifficultyAI()
    {
        switch (GameManager.CurrentDiffAI)
        {
            case DiffAI.Easy:
                this.minShootForce = this.easyDiffSettings.MinShootForce;
                this.maxShootForce = this.easyDiffSettings.MaxShootForce;
                this.minShootSpread = this.easyDiffSettings.MinShootSpread;
                this.maxShootSpread = this.easyDiffSettings.MaxShootSpread;
                break;
            case DiffAI.Medium:
                this.minShootForce = this.mediumDiffSettings.MinShootForce;
                this.maxShootForce = this.mediumDiffSettings.MaxShootForce;
                this.minShootSpread = this.mediumDiffSettings.MinShootSpread;
                this.maxShootSpread = this.mediumDiffSettings.MaxShootSpread;
                break;
            case DiffAI.Hard:
                this.minShootForce = this.hardDiffSettings.MinShootForce;
                this.maxShootForce = this.hardDiffSettings.MaxShootForce;
                this.minShootSpread = this.hardDiffSettings.MinShootSpread;
                this.maxShootSpread = this.hardDiffSettings.MaxShootSpread;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        this.pawn.Ball.TouchedFloor -= OnBallTouchedFloor;
    }

    private void OnBallTouchedFloor()
    {
        base.StartCoroutine(this.WaitForSetup());
    }

    private IEnumerator WaitForSetup()
    {
        yield return new WaitForSeconds(this.setupDelay);
        this.currentState = base.SwitchState(this.stateIdle);
        yield break;
    }


    private class StateIdle : FSMState
    {
        private float time;

        private FSMShootRoutineAI owner;

        public StateIdle(FSMShootRoutineAI owner)
        {
            this.owner = owner;
        }

        public FSMState Next { get; internal set; }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            this.owner.pawn.Setup();
            this.time = UnityEngine.Random.Range(this.owner.minReactionTime, this.owner.maxReactionTime);
        }
        public override FSMState OnStateUpdate()
        {
            if (this.owner.pawn.CanShoot)
            {
                this.time -= Time.deltaTime;
                if (this.time < 0f)
                {
                    return this.owner.SwitchState(this.Next);
                }
            }
            return this;
        }
        public override string ToString()
        {
            return "IDLE";
        }
    }

    private class StateShoot : FSMState
    {
        private FSMShootRoutineAI owner;

        private float time;

        public StateShoot(FSMShootRoutineAI owner)
        {
            this.owner = owner;
        }

        public FSMState Next { get; internal set; }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            this.time = this.owner.shootDelay;
        }

        public override FSMState OnStateUpdate()
        {
            this.time -= Time.deltaTime;

            if (this.time < 0f)
            {
                this.owner.pawn.ShootingForceNormalized = UnityEngine.Random.Range(this.owner.minShootForce, this.owner.maxShootForce);
                this.owner.pawn.ShootingSpread = UnityEngine.Random.Range(-this.owner.pawn.ShootMaxSpread * this.owner.minShootSpread, this.owner.pawn.ShootMaxSpread * this.owner.maxShootSpread);
                this.owner.pawn.Shoot();
                return this.owner.SwitchState(this.Next);
            }
            return this;
        }

        public override string ToString()
        {
            return "SHOOT";
        }
    }

    private class StateWait : FSMState
    {
        public override FSMState OnStateUpdate()
        {
            return this;
        }
        public override string ToString()
        {
            return "WAIT";
        }
    }
}
