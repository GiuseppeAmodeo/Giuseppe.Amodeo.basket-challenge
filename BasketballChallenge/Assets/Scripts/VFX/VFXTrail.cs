using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTrail : MonoBehaviour, IEffect, IInputReceiver
{
    public TrailRenderer Renderer;

    [Tooltip("Render Offset From Camera")]
    public float Offset = 1.0f;

    [Tooltip("Duration Time")]
    public float duration = 1.0f;

    private void Reset()
    {
        this.Renderer = base.GetComponent<TrailRenderer>();
        this.Renderer.enabled = false;
    }

    void IInputReceiver.OnInputDown(Vector3 position)
    {
        this.Play();
    }

    void IInputReceiver.OnInputUp(Vector3 position)
    {
        this.Stop();
    }

    void IInputReceiver.OnInputPressed(Vector3 position)
    {
        position.z = this.Offset;
        Vector3 position2 = Camera.main.ScreenToWorldPoint(position);
        base.transform.position = position2;
    }

    public void Play()
    {
        this.Renderer.enabled = true;
        this.Renderer.time = this.duration;
    }

    public void Stop()
    {
        this.Renderer.Clear();
        this.Renderer.time = -1f;
        this.Renderer.enabled = false;
    }
}
