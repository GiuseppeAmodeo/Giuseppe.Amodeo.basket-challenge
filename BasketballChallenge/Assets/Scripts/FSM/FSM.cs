using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    protected FSMState currentState;

    public FSMState SwitchState(FSMState nextState)
    {
        if (this.currentState != null)
        {
            this.currentState.OnStateExit();
        }
        this.currentState = nextState;
        if (this.currentState != null)
        {
            this.currentState.OnStateEnter();
        }
        return this.currentState;
    }

    protected virtual void Update()
    {
        if (this.currentState != null)
        {
            this.currentState = this.currentState.OnStateUpdate();
        }
    }

}
