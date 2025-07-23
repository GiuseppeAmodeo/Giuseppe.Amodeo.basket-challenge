using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InputProviderMouse : InputProvider
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            base.OnInputDown(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            base.OnInputUp(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            base.OnInputPressed(Input.mousePosition);
        }
    }

}
