using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputProvider : MonoBehaviour
{

    public IInputReceiver[] InputReceivers;

    private void Awake()
    {
        this.InputReceivers = base.GetComponentsInChildren<IInputReceiver>();
    }


    protected void OnInputDown(Vector3 position)
    {
        for (int i = 0; i < this.InputReceivers.Length; i++)
        {
            this.InputReceivers[i].OnInputDown(position);
        }
    }

    protected void OnInputUp(Vector3 position)
    {
        for (int i = 0; i < this.InputReceivers.Length; i++)
        {
            this.InputReceivers[i].OnInputUp(position);
        }
    }

    protected void OnInputPressed(Vector3 position)
    {
        for (int i = 0; i < this.InputReceivers.Length; i++)
        {
            this.InputReceivers[i].OnInputPressed(position);
        }
    }
}
