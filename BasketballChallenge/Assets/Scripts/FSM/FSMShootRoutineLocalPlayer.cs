using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMShootRoutineLocalPlayer : FSM
{
    [SerializeField]
    private Pawn pawn;

    [SerializeField]
    private float shootDelay = 0.5f;

    [SerializeField]
    private float setupDelay = 0.5f;

    [SerializeField]
    [Tooltip("Input threshold to shoot in screen size percentage")]
    private float shootInputThreshold = 0.1f;

    private FSMShootRoutineLocalPlayer.StateIdle stateIdle;

    private FSMShootRoutineLocalPlayer.StateShoot stateShoot;

    private FSMShootRoutineLocalPlayer.StateWait stateWait;


    private void Reset()
    {
        this.pawn = base.GetComponent<Pawn>();
    }

    private IEnumerator Start()
    {
        this.pawn.Ball.TouchedFloor += this.OnBallTouchedFloor;
        this.stateIdle = new FSMShootRoutineLocalPlayer.StateIdle(this);
        this.stateShoot = new FSMShootRoutineLocalPlayer.StateShoot(this);
        this.stateWait = new FSMShootRoutineLocalPlayer.StateWait();
        this.stateIdle.Next = this.stateShoot;
        this.stateShoot.Next = this.stateWait;
        yield return null;
        base.SwitchState(this.stateIdle);
        yield break;
    }

    private void OnDestroy()
    {
        this.pawn.Ball.TouchedFloor -= this.OnBallTouchedFloor;
    }

    private void OnBallTouchedFloor()
    {
        base.StartCoroutine(this.WaitForSetup());
    }

    protected virtual IEnumerator WaitForSetup()
    {
        yield return new WaitForSeconds(this.setupDelay);
        this.currentState = base.SwitchState(this.stateIdle);
        yield break;
    }

    private class StateIdle : FSMState
    {
        public FSMState Next { get; internal set; }

        private FSMShootRoutineLocalPlayer owner;

        public StateIdle(FSMShootRoutineLocalPlayer owner)
        {
            this.owner = owner;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            this.owner.pawn.Setup();
        }

        public override FSMState OnStateUpdate()
        {
            if (this.owner.pawn.CanShoot && this.owner.pawn.ShootingForceNormalized > this.owner.shootInputThreshold)
            {
                return this.owner.SwitchState(this.Next);
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
        public FSMState Next { get; internal set; }

        private FSMShootRoutineLocalPlayer owner;

        private float time;

        public StateShoot(FSMShootRoutineLocalPlayer owner)
        {
            this.owner = owner;
        }


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
