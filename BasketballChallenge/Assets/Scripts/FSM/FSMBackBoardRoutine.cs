using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBackBoardRoutine : FSM
{

    [SerializeField]
    private Backboard backboard;

    [SerializeField]
    private float minWaitTime = 10f;

    [SerializeField]
    private float maxWaitTime = 20f;

    [SerializeField]
    private float minBlinkTime = 10f;

    [SerializeField]
    private float maxBlinkTime = 20f;

    private FSMBackBoardRoutine.StateIdle stateIdle;

    private FSMBackBoardRoutine.StateBlink stateBlink;


    private void Reset()
    {
        this.backboard = base.GetComponent<Backboard>();
    }

    private void Start()
    {
        this.stateIdle = new FSMBackBoardRoutine.StateIdle(this);
        this.stateBlink = new FSMBackBoardRoutine.StateBlink(this);
        this.stateIdle.Next = this.stateBlink;
        this.stateBlink.Next = this.stateIdle;
        this.SwitchState(this.stateIdle);
    }

    private class StateIdle : FSMState
    {
        private FSMBackBoardRoutine owner;

        private float time;

        public StateIdle(FSMBackBoardRoutine owner)
        {
            this.owner = owner;
        }

        public FSMState Next { get; internal set; }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            this.time = UnityEngine.Random.Range(this.owner.minWaitTime, this.owner.maxWaitTime);
        }

        public override FSMState OnStateUpdate()
        {
            this.time -= Time.deltaTime;
            if (this.time < 0f)
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

    private class StateBlink : FSMState
    {
        public FSMState Next { get; internal set; }

        private FSMBackBoardRoutine owner;

        private float time;

        public StateBlink(FSMBackBoardRoutine owner)
        {
            this.owner = owner;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            this.owner.backboard.StartBlink();
            this.time = UnityEngine.Random.Range(this.owner.minBlinkTime, this.owner.maxBlinkTime);
        }

        public override FSMState OnStateUpdate()
        {
            this.time -= Time.deltaTime;
            if (this.time < 0f)
            {
                return this.owner.SwitchState(this.Next);
            }
            return this;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            this.owner.backboard.StopBlink();
        }

        public override string ToString()
        {
            return "BLINK";
        }
    }

}
