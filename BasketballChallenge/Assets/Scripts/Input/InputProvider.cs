using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputProvider : MonoBehaviour
{

    public IInputReceiver[] inputReceivers;

    private void Awake()
    {
        this.inputReceivers = base.GetComponentsInChildren<IInputReceiver>();
    }


    protected void OnInputDown(Vector3 position)
    {
        for (int i = 0; i < this.inputReceivers.Length; i++)
        {
            this.inputReceivers[i].OnInputDown(position);
        }
    }

    protected void OnInputUp(Vector3 position)
    {
        for (int i = 0; i < this.inputReceivers.Length; i++)
        {
            this.inputReceivers[i].OnInputUp(position);
        }
    }

    protected void OnInputPressed(Vector3 position)
    {
        for (int i = 0; i < this.inputReceivers.Length; i++)
        {
            this.inputReceivers[i].OnInputPressed(position);
        }
    }
}
