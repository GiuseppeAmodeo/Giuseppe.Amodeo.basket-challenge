using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnLocalPlayer : Pawn, IInputReceiver
{
    //public event Action<float> ForceChanged;

    //public event Action<float> PerfectForceChanged;

    //private Vector3 inputOrigin;
    //private Vector3 inputEnd;
    //private Vector3 previousInputEnd;

    private void Start()
    {
        InputManager.Instance.IsInputEnabled = true;
    }

    void IInputReceiver.OnInputDown(Vector3 position)
    {

    }

    void IInputReceiver.OnInputUp(Vector3 position)
    {

    }

    void IInputReceiver.OnInputPressed(Vector3 position)
    {


    }
  
}
