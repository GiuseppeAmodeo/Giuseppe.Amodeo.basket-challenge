using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera Camera;

    public float MoveUpTime = 0.5f;

    public float MoveForwardTime = 1f;

    public float OffsetFromLookAtPoint = 1.5f;
    
    public float OffsetY = 0.5f;

    private bool isMoving;

    private LTDescr descr;

    private void Reset()
    {
        this.Camera = base.GetComponent<Camera>();
    }

    private void Awake()
    {
        if (CameraManager.Instance != null)
        {
            Destroy(base.gameObject);
        }

        CameraManager.Instance = this;
    }

    private void FixedUpdate()
    {
        if (this.isMoving)
        {
            this.descr = LeanTween.moveLocalY(base.gameObject, Court.Instance.PointEnterHoop.transform.position.y + OffsetY, this.MoveUpTime).setEase(LeanTweenType.easeOutSine).setOnComplete(delegate ()
            {
                Vector3 position = Court.Instance.PointEnterHoop.transform.position;
                Vector3 vector = position - base.transform.position;
                Vector3 to = base.transform.position + vector.normalized * (vector.magnitude - this.OffsetFromLookAtPoint);
                this.descr = LeanTween.move(base.gameObject, to, this.MoveForwardTime).setEase(LeanTweenType.easeOutSine);
            });
        }
    }

    public void Translate(Vector3 position)
    {
        base.transform.position = position;
    }

    public void LookAt(Transform target)
    {
        Vector3 normalized = (target.position - base.transform.position).normalized;
        base.transform.rotation = Quaternion.LookRotation(normalized);
    }

    public Vector3 ScreenToViewportPoint(Vector3 position)
    {
        return this.Camera.ScreenToViewportPoint(position);
    }

    public void BeginMove()
    {
        this.isMoving = true;
    }

    public void EndMove()
    {
        this.isMoving = false;
        if (this.descr != null)
        {
            LeanTween.cancel(base.gameObject);
        }
    }
}
