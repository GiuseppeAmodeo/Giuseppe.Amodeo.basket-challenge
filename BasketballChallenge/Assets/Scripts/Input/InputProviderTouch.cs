using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProviderTouch : InputProvider
{
    private Touch touch;

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            this.touch = Input.touches[0];

            if (this.touch.phase == TouchPhase.Began)
            {
                base.OnInputDown(this.touch.position);
            }
            if (this.touch.phase == TouchPhase.Ended)
            {
                base.OnInputUp(this.touch.position);
            }
            if (this.touch.phase == TouchPhase.Moved)
            {
                base.OnInputPressed(this.touch.position);
            }
        }
    }
}
