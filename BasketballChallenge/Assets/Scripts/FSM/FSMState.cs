using System; 

public abstract class FSMState
{
    public virtual void OnStateEnter()
    {
    }

    public abstract FSMState OnStateUpdate();

    public virtual void OnStateExit()
    {
    }
}

