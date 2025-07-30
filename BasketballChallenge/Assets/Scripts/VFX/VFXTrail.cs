using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VFXTrail : MonoBehaviour, IEffect, IInputReceiver
{
    public TrailRenderer Renderer;

    [Tooltip("Render Offset From Camera")]
    public float Offset = 1.0f;

    [Tooltip("Duration Time")]
    public float Duration = 1.0f;

    [Tooltip("Minimum distance before a new vertex is added")]
    public float MinVertexDistance = 0.1f;

    private Camera cameraMain;

    private void Reset()
    {
        this.Renderer = base.GetComponent<TrailRenderer>();
        this.Renderer.emitting = false;
    }

    private void Awake()
    {
        cameraMain = Camera.main;

        Renderer.time = Duration;
        Renderer.minVertexDistance = MinVertexDistance;
        Renderer.numCapVertices = 0;
        Renderer.numCornerVertices = 0;
        Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Renderer.receiveShadows = false;
    }

    void IInputReceiver.OnInputDown(Vector3 position)
    {
        UpdatePosition(position);
        this.Play();
    }

    void IInputReceiver.OnInputUp(Vector3 position)
    {
        this.Stop();
    }

    void IInputReceiver.OnInputPressed(Vector3 position)
    {
        UpdatePosition(position);
    }

    public void Play()
    {
        Renderer.Clear();
        this.Renderer.emitting = true;
    }

    public void Stop()
    {
        this.Renderer.emitting = false;
    }

    private void UpdatePosition(Vector3 position)
    {
        position.z = this.Offset;
        Vector3 position2 = cameraMain.ScreenToWorldPoint(position);
        base.transform.position = position2;
    }
}
