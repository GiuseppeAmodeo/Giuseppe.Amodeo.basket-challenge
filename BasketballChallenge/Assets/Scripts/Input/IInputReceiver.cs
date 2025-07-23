using System;
using UnityEngine;

public interface IInputReceiver
{
    public void OnInputDown(Vector3 position);

    public void OnInputUp(Vector3 position);

    public void OnInputPressed(Vector3 position);
}
